using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xtender.Async;

namespace Xtender.DependencyInjection.Async
{
    public static class ServiceCollectionAsyncExtensions
    {
        /// <summary>
        /// AddXtender, to register a new AsyncExtender. Note that the AsyncExtender is transient, but the AsyncExtenderCore is registered singleton.
        /// </summary>
        /// <typeparam name="TState">Type of the visitor-state.</typeparam>
        /// <param name="services">IServiceCollection.</param>
        /// <param name="configuration">To setup the AsyncExtender.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddAsyncXtender<TState>(this IServiceCollection services, Action<IAsyncExtenderBuilder<TState>, IServiceProvider> configuration)
            => services.AddAsyncXtender(ServiceLifetime.Transient, configuration);

        /// <summary>
        /// AddXtender, to register a new AsyncExtender. Note that the AsyncExtender is transient, but the AsyncExtenderCore is registered singleton.
        /// </summary>
        /// <typeparam name="TState">Type of the visitor-state.</typeparam>
        /// <param name="services">IServiceCollection.</param>
        /// <param name="lifetime">ServiceLifetime.</param>
        /// <param name="configuration">To setup the AsyncExtender.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddAsyncXtender<TState>(this IServiceCollection services, ServiceLifetime lifetime, Action<IAsyncExtenderBuilder<TState>, IServiceProvider> configuration)
        {
            return services
                .TryAdd(lifetime, provider => new ServiceFactory(provider.GetService))
                .Add<IAsyncExtender<TState>>(lifetime, provider =>
                {
                    var core = provider.GetRequiredService<IAsyncExtenderCore<TState>>();
                    var factory = provider.GetRequiredService<ServiceFactory>();

                    return new AsyncExtenderProxy<TState>(proxy => new AsyncExtender<TState>(core, proxy, factory));
                })
                .AddSingleton<IAsyncExtenderCore<TState>>(provider => 
                {
                    var cores = new Dictionary<string, Func<ServiceFactory, IAsyncExtensionBase>>();
                    var builder = new AsyncExtenderBuilder<TState>(cores);

                    configuration.Invoke(builder, provider);
                    return new AsyncExtenderCore<TState>(cores.ToConcurrentDictionary());
                });
        }

        /// <summary>
        /// AddXtenderFactory, to register a new AsyncExtenderFactory given a key-type and state-type. Note that the AsyncExtenderFactory is registered singletone. The AsyncExtenderCors are internally stored and AsyncExtenders are created by using their corresponding cores.
        /// </summary>
        /// <typeparam name="TKey">Type of the search-key to identify the right AsyncExtender in the AsyncExtenderFactory.</typeparam>
        /// <typeparam name="TState">Type of the visitor-state.</typeparam>
        /// <param name="services">IServiceCollection.</param>
        /// <param name="configuration">To setup the AsyncExtenderFactory.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddAsyncXtenderFactory<TKey, TState>(this IServiceCollection services, Action<IAsyncExtenderFactoryBuilder<TKey, TState>, IServiceProvider> configuration)
        {
            return services
                .TryAdd(ServiceLifetime.Transient, provider => new ServiceFactory(provider.GetService))
                .AddSingleton<IAsyncExtenderFactoryCore<TKey, TState>>(provider =>
                {
                    var cores = new Dictionary<TKey, Func<IAsyncExtenderCore>>();
                    configuration.Invoke(new AsyncExtenderFactoryBuilder<TKey, TState>(cores), provider);

                    return new AsyncExtenderFactoryCore<TKey, TState>(cores.ToConcurrentDictionary());
                })
                .AddSingleton<IAsyncExtenderFactory<TKey, TState>>(provider =>
                {
                    var core = provider.GetRequiredService<IAsyncExtenderFactoryCore<TKey, TState>>();
                    var factory = new Func<ServiceFactory>(provider.GetRequiredService<ServiceFactory>);

                    return new AsyncExtenderFactory<TKey, TState>(core, factory);
                });
        }

        /// <summary>
        /// AddXtender, to register a new AsyncExtender. Note that the AsyncExtender is transient, but the AsyncExtenderCore is registered singleton.
        /// Stateless version, note that the diversity in AsyncExtenders is reduced to only a single registration as the state type no longer serves that purpose.
        /// </summary>
        /// <param name="services">IServiceCollection.</param>
        /// <param name="configuration">To setup the AsyncExtender.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddAsyncXtender(this IServiceCollection services, Action<IAsyncExtenderBuilder, IServiceProvider> configuration)
            => services.AddAsyncXtender(ServiceLifetime.Transient, configuration);

        /// <summary>
        /// AddXtender, to register a new AsyncExtender. Note that the AsyncExtender is transient, but the AsyncExtenderCore is registered singleton.
        /// Stateless version, note that the diversity in AsyncExtenders is reduced to only a single registration as the state type no longer serves that purpose.
        /// </summary>
        /// <param name="services">IServiceCollection.</param>
        /// <param name="lifetime">ServiceLifetime.</param>
        /// <param name="configuration">To setup the AsyncExtender.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddAsyncXtender(this IServiceCollection services, ServiceLifetime lifetime, Action<IAsyncExtenderBuilder, IServiceProvider> configuration)
        {
            return services
                .TryAdd(lifetime, provider => new ServiceFactory(provider.GetService))
                .Add<IAsyncExtender>(lifetime, provider =>
                {
                    var core = provider.GetRequiredService<IAsyncExtenderCore>();
                    var factory = provider.GetRequiredService<ServiceFactory>();

                    return new AsyncExtenderProxy(proxy => new AsyncExtender(core, proxy, factory));
                })
                .AddSingleton<IAsyncExtenderCore>(provider =>
                {
                    var cores = new Dictionary<string, Func<ServiceFactory, IAsyncExtensionBase>>();
                    var builder = new AsyncExtenderBuilder(cores);

                    configuration.Invoke(builder, provider);
                    return new AsyncExtenderCore(cores.ToConcurrentDictionary());
                });
        }

        /// <summary>
        /// AddXtenderFactory, to register a new AsyncExtenderFactory given a key-type and state-type. Note that the AsyncExtenderFactory is registered singletone. The AsyncExtenderCors are internally stored and AsyncExtenders are created by using their corresponding cores. Stateless version, note that the diversity in factories is reduced to key diversity only as the state type no longer serves that purpose.
        /// </summary>
        /// <typeparam name="TKey">Type of the search-key to identify the right AsyncExtender in the AsyncExtenderFactory.</typeparam>
        /// <param name="services">IServiceCollection.</param>
        /// <param name="configuration">To setup the AsyncExtenderFactory.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddAsyncXtenderFactory<TKey>(this IServiceCollection services, Action<IAsyncExtenderFactoryBuilder<TKey>, IServiceProvider> configuration)
        {
            return services
                .TryAdd(ServiceLifetime.Transient, provider => new ServiceFactory(provider.GetService))
                .AddSingleton<IAsyncExtenderFactoryCore<TKey>>(provider =>
                {
                    var cores = new Dictionary<TKey, Func<IAsyncExtenderCore>>();
                    configuration.Invoke(new AsyncExtenderFactoryBuilder<TKey>(cores), provider);

                    return new AsyncExtenderFactoryCore<TKey>(cores.ToConcurrentDictionary());
                })
                .AddSingleton<IAsyncExtenderFactory<TKey>>(provider =>
                {
                    var core = provider.GetRequiredService<IAsyncExtenderFactoryCore<TKey>>();
                    var factory = new Func<ServiceFactory>(provider.GetRequiredService<ServiceFactory>);

                    return new AsyncExtenderFactory<TKey>(core, factory);
                });
        }

        private static IServiceCollection TryAdd<TService>(this IServiceCollection services, ServiceLifetime lifetime, Func<IServiceProvider, TService> factory)
        {
            var service = services.FirstOrDefault(x => x.ServiceType == typeof(TService));
            if (service is not null)
            {
                if (!HasHigherLifetime(service.Lifetime, lifetime))
                {
                    return services;
                }

                services.Remove(service);
            }
            
            services.Add<TService>(lifetime, p => factory.Invoke(p));
            return services;
        }

        private static IServiceCollection Add<TService>(this IServiceCollection services, ServiceLifetime lifetime, Func<IServiceProvider, object> factory)
        {
            services.Add(new ServiceDescriptor(typeof(TService), factory.Invoke, lifetime));
            return services;
        }

        private static bool HasHigherLifetime(ServiceLifetime oldLifetime, ServiceLifetime newLifetime) =>
            (oldLifetime == ServiceLifetime.Scoped && newLifetime == ServiceLifetime.Singleton) ||
            (oldLifetime == ServiceLifetime.Transient && newLifetime != ServiceLifetime.Transient);
    }
}

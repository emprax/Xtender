using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xtender.Sync;

namespace Xtender.DependencyInjection.Sync
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// AddXtender, to register a new Extender. Note that the extender is transient, but the ExtenderCore is registered singleton.
        /// </summary>
        /// <typeparam name="TState">Type of the visitor-state.</typeparam>
        /// <param name="services">IServiceCollection.</param>
        /// <param name="configuration">To setup the Extender.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddXtender<TState>(this IServiceCollection services, Action<IExtenderBuilder<TState>, IServiceProvider> configuration)
            => services.AddXtender(ServiceLifetime.Transient, configuration);

        /// <summary>
        /// AddXtender, to register a new Extender. Note that the extender is transient, but the ExtenderCore is registered singleton.
        /// </summary>
        /// <typeparam name="TState">Type of the visitor-state.</typeparam>
        /// <param name="services">IServiceCollection.</param>
        /// <param name="lifetime">ServiceLifetime.</param>
        /// <param name="configuration">To setup the Extender.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddXtender<TState>(this IServiceCollection services, ServiceLifetime lifetime, Action<IExtenderBuilder<TState>, IServiceProvider> configuration)
        {
            return services
                .TryAdd(lifetime, provider => new ServiceFactory(provider.GetService))
                .Add<IExtender<TState>>(lifetime, provider =>
                {
                    var core = provider.GetRequiredService<IExtenderCore<TState>>();
                    var factory = provider.GetRequiredService<ServiceFactory>();

                    return new ExtenderProxy<TState>(proxy => new Extender<TState>(core, proxy, factory));
                })
                .AddSingleton<IExtenderCore<TState>>(provider => 
                {
                    var cores = new Dictionary<string, Func<ServiceFactory, IExtensionBase>>();
                    var builder = new ExtenderBuilder<TState>(cores);

                    configuration.Invoke(builder, provider);
                    return new ExtenderCore<TState>(cores.ToConcurrentDictionary());
                });
        }

        /// <summary>
        /// AddXtenderFactory, to register a new ExtenderFactory given a key-type and state-type. Note that the ExtenderFactory is registered singletone. The ExtenderCors are internally stored and Extenders are created by using their corresponding cores.
        /// </summary>
        /// <typeparam name="TKey">Type of the search-key to identify the right Extender in the ExtenderFactory.</typeparam>
        /// <typeparam name="TState">Type of the visitor-state.</typeparam>
        /// <param name="services">IServiceCollection.</param>
        /// <param name="configuration">To setup the ExtenderFactory.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddXtenderFactory<TKey, TState>(this IServiceCollection services, Action<IExtenderFactoryBuilder<TKey, TState>, IServiceProvider> configuration)
        {
            return services
                .TryAdd(ServiceLifetime.Transient, provider => new ServiceFactory(provider.GetService))
                .AddSingleton<IExtenderFactoryCore<TKey, TState>>(provider =>
                {
                    var cores = new Dictionary<TKey, Func<IExtenderCore>>();
                    configuration.Invoke(new ExtenderFactoryBuilder<TKey, TState>(cores), provider);

                    return new ExtenderFactoryCore<TKey, TState>(cores.ToConcurrentDictionary());
                })
                .AddSingleton<IExtenderFactory<TKey, TState>>(provider =>
                {
                    var core = provider.GetRequiredService<IExtenderFactoryCore<TKey, TState>>();
                    var factory = new Func<ServiceFactory>(provider.GetRequiredService<ServiceFactory>);

                    return new ExtenderFactory<TKey, TState>(core, factory);
                });
        }

        /// <summary>
        /// AddXtender, to register a new Extender. Note that the extender is transient, but the ExtenderCore is registered singleton.
        /// Stateless version, note that the diversity in extenders is reduced to only a single registration as the state type no longer serves that purpose.
        /// </summary>
        /// <param name="services">IServiceCollection.</param>
        /// <param name="configuration">To setup the Extender.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddXtender(this IServiceCollection services, Action<IExtenderBuilder, IServiceProvider> configuration)
            => services.AddXtender(ServiceLifetime.Transient, configuration);

        /// <summary>
        /// AddXtender, to register a new Extender. Note that the extender is transient, but the ExtenderCore is registered singleton.
        /// Stateless version, note that the diversity in extenders is reduced to only a single registration as the state type no longer serves that purpose.
        /// </summary>
        /// <param name="services">IServiceCollection.</param>
        /// <param name="lifetime">ServiceLifetime.</param>
        /// <param name="configuration">To setup the Extender.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddXtender(this IServiceCollection services, ServiceLifetime lifetime, Action<IExtenderBuilder, IServiceProvider> configuration)
        {
            return services
                .TryAdd(lifetime, provider => new ServiceFactory(provider.GetService))
                .Add<IExtender>(lifetime, provider =>
                {
                    var core = provider.GetRequiredService<IExtenderCore>();
                    var factory = provider.GetRequiredService<ServiceFactory>();

                    return new ExtenderProxy(proxy => new Extender(core, proxy, factory));
                })
                .AddSingleton<IExtenderCore>(provider =>
                {
                    var cores = new Dictionary<string, Func<ServiceFactory, IExtensionBase>>();
                    var builder = new ExtenderBuilder(cores);

                    configuration.Invoke(builder, provider);
                    return new ExtenderCore(cores.ToConcurrentDictionary());
                });
        }

        /// <summary>
        /// AddXtenderFactory, to register a new ExtenderFactory given a key-type and state-type. Note that the ExtenderFactory is registered singletone. The ExtenderCors are internally stored and Extenders are created by using their corresponding cores. Stateless version, note that the diversity in factories is reduced to key diversity only as the state type no longer serves that purpose.
        /// </summary>
        /// <typeparam name="TKey">Type of the search-key to identify the right Extender in the ExtenderFactory.</typeparam>
        /// <param name="services">IServiceCollection.</param>
        /// <param name="configuration">To setup the ExtenderFactory.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddXtenderFactory<TKey>(this IServiceCollection services, Action<IExtenderFactoryBuilder<TKey>, IServiceProvider> configuration)
        {
            return services
                .TryAdd(ServiceLifetime.Transient, provider => new ServiceFactory(provider.GetService))
                .AddSingleton<IExtenderFactoryCore<TKey>>(provider =>
                {
                    var cores = new Dictionary<TKey, Func<IExtenderCore>>();
                    configuration.Invoke(new ExtenderFactoryBuilder<TKey>(cores), provider);

                    return new ExtenderFactoryCore<TKey>(cores.ToConcurrentDictionary());
                })
                .AddSingleton<IExtenderFactory<TKey>>(provider =>
                {
                    var core = provider.GetRequiredService<IExtenderFactoryCore<TKey>>();
                    var factory = new Func<ServiceFactory>(provider.GetRequiredService<ServiceFactory>);

                    return new ExtenderFactory<TKey>(core, factory);
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

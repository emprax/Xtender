using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

namespace Xtender.DependencyInjection
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
        {
            return services
                .AddTransient<IExtender<TState>>(provider => 
                {
                    var core = provider.GetRequiredService<IExtenderCore<TState>>();
                    return new ExtenderProxy<TState>(proxy => new Extender<TState>(core.Provider, proxy));
                })
                .AddSingleton<IExtenderCore<TState>>(provider => 
                {
                    var cores = new ConcurrentDictionary<string, Func<IExtensionBase>>();
                    var builder = new ExtenderBuilder<TState>(cores, provider);

                    configuration.Invoke(builder, provider);
                    return new ExtenderCore<TState>(cores);
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
            return services.AddSingleton<IExtenderFactory<TKey, TState>>(provider =>
            {
                var cores = new ConcurrentDictionary<TKey, Func<IExtenderCore<TState>>>();
                configuration.Invoke(new ExtenderFactoryBuilder<TKey, TState>(cores, provider), provider);
                
                return new ExtenderFactory<TKey, TState>(cores);
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
        {
            return services
                .AddTransient<IExtender>(provider =>
                {
                    var core = provider.GetRequiredService<IExtenderCore>();
                    return new ExtenderProxy(proxy => new Extender(core.Provider, proxy));
                })
                .AddSingleton<IExtenderCore>(provider =>
                {
                    var cores = new ConcurrentDictionary<string, Func<IExtensionBase>>();
                    var builder = new ExtenderBuilder(cores, provider);

                    configuration.Invoke(builder, provider);
                    return new ExtenderCore(cores);
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
            return services.AddSingleton<IExtenderFactory<TKey>>(provider =>
            {
                var cores = new ConcurrentDictionary<TKey, Func<IExtenderCore>>();
                configuration.Invoke(new ExtenderFactoryBuilder<TKey>(cores, provider), provider);

                return new ExtenderFactory<TKey>(cores);
            });
        }
    }
}

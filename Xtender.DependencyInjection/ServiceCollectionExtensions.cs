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
                    var cores = new ConcurrentDictionary<string, Func<object>>();
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
    }
}

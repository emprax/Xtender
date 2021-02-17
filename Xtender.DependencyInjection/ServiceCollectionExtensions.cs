using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

namespace Xtender.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddXtender<TState>(this IServiceCollection services, Action<IExtenderBuilder<TState>, IServiceProvider> configuration)
        {
            return services
                .AddTransient<IExtender<TState>>(provider => new Extender<TState>(provider.GetRequiredService<IExtenderCore<TState>>().Provider))
                .AddSingleton<IExtenderCore<TState>>(provider => 
                {
                    var cores = new ConcurrentDictionary<string, Func<object>>();
                    configuration.Invoke(new ExtenderBuilder<TState>(cores, provider), provider);

                    return new ExtenderCore<TState>(cores);
                });
        }

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

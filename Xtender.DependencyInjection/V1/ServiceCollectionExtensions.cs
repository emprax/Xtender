using System;
using Microsoft.Extensions.DependencyInjection;
using Xtender.V1;

namespace Xtender.DependencyInjection.V1
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddXtender<TBaseValue, TState>(
            this IServiceCollection services, 
            Func<IExtenderBuilder<TBaseValue, TState>, IServiceProvider, IExtender<TBaseValue, TState>> configuration)
        {
            return services.AddTransient(provider => configuration.Invoke(new ExtenderBuilder<TBaseValue, TState>(), provider));
        }
    }
}

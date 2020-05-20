using System;
using Microsoft.Extensions.DependencyInjection;
using Xtender.V2;

namespace Xtender.DependencyInjection.V2
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddXtender<TState>(
            this IServiceCollection services,
            Func<IExtenderBuilder<TState>, IServiceProvider, IExtender<TState>> configuration)
        {
            return services.AddTransient(provider => configuration.Invoke(new ExtenderBuilder<TState>(), provider));
        }
    }
}

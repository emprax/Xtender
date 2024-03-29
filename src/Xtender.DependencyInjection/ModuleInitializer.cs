using Microsoft.Extensions.DependencyInjection;
using System;
using Xtender.DependencyInjection.Sync;
using Xtender.Sync;
using Xtender.Sync.Builders;

namespace Xtender.DependencyInjection;

public static class ModuleInitializer
{
    public static IServiceCollection AddXtender(this IServiceCollection services, Action<IExtendedExtenderBuilder> action) => services.AddXtender(ServiceLifetime.Singleton, action);

    public static IServiceCollection AddXtender(this IServiceCollection services, ServiceLifetime lifetime, Action<IExtendedExtenderBuilder> action)
    {
        services.Add(new ServiceDescriptor(serviceType: typeof(IExtender), lifetime: lifetime, factory: provider =>
        {
            var builder = new ExtenderBuilder();
            action.Invoke(new ExtendedExtenderBuilder(new DependencyFactory(provider), builder));

            return builder.Build();
        }));

        return services;
    }

    public static IServiceCollection AddXtender<TState>(this IServiceCollection services, Action<IExtendedExtenderBuilder<TState>> action) => services.AddXtender(ServiceLifetime.Singleton, action);

    public static IServiceCollection AddXtender<TState>(this IServiceCollection services, ServiceLifetime lifetime, Action<IExtendedExtenderBuilder<TState>> action)
    {
        services.Add(new ServiceDescriptor(serviceType: typeof(IExtender<TState>), lifetime: lifetime, factory: provider =>
        {
            var builder = new ExtenderBuilder<TState>();
            action.Invoke(new ExtendedExtenderBuilder<TState>(new DependencyFactory(provider), builder));

            return builder.Build();
        }));

        return services;
    }

    public static IServiceCollection AddXtenders(this IServiceCollection services, Action<IExtendedExtenderRegistryBuilder> action) => services.AddXtenders(ServiceLifetime.Singleton, action);

    public static IServiceCollection AddXtenders(this IServiceCollection services, ServiceLifetime lifetime, Action<IExtendedExtenderRegistryBuilder> action)
    {
        services.Add(new(serviceType: typeof(IExtenderRegistry), lifetime: lifetime, factory: provider =>
        {
            var builder = new ExtenderRegistryBuilder();
            action.Invoke(new ExtendedExtenderRegistryBuilder(new DependencyFactory(provider), builder));

            return builder.Build();
        }));

        return services;
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;
using Xtender.Async;
using Xtender.Async.Builders;
using Xtender.DependencyInjection.Async;

namespace Xtender.DependencyInjection;

public static class AsyncModuleInitializer
{
    public static IServiceCollection AddAsyncXtender(this IServiceCollection services, Action<IAsyncExtendedExtenderBuilder> action) => services.AddAsyncXtender(ServiceLifetime.Singleton, action);

    public static IServiceCollection AddAsyncXtender(this IServiceCollection services, ServiceLifetime lifetime, Action<IAsyncExtendedExtenderBuilder> action)
    {
        services.Add(new ServiceDescriptor(serviceType: typeof(IAsyncExtender), lifetime: lifetime, factory: provider =>
        {
            var builder = new AsyncExtenderBuilder();
            action.Invoke(new AsyncExtendedExtenderBuilder(new DependencyFactory(provider), builder));

            return builder.Build();
        }));

        return services;
    }

    public static IServiceCollection AddAsyncXtender<TState>(this IServiceCollection services, Action<IAsyncExtendedExtenderBuilder<TState>> action) => services.AddAsyncXtender(ServiceLifetime.Singleton, action);

    public static IServiceCollection AddAsyncXtender<TState>(this IServiceCollection services, ServiceLifetime lifetime, Action<IAsyncExtendedExtenderBuilder<TState>> action)
    {
        services.Add(new ServiceDescriptor(serviceType: typeof(IAsyncExtender<TState>), lifetime: lifetime, factory: provider =>
        {
            var builder = new AsyncExtenderBuilder<TState>();
            action.Invoke(new AsyncExtendedExtenderBuilder<TState>(new DependencyFactory(provider), builder));

            return builder.Build();
        }));

        return services;
    }

    public static IServiceCollection AddAsyncXtenders(this IServiceCollection services, Action<IAsyncExtendedExtenderRegistryBuilder> action) => services.AddAsyncXtenders(ServiceLifetime.Singleton, action);

    public static IServiceCollection AddAsyncXtenders(this IServiceCollection services, ServiceLifetime lifetime, Action<IAsyncExtendedExtenderRegistryBuilder> action)
    {
        services.Add(new(serviceType: typeof(IAsyncExtenderRegistry), lifetime: lifetime, factory: provider =>
        {
            var builder = new AsyncExtenderRegistryBuilder();
            action.Invoke(new AsyncExtendedExtenderRegistryBuilder(new DependencyFactory(provider), builder));

            return builder.Build();
        }));

        return services;
    }
}

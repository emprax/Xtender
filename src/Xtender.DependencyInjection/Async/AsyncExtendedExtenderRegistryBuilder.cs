using System;
using Xtender.Async.Builders;

namespace Xtender.DependencyInjection.Async;

internal class AsyncExtendedExtenderRegistryBuilder(IDependencyFactory factory, IAsyncExtenderRegistryBuilder builder) : IAsyncExtendedExtenderRegistryBuilder
{
    public IAsyncExtendedExtenderRegistryBuilder AddExtender(string key, Action<IAsyncExtendedExtenderBuilder> action)
    {
        builder.AddExtender(key, b => action.Invoke(new AsyncExtendedExtenderBuilder(factory, b)));
        return this;
    }

    public IAsyncExtendedExtenderRegistryBuilder AddExtender<TState>(string key, Action<IAsyncExtendedExtenderBuilder<TState>> action)
    {
        builder.AddExtender<TState>(key, b => action.Invoke(new AsyncExtendedExtenderBuilder<TState>(factory, b)));
        return this;
    }
}

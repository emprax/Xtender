using System;
using Xtender.Sync.Builders;

namespace Xtender.DependencyInjection.Sync;

internal class ExtendedExtenderRegistryBuilder(IDependencyFactory factory, IExtenderRegistryBuilder builder) : IExtendedExtenderRegistryBuilder
{
    public IExtendedExtenderRegistryBuilder AddExtender(string key, Action<IExtendedExtenderBuilder> action)
    {
        builder.AddExtender(key, b => action.Invoke(new ExtendedExtenderBuilder(factory, b)));
        return this;
    }

    public IExtendedExtenderRegistryBuilder AddExtender<TState>(string key, Action<IExtendedExtenderBuilder<TState>> action)
    {
        builder.AddExtender<TState>(key, b => action.Invoke(new ExtendedExtenderBuilder<TState>(factory, b)));
        return this;
    }
}

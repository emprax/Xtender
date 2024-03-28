using System;
using System.Collections.Generic;
using Xtender.New.Sync;

namespace Xtender.Sync.Builders;

public class ExtenderRegistryBuilder(IDictionary<string, IExtender> extenders) : IExtenderRegistryBuilder
{
    public ExtenderRegistryBuilder() : this(new Dictionary<string, IExtender>()) { }

    public IExtenderRegistry Build() => new ExtenderRegistry(extenders);

    public IExtenderRegistryBuilder AddExtender(string key, Action<IExtenderBuilder> action)
    {
        var builder = new ExtenderBuilder();
        action.Invoke(builder);

        var extender = builder.Build();
        if (!extenders.TryAdd(key, extender))
        {
            extenders[key] = extender;
        }

        return this;
    }

    public IExtenderRegistryBuilder AddExtender<TState>(string key, Action<IExtenderBuilder<TState>> action)
    {
        var builder = new ExtenderBuilder<TState>();
        action.Invoke(builder);

        var extender = builder.Build();
        if (!extenders.TryAdd(key, extender))
        {
            extenders[key] = extender;
        }

        return this;
    }
}

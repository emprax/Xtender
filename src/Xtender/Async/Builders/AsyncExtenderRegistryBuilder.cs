using System;
using System.Collections.Generic;
using Xtender.New.Async;

namespace Xtender.Async.Builders;

public class AsyncExtenderRegistryBuilder(IDictionary<string, IAsyncExtender> extenders) : IAsyncExtenderRegistryBuilder
{
    public AsyncExtenderRegistryBuilder() : this(new Dictionary<string, IAsyncExtender>()) { }

    public IAsyncExtenderRegistry Build() => new AsyncExtenderRegistry(extenders);

    public IAsyncExtenderRegistryBuilder AddExtender(string key, Action<IAsyncExtenderBuilder> action)
    {
        var builder = new AsyncExtenderBuilder();
        action.Invoke(builder);

        var extender = builder.Build();
        if (!extenders.TryAdd(key, extender))
        {
            extenders[key] = extender;
        }

        return this;
    }

    public IAsyncExtenderRegistryBuilder AddExtender<TState>(string key, Action<IAsyncExtenderBuilder<TState>> action)
    {
        var builder = new AsyncExtenderBuilder<TState>();
        action.Invoke(builder);

        var extender = builder.Build();
        if (!extenders.TryAdd(key, extender))
        {
            extenders[key] = extender;
        }

        return this;
    }
}

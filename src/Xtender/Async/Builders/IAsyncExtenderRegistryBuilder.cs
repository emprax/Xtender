using System;

namespace Xtender.Async.Builders;

public interface IAsyncExtenderRegistryBuilder
{
    IAsyncExtenderRegistryBuilder AddExtender(string key, Action<IAsyncExtenderBuilder> action);

    IAsyncExtenderRegistryBuilder AddExtender<TState>(string key, Action<IAsyncExtenderBuilder<TState>> action);

    IAsyncExtenderRegistry Build();
}

using System;
using Xtender.Sync;

namespace Xtender.Sync.Builders;

public interface IExtenderRegistryBuilder
{
    IExtenderRegistryBuilder AddExtender(string key, Action<IExtenderBuilder> action);

    IExtenderRegistryBuilder AddExtender<TState>(string key, Action<IExtenderBuilder<TState>> action);

    IExtenderRegistry Build();
}

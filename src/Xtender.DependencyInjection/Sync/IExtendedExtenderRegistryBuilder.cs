using System;

namespace Xtender.DependencyInjection.Sync;

public interface IExtendedExtenderRegistryBuilder
{
    IExtendedExtenderRegistryBuilder AddExtender(string key, Action<IExtendedExtenderBuilder> action);

    IExtendedExtenderRegistryBuilder AddExtender<TState>(string key, Action<IExtendedExtenderBuilder<TState>> action);
}

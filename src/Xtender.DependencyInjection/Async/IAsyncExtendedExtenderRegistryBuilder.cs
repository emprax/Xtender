using System;

namespace Xtender.DependencyInjection.Async;

public interface IAsyncExtendedExtenderRegistryBuilder
{
    IAsyncExtendedExtenderRegistryBuilder AddExtender(string key, Action<IAsyncExtendedExtenderBuilder> action);

    IAsyncExtendedExtenderRegistryBuilder AddExtender<TState>(string key, Action<IAsyncExtendedExtenderBuilder<TState>> action);
}

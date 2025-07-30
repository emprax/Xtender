using System;

namespace Xtender.DependencyInjection;

internal class DependencyFactory(IServiceProvider provider) : IDependencyFactory
{
    public object? Create(Type type) => provider.GetService(type);
}

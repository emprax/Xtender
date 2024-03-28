using System;

namespace Xtender.DependencyInjection;

internal interface IDependencyFactory
{
    object? Create(Type type);
}

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Xtender.Sync;

namespace Xtender.DependencyInjection.Sync;

internal class ExtensionFunctor<TContext>(ConstructorInfo constructor, IEnumerable<ParameterInfo> parameters) : IExtensionFunctor<TContext>
{
    private readonly List<ParameterInfo> parameters = parameters.ToList();

    public IExtension<TContext> Invoke(IDependencyFactory factory)
    {
        var span = CollectionsMarshal.AsSpan(this.parameters);
        ref var searchSpace = ref MemoryMarshal.GetReference(span);

        var parameters = new object?[span.Length];
        for (var index = 0; index < span.Length; index++)
        {
            var item = Unsafe.Add(ref searchSpace, index);
            parameters[index] = factory.Create(item.ParameterType);
        }

        return (IExtension<TContext>)constructor.Invoke(parameters);
    }
}

internal class ExtensionFunctor<TState, TContext>(ConstructorInfo constructor, IEnumerable<ParameterInfo> parameters) : IExtensionFunctor<TState, TContext>
{
    private readonly List<ParameterInfo> parameters = parameters.ToList();

    public IExtension<TState, TContext> Invoke(IDependencyFactory factory)
    {
        var span = CollectionsMarshal.AsSpan(this.parameters);
        ref var searchSpace = ref MemoryMarshal.GetReference(span);

        var parameters = new object?[span.Length];
        for (var index = 0; index < span.Length; index++)
        {
            var item = Unsafe.Add(ref searchSpace, index);
            parameters[index] = factory.Create(item.ParameterType);
        }

        return (IExtension<TState, TContext>)constructor.Invoke(parameters);
    }
}

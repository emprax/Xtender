using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Xtender.Async;

namespace Xtender.DependencyInjection.Async;

internal class AsyncExtensionFunctor<TContext>(ConstructorInfo constructor, IEnumerable<ParameterInfo> parameters) : IAsyncExtensionFunctor<TContext>
{
    private readonly List<ParameterInfo> parameters = parameters.ToList();

    public IAsyncExtension<TContext> Invoke(IDependencyFactory factory)
    {
        var span = CollectionsMarshal.AsSpan(this.parameters);
        ref var searchSpace = ref MemoryMarshal.GetReference(span);

        var parameters = new object?[span.Length];
        for (var index = 0; index < span.Length; index++)
        {
            var item = Unsafe.Add(ref searchSpace, index);
            parameters[index] = factory.Create(item.ParameterType);
        }

        return (IAsyncExtension<TContext>)constructor.Invoke(parameters);
    }
}

internal class AsyncExtensionFunctor<TState, TContext>(ConstructorInfo constructor, IEnumerable<ParameterInfo> parameters) : IAsyncExtensionFunctor<TState, TContext>
{
    private readonly List<ParameterInfo> parameters = parameters.ToList();

    public IAsyncExtension<TState, TContext> Invoke(IDependencyFactory factory)
    {
        var span = CollectionsMarshal.AsSpan(this.parameters);
        ref var searchSpace = ref MemoryMarshal.GetReference(span);

        var parameters = new object?[span.Length];
        for (var index = 0; index < span.Length; index++)
        {
            var item = Unsafe.Add(ref searchSpace, index);
            parameters[index] = factory.Create(item.ParameterType);
        }

        return (IAsyncExtension<TState, TContext>)constructor.Invoke(parameters);
    }
}
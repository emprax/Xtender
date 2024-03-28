using Xtender.Async;
using Xtender.DependencyInjection.New.Async;

namespace Xtender.DependencyInjection.Async;

internal class AsyncExtensionFunctorWrapper<TState, TContext>(IAsyncExtensionFunctor<TContext> functor) : IAsyncExtensionFunctor<TState, TContext>
{
    public IAsyncExtension<TState, TContext> Invoke(IDependencyFactory factory)
    {
        var result = functor.Invoke(factory);
        return new AsyncExtensionWrapper<TState, TContext>(result);
    }
}

using Xtender.Sync;

namespace Xtender.DependencyInjection.Sync;

internal class ExtensionFunctorWrapper<TState, TContext>(IExtensionFunctor<TContext> functor) : IExtensionFunctor<TState, TContext>
{
    public IExtension<TState, TContext> Invoke(IDependencyFactory factory)
    {
        var result = functor.Invoke(factory);
        return new ExtensionWrapper<TState, TContext>(result);
    }
}

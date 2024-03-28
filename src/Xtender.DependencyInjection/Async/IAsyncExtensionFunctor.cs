using Xtender.Async;

namespace Xtender.DependencyInjection.Async;

internal interface IAsyncExtensionFunctor<TContext>
{
    IAsyncExtension<TContext> Invoke(IDependencyFactory factory);
}

internal interface IAsyncExtensionFunctor<TState, TContext>
{
    IAsyncExtension<TState, TContext> Invoke(IDependencyFactory factory);
}

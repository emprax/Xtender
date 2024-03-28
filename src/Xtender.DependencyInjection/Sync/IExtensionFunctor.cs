using Xtender.Sync;

namespace Xtender.DependencyInjection.Sync;

internal interface IExtensionFunctor<TContext>
{
    IExtension<TContext> Invoke(IDependencyFactory factory);
}

internal interface IExtensionFunctor<TState, TContext>
{
    IExtension<TState, TContext> Invoke(IDependencyFactory factory);
}

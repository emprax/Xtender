using Xtender.Sync;

namespace Xtender.DependencyInjection.Sync;

internal class FactoryHandler<TItem>(IDependencyFactory factory, IExtensionFunctor<TItem> functor) : IExtension<TItem>
{
    public void Extend(TItem context, IExtender extender) => functor
        .Invoke(factory)
        .Extend(context, extender);
}

internal class FactoryHandler<TState, TItem>(IDependencyFactory factory, IExtensionFunctor<TState, TItem> functor) : IExtension<TState, TItem>
{
    public void Extend(TItem context, IExtender<TState> extender) => functor
        .Invoke(factory)
        .Extend(context, extender);
}

using System.Threading.Tasks;
using Xtender.Async;

namespace Xtender.DependencyInjection.Async;

internal class AsyncFactoryHandler<TItem>(IDependencyFactory factory, IAsyncExtensionFunctor<TItem> functor) : IAsyncExtension<TItem>
{
    public Task Extend(TItem context, IAsyncExtender extender) => functor
        .Invoke(factory)
        .Extend(context, extender);
}

internal class AsyncFactoryHandler<TState, TItem>(IDependencyFactory factory, IAsyncExtensionFunctor<TState, TItem> functor) : IAsyncExtension<TState, TItem>
{
    public Task Extend(TItem context, IAsyncExtender<TState> extender) => functor
        .Invoke(factory)
        .Extend(context, extender);
}

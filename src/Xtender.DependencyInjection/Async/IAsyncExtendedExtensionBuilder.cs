using Xtender.Async;

namespace Xtender.DependencyInjection.Async;

public interface IAsyncExtendedExtenderBuilder
{
    IAsyncExtendedExtenderBuilder Attach<TContext, TExtension>() where TExtension : IAsyncExtension<TContext>;

    IAsyncExtendedExtenderBuilder Default<TExtension>() where TExtension : IAsyncExtension<object>;
}

public interface IAsyncExtendedExtenderBuilder<TState>
{
    IAsyncExtendedExtenderBuilder<TState> Attach<TContext, TExtension>() where TExtension : IAsyncExtension<TState, TContext>;

    IAsyncExtendedExtenderBuilder<TState> AttachWithoutState<TContext, TExtension>() where TExtension : IAsyncExtension<TContext>;

    IAsyncExtendedExtenderBuilder<TState> Default<TExtension>() where TExtension : IAsyncExtension<TState, object>;
}

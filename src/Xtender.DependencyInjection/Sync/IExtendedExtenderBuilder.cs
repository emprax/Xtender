using Xtender.Sync;

namespace Xtender.DependencyInjection.Sync;

public interface IExtendedExtenderBuilder
{
    IExtendedExtenderBuilder Attach<TContext, TExtension>() where TExtension : IExtension<TContext>;

    IExtendedExtenderBuilder Default<TExtension>() where TExtension : IExtension<object>;
}

public interface IExtendedExtenderBuilder<TState>
{
    IExtendedExtenderBuilder<TState> Attach<TContext, TExtension>() where TExtension : IExtension<TState, TContext>;

    IExtendedExtenderBuilder<TState> AttachWithoutState<TContext, TExtension>() where TExtension : IExtension<TContext>;

    IExtendedExtenderBuilder<TState> Default<TExtension>() where TExtension : IExtension<TState, object>;
}

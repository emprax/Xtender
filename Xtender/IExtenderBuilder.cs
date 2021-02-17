using System;

namespace Xtender
{
    public interface IExtenderBuilder<TState>
    {
        IConnectedExtenderBuilder<TState> Default<TDefaultExtension>() where TDefaultExtension : class, IExtension<TState, object>;

        IConnectedExtenderBuilder<TState> Default();
    }

    public interface IConnectedExtenderBuilder<TState>
    {
        IConnectedExtenderBuilder<TState> Attach<TContext, TExtension>(Func<TExtension> configuration) where TExtension : class, IExtension<TState, TContext>;

        IConnectedExtenderBuilder<TState> Attach<TContext, TExtension>() where TExtension : class, IExtension<TState, TContext>;
    }
}

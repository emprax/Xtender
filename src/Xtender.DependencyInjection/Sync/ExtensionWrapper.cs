using Xtender.Sync;

namespace Xtender.DependencyInjection.Sync;

internal class ExtensionWrapper<TState, TContext>(IExtension<TContext> extension) : IExtension<TState, TContext>
{
    public void Extend(TContext context, IExtender<TState> extender) => extension.Extend(context, extender);
}

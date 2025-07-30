namespace Xtender.Sync;

internal class DefaultExtension : IExtension<object>
{
    public void Extend(object context, IExtender extender) { }
}

internal class DefaultExtension<TState> : IExtension<TState, object>
{
    public void Extend(object context, IExtender<TState> extender) { }
}

namespace Xtender.Sync;

public interface IExtension { }

public interface IExtension<in TContext> : IExtension
{
    void Extend(TContext context, IExtender extender);
}

public interface IExtension<TState, in TContext> : IExtension
{
    void Extend(TContext context, IExtender<TState> extender);
}

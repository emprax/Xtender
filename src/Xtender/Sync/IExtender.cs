namespace Xtender.Sync;

public interface IExtender
{
    void Extend<TAccepter>(Accepter<TAccepter> accepter);

    void Extend<TAccepter>(TAccepter accepter) where TAccepter : IAccepter;
}

public interface IExtender<TState> : IExtender
{
    TState? State { get; set; }
}

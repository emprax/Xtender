namespace Xtender.Sync;

internal class ExtenderProxy(IExtender extender) : IExtender
{
    public void Extend<TAccepter>(TAccepter accepter) where TAccepter : IAccepter => accepter.Accept(extender);

    public void Extend<TAccepter>(Accepter<TAccepter> accepter) => accepter.Accept(extender);
}

internal class ExtenderProxy<TState>(IExtender<TState> extender) : IExtender<TState>
{
    public TState? State
    {
        get => extender.State;
        set => extender.State = value;
    }

    public void Extend<TAccepter>(TAccepter accepter) where TAccepter : IAccepter => accepter.Accept(extender);

    public void Extend<TAccepter>(Accepter<TAccepter> accepter) => accepter.Accept(extender);
}

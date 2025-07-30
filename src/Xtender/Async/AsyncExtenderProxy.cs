using System.Threading.Tasks;

namespace Xtender.Async;

internal class AsyncExtenderProxy(IAsyncExtender extender) : IAsyncExtender
{
    public Task Extend<TAccepter>(TAccepter accepter) where TAccepter : IAsyncAccepter => accepter.Accept(extender);

    public Task Extend<TAccepter>(AsyncAccepter<TAccepter> accepter) => accepter.Accept(extender);
}

internal class AsyncExtenderProxy<TState>(IAsyncExtender<TState> extender) : IAsyncExtender<TState>
{
    public TState? State
    {
        get => extender.State;
        set => extender.State = value;
    }

    public Task Extend<TAccepter>(TAccepter accepter) where TAccepter : IAsyncAccepter => accepter.Accept(extender);

    public Task Extend<TAccepter>(AsyncAccepter<TAccepter> accepter) => accepter.Accept(extender);
}

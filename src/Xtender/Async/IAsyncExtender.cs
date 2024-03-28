using System.Threading.Tasks;

namespace Xtender.Async;

public interface IAsyncExtender
{
    Task Extend<TAccepter>(AsyncAccepter<TAccepter> accepter);

    Task Extend<TAccepter>(TAccepter accepter) where TAccepter : IAsyncAccepter;
}

public interface IAsyncExtender<TState> : IAsyncExtender
{
    TState? State { get; set; }
}

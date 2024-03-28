using System.Threading.Tasks;

namespace Xtender.Async;

public class AsyncAccepter<TContext> : IAsyncAccepter
{
    public TContext Context { get; }

    public AsyncAccepter(TContext context) => this.Context = context;

    public Task Accept(IAsyncExtender extender) => extender.Extend(this);
}

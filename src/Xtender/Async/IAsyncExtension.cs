using System.Threading.Tasks;

namespace Xtender.Async;

public interface IAsyncExtension { }

public interface IAsyncExtension<in TContext> : IAsyncExtension
{
    Task Extend(TContext context, IAsyncExtender extender);
}

public interface IAsyncExtension<TState, in TContext> : IAsyncExtension
{
    Task Extend(TContext context, IAsyncExtender<TState> extender);
}

using System.Threading.Tasks;

namespace Xtender.Async;

internal class AsyncDefaultExtension : IAsyncExtension<object>
{
    public Task Extend(object context, IAsyncExtender extender) => Task.CompletedTask;
}

internal class AsyncDefaultExtension<TState> : IAsyncExtension<TState, object>
{
    public Task Extend(object context, IAsyncExtender<TState> extender) => Task.CompletedTask;
}

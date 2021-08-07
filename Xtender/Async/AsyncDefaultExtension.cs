using System.Threading.Tasks;

namespace Xtender.Async
{
    /// <summary>
    /// Default async extension. Takes all objects and simply returns a task.
    /// </summary>
    /// <typeparam name="TState">The type of visitor-state.</typeparam>
    public class AsyncDefaultExtension<TState> : IAsyncExtension<TState, object>
    {
        public Task Extend(object context, IAsyncExtender<TState> extender) => Task.CompletedTask;
    }

    /// <summary>
    /// Default async extension. Takes all objects and simply returns a task. This is the stateless version.
    /// </summary>
    public class AsyncDefaultExtension : IAsyncExtension<object>
    {
        public Task Extend(object context, IAsyncExtender extender) => Task.CompletedTask;
    }
}
using System.Threading.Tasks;

namespace Xtender
{
    /// <summary>
    /// Default extension. Takes all objects and simply returns a task.
    /// </summary>
    /// <typeparam name="TState">The type of visitor-state.</typeparam>
    public class DefaultExtension<TState> : IExtension<TState, object>
    {
        public Task Extend(object context, IExtender<TState> extender) => Task.CompletedTask;
    }
}

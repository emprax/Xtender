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

    /// <summary>
    /// Default extension. Takes all objects and simply returns a task. This is the stateless version.
    /// </summary>
    public class DefaultExtension : IExtension<object>
    {
        public Task Extend(object context, IExtender extender) => Task.CompletedTask;
    }
}

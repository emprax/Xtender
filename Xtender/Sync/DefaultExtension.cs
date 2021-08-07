namespace Xtender.Sync
{
    /// <summary>
    /// Default extension. Takes all objects and simply returns a task.
    /// </summary>
    /// <typeparam name="TState">The type of visitor-state.</typeparam>
    public class DefaultExtension<TState> : IExtension<TState, object>
    {
        public void Extend(object context, IExtender<TState> extender) { }
    }

    /// <summary>
    /// Default extension. Takes all objects and simply returns a task. This is the stateless version.
    /// </summary>
    public class DefaultExtension : IExtension<object>
    {
        public void Extend(object context, IExtender extender) { }
    }
}

using System.Threading.Tasks;

namespace Xtender.Async
{
    /// <summary>
    /// The AsyncExtension interface. These are to implement your extensions --> visitor-segments.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    /// <typeparam name="TContext">The concrete type that this extension is visiting to handle.</typeparam>
    public interface IAsyncExtension<TState, in TContext> : IAsyncExtensionBase<TContext>
    {
        /// <summary>
        /// The Extend method (Visit-method) which is used to apply operations of sorts to the concretely implemented object that is visited.
        /// </summary>
        /// <param name="context">The concrete implementation of an object for which this extension is used to visit and handle.</param>
        /// <param name="extender">The extender which contains this extension, here injected in the Extend method for further visiting/extending and visitor-state access.</param>
        /// <returns>Task.</returns>
        Task Extend(TContext context, IAsyncExtender<TState> extender);
    }

    /// <summary>
    /// The AsyncExtension interface. These are to implement your extensions --> visitor-segments. Notice that there is no state type.
    /// </summary>
    /// <typeparam name="TContext">The concrete type that this extension is visiting to handle.</typeparam>
    public interface IAsyncExtension<in TContext> : IAsyncExtensionBase<TContext>
    {
        /// <summary>
        /// The Extend method (Visit-method) which is used to apply operations of sorts to the concretely implemented object that is visited.
        /// </summary>
        /// <param name="context">The concrete implementation of an object for which this extension is used to visit and handle.</param>
        /// <param name="extender">The extender which contains this extension, here injected in the Extend method for further visiting/extending.</param>
        /// <returns>Task.</returns>
        Task Extend(TContext context, IAsyncExtender extender);
    }
}
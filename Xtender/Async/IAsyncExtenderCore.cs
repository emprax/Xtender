using System;

namespace Xtender.Async
{
    /// <summary>
    /// The interface for implementing the async version of the core of the extender setup, provides the extensions key-value store.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public interface IAsyncExtenderCore<TState> : IAsyncExtenderCore { }

    /// <summary>
    /// The interface for implementing the async version of the core of the extender setup, provides the extensions key-value store.
    /// Notice that there is no state type.
    /// </summary>
    public interface IAsyncExtenderCore
    {
        /// <summary>
        /// Getting the extension-factory corresponding to the <typeparamref name="TKeyValue"/> type as key.
        /// NOTE: Asynchronous extensions can only be handled, for synchronous extensions cannot handle the use of an async-extender.
        /// </summary>
        /// <typeparam name="TKeyValue">Type of object to perform as key.</typeparam>
        /// <exception cref="KeyNotFoundException">When an extender-type cannot be found for the type of <typeparamref name="TKeyValue"/> as key.</exception>
        /// <returns>The async extension-factory corresponding to the <typeparamref name="TKeyValue"/> type.</returns>
        Func<ServiceFactory, IAsyncExtensionBase> GetExtensionType<TKeyValue>();
    }
}
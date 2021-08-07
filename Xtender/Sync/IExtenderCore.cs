using System;
using System.Collections.Generic;

namespace Xtender.Sync
{
    /// <summary>
    /// The interface for implementing the core of the extender setup, provides the extensions key-value store.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public interface IExtenderCore<TState> : IExtenderCore { }

    /// <summary>
    /// The interface for implementing the core of the extender setup, provides the extensions key-value store.
    /// Notice that there is no state type.
    /// </summary>
    public interface IExtenderCore
    {
        /// <summary>
        /// Getting the extension-factory corresponding to the <typeparamref name="TKeyValue"/> type as key.
        /// NOTE: Synchronous extensions only, for async extensions cannot be handled.
        /// </summary>
        /// <typeparam name="TKeyValue">Type of object to perform as key.</typeparam>
        /// <exception cref="KeyNotFoundException">When an extender-type cannot be found for the type of <typeparamref name="TKeyValue"/> as key.</exception>
        /// <returns>The extension-factory corresponding to the <typeparamref name="TKeyValue"/> type.</returns>
        Func<ServiceFactory, IExtensionBase> GetExtensionType<TKeyValue>();
    }
}

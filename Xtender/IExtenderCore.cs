using System;
using System.Collections.Generic;

namespace Xtender
{
    /// <summary>
    /// The interface for implementing the core of the extender setup, provides the extensions key-value store.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public interface IExtenderCore<TState>
    {
        /// <summary>
        /// The collection of extensions for the extender.
        /// </summary>
        IDictionary<string, Func<object>> Provider { get; }
    }
}

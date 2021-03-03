using System;
using System.Collections.Generic;

namespace Xtender
{
    /// <summary>
    /// The core of the extender setup, provides the extensions key-value store and the ExtenderAbstractionHandler.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public class ExtenderCore<TState> : IExtenderCore<TState>
    {
        public ExtenderCore(IDictionary<string, Func<object>> provider, IExtenderAbstractionHandler<TState> handler)
        {
            this.Provider = provider;
            this.Handler = handler;
        }

        /// <summary>
        /// The collection of extensions for the extender.
        /// </summary>
        public IDictionary<string, Func<object>> Provider { get; }

        /// <summary>
        /// The optional handler (be very aware of this feature and how to use it).
        /// </summary>
        public IExtenderAbstractionHandler<TState> Handler { get; }
    }
}

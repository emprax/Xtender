using System;
using System.Collections.Generic;

namespace Xtender
{
    /// <summary>
    /// The core of the extender setup, provides the extensions key-value store.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public class ExtenderCore<TState> : ExtenderCore, IExtenderCore<TState>
    {
        public ExtenderCore(IDictionary<string, Func<IExtensionBase>> provider) : base(provider) { }
    }

    /// <summary>
    /// The core of the extender setup, provides the extensions key-value store.
    /// </summary>
    public class ExtenderCore : IExtenderCore
    {
        public ExtenderCore(IDictionary<string, Func<IExtensionBase>> provider) => this.Provider = provider;

        /// <summary>
        /// The collection of extensions for the extender.
        /// </summary>
        public IDictionary<string, Func<IExtensionBase>> Provider { get; }
    }
}

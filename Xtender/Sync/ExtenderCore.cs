using System;
using System.Collections.Generic;

namespace Xtender.Sync
{
    /// <summary>
    /// The core of the extender setup, provides the extensions key-value store.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public class ExtenderCore<TState> : ExtenderCore, IExtenderCore<TState>
    {
        public ExtenderCore(IDictionary<string, Func<ServiceFactory, IExtensionBase>> registry) : base(registry) { }
    }

    /// <summary>
    /// The core of the extender setup, provides the extensions key-value store.
    /// </summary>
    public class ExtenderCore : IExtenderCore
    {
        private readonly IDictionary<string, Func<ServiceFactory, IExtensionBase>> registry;
        private static object lockSync = new();

        public ExtenderCore(IDictionary<string, Func<ServiceFactory, IExtensionBase>> registry) => this.registry = registry;

        /// <summary>
        /// Getting the extender-type corresponding to the <typeparamref name="TKeyValue"/> type as key.
        /// </summary>
        /// <typeparam name="TKeyValue">Type of object to perform as key.</typeparam>
        /// <returns>The extender-type corresponding to the <typeparamref name="TKeyValue"/> type.</returns>
        public Func<ServiceFactory, IExtensionBase> GetExtensionType<TKeyValue>()
        {
            lock (lockSync)
            {
                var name = typeof(TKeyValue).FullName;

                return this.registry.TryGetValue(name, out var factory)
                    ? factory
                    : null;
            }
        }
    }
}

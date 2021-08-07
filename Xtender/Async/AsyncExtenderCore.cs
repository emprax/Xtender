using System;
using System.Collections.Generic;

namespace Xtender.Async
{
    /// <summary>
    /// The core of the async extender setup, provides the async extensions key-value store.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public class AsyncExtenderCore<TState> : AsyncExtenderCore, IAsyncExtenderCore<TState>
    {
        public AsyncExtenderCore(IDictionary<string, Func<ServiceFactory, IAsyncExtensionBase>> registry) : base(registry) { }
    }

    /// <summary>
    /// The core of the async extender setup, provides the async extensions key-value store.
    /// </summary>
    public class AsyncExtenderCore : IAsyncExtenderCore
    {
        private readonly IDictionary<string, Func<ServiceFactory, IAsyncExtensionBase>> registry;
        private static object lockSync = new();

        public AsyncExtenderCore(IDictionary<string, Func<ServiceFactory, IAsyncExtensionBase>> registry) => this.registry = registry;

        /// <summary>
        /// Getting the extender-factory corresponding to the <typeparamref name="TKeyValue"/> type as key.
        /// </summary>
        /// <typeparam name="TKeyValue">Type of object to perform as key.</typeparam>
        /// <returns>The extender-factory corresponding to the <typeparamref name="TKeyValue"/> type.</returns>
        public Func<ServiceFactory, IAsyncExtensionBase> GetExtensionType<TKeyValue>()
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
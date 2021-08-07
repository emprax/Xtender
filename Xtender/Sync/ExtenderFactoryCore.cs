using System;
using System.Collections.Generic;

namespace Xtender.Sync
{
    /// <summary>
    /// The core of the extender-factory, providing the extender-cores needed to create extenders per key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key marked by the extender-factories.</typeparam>
    /// <typeparam name="TState">The type of the state marked by the extender-factories.</typeparam>
    public class ExtenderFactoryCore<TKey, TState> : ExtenderFactoryCore<TKey>, IExtenderFactoryCore<TKey, TState>
    {
        private readonly IDictionary<TKey, Func<IExtenderCore>> extenders;

        public ExtenderFactoryCore(IDictionary<TKey, Func<IExtenderCore>> extenders) : base(extenders) { }
    }

    /// <summary>
    /// The core of the extender-factory, providing the extender-cores needed to create extenders per key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key marked by the extender-factories.</typeparam>
    public class ExtenderFactoryCore<TKey> : IExtenderFactoryCore<TKey>
    {
        private readonly IDictionary<TKey, Func<IExtenderCore>> extenders;
        private static object lockSync = new();

        public ExtenderFactoryCore(IDictionary<TKey, Func<IExtenderCore>> extenders) => this.extenders = extenders;

        /// <summary>
        /// The method to actually get the extender-cores.
        /// </summary>
        /// <param name="key">The key from the factory presented by the user to get the right core for the right extender.</param>
        /// <returns>The requested core for the to-be-constructed extender.</returns>
        public Func<IExtenderCore> GetExtenderCoreFactory(TKey key)
        {
            lock (lockSync)
            {
                return this.extenders.TryGetValue(key, out var factory)
                    ? factory
                    : null;
            }
        }
    }
}
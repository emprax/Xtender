using System;

namespace Xtender.Async
{
    /// <summary>
    /// The core of the extender-factory, providing the extender-cores needed to create extenders per key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key marked by the extender-factories.</typeparam>
    /// <typeparam name="TState">The type of the state marked by the extender-factories.</typeparam>
    public interface IAsyncExtenderFactoryCore<in TKey, TState> : IAsyncExtenderFactoryCore<TKey> { }

    /// <summary>
    /// The core of the extender-factory, providing the extender-cores needed to create extenders per key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key marked by the extender-factories.</typeparam>
    public interface IAsyncExtenderFactoryCore<in TKey>
    {
        /// <summary>
        /// The method to actually get the extender-cores.
        /// </summary>
        /// <param name="key">The key from the factory presented by the user to get the right core for the right extender.</param>
        /// <returns>The requested core for the to-be-constructed extender.</returns>
        Func<IAsyncExtenderCore> GetExtenderCoreFactory(TKey key);
    }
}
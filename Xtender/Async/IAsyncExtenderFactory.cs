using System;
using Xtender.Sync;

namespace Xtender.Async
{
    /// <summary>
    /// The interface for implementing the AsyncExtenderFactory, a possible design choice to use a factory that creates the extenders based on stored AsyncExtenderCores.
    /// </summary>
    /// <typeparam name="TKey">Type of the search-key.</typeparam>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public interface IAsyncExtenderFactory<in TKey, TState>
    {
        /// <summary>
        /// The create method. Used to create the extenders by providing their cores retrieved from the store.
        /// </summary>
        /// <param name="key">The key used to search to find the right ExtnderCore in the key-value store.</param>
        /// <returns>The newly constructed extender.</returns>
        IAsyncExtender<TState> Create(TKey key);
    }

    /// <summary>
    /// The interface for implementing the AsyncExtenderFactory, a possible design choice to use a factory that creates the extenders based on stored AsyncExtenderCores.
    /// Notice that there is no state type.
    /// </summary>
    /// <typeparam name="TKey">Type of the search-key.</typeparam>
    public interface IAsyncExtenderFactory<in TKey>
    {
        /// <summary>
        /// The create method. Used to create the extenders by providing their cores retrieved from the store.
        /// </summary>
        /// <param name="key">The key used to search to find the right ExtnderCore in the key-value store.</param>
        /// <returns>The newly constructed extender.</returns>
        IAsyncExtender Create(TKey key);
    }
}

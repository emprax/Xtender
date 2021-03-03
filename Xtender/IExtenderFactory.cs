namespace Xtender
{
    /// <summary>
    /// The interface for implementing the ExtenderFactory, a possible design choice to use a factory that creates the extenders based on stored ExtenderCores.
    /// </summary>
    /// <typeparam name="TKey">Type of the search-key.</typeparam>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public interface IExtenderFactory<TKey, TState>
    {
        /// <summary>
        /// The create method. Used to create the extenders by providing their cores retrieved from the store.
        /// </summary>
        /// <param name="key">The key used to search to find the right ExtnderCore in the key-value store.</param>
        /// <returns>The newly constructed extender.</returns>
        IExtender<TState> Create(TKey key);
    }
}

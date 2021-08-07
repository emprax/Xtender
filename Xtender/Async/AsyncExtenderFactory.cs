using System;

namespace Xtender.Async
{
    /// <summary>
    /// The ExtenderFactory, a possible design choice to use a factory that creates the extenders based on stored ExtenderCores.
    /// </summary>
    /// <typeparam name="TKey">Type of the search-key.</typeparam>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public class AsyncExtenderFactory<TKey, TState> : IAsyncExtenderFactory<TKey, TState>
    {
        private readonly IAsyncExtenderFactoryCore<TKey, TState> extenders;
        private readonly Func<ServiceFactory> provider;

        public AsyncExtenderFactory(IAsyncExtenderFactoryCore<TKey, TState> extenders, Func<ServiceFactory> provider)
        {
            this.extenders = extenders;
            this.provider = provider;
        }

        /// <summary>
        /// The create method. Used to create the extenders by providing their cores retrieved from the store.
        /// </summary>
        /// <param name="key">The key used to search to find the right AsyncExtenderCore in the key-value store.</param>
        /// <returns>The newly constructed extender.</returns>
        public IAsyncExtender<TState> Create(TKey key)
        {
            var extenderCore = this.extenders
                .GetExtenderCoreFactory(key)?
                .Invoke();

            return extenderCore is IAsyncExtenderCore<TState> core
                ? new AsyncExtenderProxy<TState>(proxy => new AsyncExtender<TState>(core, proxy, this.provider.Invoke()))
                : null;
        }
    }

    /// <summary>
    /// The AsyncExtenderFactory, a possible design choice to use a factory that creates the extenders based on stored AsyncExtenderCores.
    /// </summary>
    /// <typeparam name="TKey">Type of the search-key.</typeparam>
    public class AsyncExtenderFactory<TKey> : IAsyncExtenderFactory<TKey>
    {
        private readonly IAsyncExtenderFactoryCore<TKey> extenders;
        private readonly Func<ServiceFactory> provider;

        public AsyncExtenderFactory(IAsyncExtenderFactoryCore<TKey> extenders, Func<ServiceFactory> provider)
        {
            this.extenders = extenders;
            this.provider = provider;
        }

        /// <summary>
        /// The create method. Used to create the extenders by providing their cores retrieved from the store.
        /// </summary>
        /// <param name="key">The key used to search to find the right AsyncExtenderCore in the key-value store.</param>
        /// <returns>The newly constructed extender.</returns>
        public IAsyncExtender Create(TKey key)
        {
            var extenderCore = this.extenders
                .GetExtenderCoreFactory(key)?
                .Invoke();

            return extenderCore is not null
                ? new AsyncExtenderProxy(proxy => new AsyncExtender(extenderCore, proxy, this.provider.Invoke()))
                : null;
        }
    }
}
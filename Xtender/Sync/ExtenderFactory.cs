using System;
using Xtender.Async;

namespace Xtender.Sync
{
    /// <summary>
    /// The ExtenderFactory, a possible design choice to use a factory that creates the extenders based on stored ExtenderCores.
    /// </summary>
    /// <typeparam name="TKey">Type of the search-key.</typeparam>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public class ExtenderFactory<TKey, TState> : IExtenderFactory<TKey, TState>
    {
        private readonly IExtenderFactoryCore<TKey, TState> extenders;
        private readonly Func<ServiceFactory> provider;

        public ExtenderFactory(IExtenderFactoryCore<TKey, TState> extenders, Func<ServiceFactory> provider)
        {
            this.extenders = extenders;
            this.provider = provider;
        }

        /// <summary>
        /// The create method. Used to create the extenders by providing their cores retrieved from the store.
        /// </summary>
        /// <param name="key">The key used to search to find the right ExtenderCore in the key-value store.</param>
        /// <returns>The newly constructed extender.</returns>
        public IExtender<TState> Create(TKey key)
        {
            var extenderCore = this.extenders
                .GetExtenderCoreFactory(key)?
                .Invoke();
            
            return extenderCore is IExtenderCore<TState> core
                ? new ExtenderProxy<TState>(proxy => new Extender<TState>(core, proxy, this.provider.Invoke()))
                : null;
        }
    }

    /// <summary>
    /// The ExtenderFactory, a possible design choice to use a factory that creates the extenders based on stored ExtenderCores.
    /// </summary>
    /// <typeparam name="TKey">Type of the search-key.</typeparam>
    public class ExtenderFactory<TKey> : IExtenderFactory<TKey>
    {
        private readonly IExtenderFactoryCore<TKey> extenders;
        private readonly Func<ServiceFactory> provider;

        public ExtenderFactory(IExtenderFactoryCore<TKey> extenders, Func<ServiceFactory> provider)
        {
            this.extenders = extenders;
            this.provider = provider;
        }

        /// <summary>
        /// The create method. Used to create the extenders by providing their cores retrieved from the store.
        /// </summary>
        /// <param name="key">The key used to search to find the right ExtenderCore in the key-value store.</param>
        /// <returns>The newly constructed extender.</returns>
        public IExtender Create(TKey key)
        {
            var extenderCore = this.extenders
                .GetExtenderCoreFactory(key)?
                .Invoke();

            return extenderCore is not null
                ? new ExtenderProxy(proxy => new Extender(extenderCore, proxy, this.provider.Invoke()))
                : null;
        }
    }
}

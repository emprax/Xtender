using System;
using System.Collections.Generic;

namespace Xtender
{
    /// <summary>
    /// The ExtenderFactory, a possible design choice to use a factory that creates the extenders based on stored ExtenderCores.
    /// </summary>
    /// <typeparam name="TKey">Type of the search-key.</typeparam>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public class ExtenderFactory<TKey, TState> : IExtenderFactory<TKey, TState>
    {
        private readonly IDictionary<TKey, Func<IExtenderCore<TState>>> extenders;

        public ExtenderFactory(IDictionary<TKey, Func<IExtenderCore<TState>>> extenders) => this.extenders = extenders;

        /// <summary>
        /// The create method. Used to create the extenders by providing their cores retrieved from the store.
        /// </summary>
        /// <param name="key">The key used to search to find the right ExtnderCore in the key-value store.</param>
        /// <returns>The newly constructed extender.</returns>
        public IExtender<TState> Create(TKey key)
        {
            if (!extenders.TryGetValue(key, out var factory))
            {
                return null;
            }
            
            var core = factory.Invoke();
            return new ExtenderProxy<TState>(proxy => new Extender<TState>(core.Provider, proxy));
        }
    }
}

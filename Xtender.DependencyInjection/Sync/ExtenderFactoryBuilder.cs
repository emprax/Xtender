using System;
using System.Collections.Generic;
using Xtender.Sync;

namespace Xtender.DependencyInjection.Sync
{
    internal class ExtenderFactoryBuilder<TKey, TState> : IExtenderFactoryBuilder<TKey, TState>
    {
        private readonly IDictionary<TKey, Func<IExtenderCore>> extenders;

        internal ExtenderFactoryBuilder(IDictionary<TKey, Func<IExtenderCore>> extenders) => this.extenders = extenders;

        public IExtenderFactoryBuilder<TKey, TState> WithExtender(TKey key, Action<IExtenderBuilder<TState>> configuration)
        {
            if (this.extenders.ContainsKey(key))
            {
                return this;
            }

            var cores = new Dictionary<string, Func<ServiceFactory, IExtensionBase>>();
            var builder = new ExtenderBuilder<TState>(cores);

            configuration.Invoke(builder);

            var results = cores.ToConcurrentDictionary();
            this.extenders.Add(key, () => new ExtenderCore<TState>(results));

            return this;
        }
    }

    internal class ExtenderFactoryBuilder<TKey> : IExtenderFactoryBuilder<TKey>
    {
        private readonly IDictionary<TKey, Func<IExtenderCore>> extenders;

        internal ExtenderFactoryBuilder(IDictionary<TKey, Func<IExtenderCore>> extenders) => this.extenders = extenders;

        public IExtenderFactoryBuilder<TKey> WithExtender(TKey key, Action<IExtenderBuilder> configuration)
        {
            if (this.extenders.ContainsKey(key))
            {
                return this;
            }

            var cores = new Dictionary<string, Func<ServiceFactory, IExtensionBase>>();
            var builder = new ExtenderBuilder(cores);

            configuration.Invoke(builder);

            var results = cores.ToConcurrentDictionary();
            this.extenders.Add(key, () => new ExtenderCore(results));

            return this;
        }
    }
}

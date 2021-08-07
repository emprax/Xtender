using System;
using System.Collections.Generic;
using Xtender.Async;

namespace Xtender.DependencyInjection.Async
{
    internal class AsyncExtenderFactoryBuilder<TKey, TState> : IAsyncExtenderFactoryBuilder<TKey, TState>
    {
        private readonly IDictionary<TKey, Func<IAsyncExtenderCore>> extenders;

        internal AsyncExtenderFactoryBuilder(IDictionary<TKey, Func<IAsyncExtenderCore>> extenders) => this.extenders = extenders;

        public IAsyncExtenderFactoryBuilder<TKey, TState> WithExtender(TKey key, Action<IAsyncExtenderBuilder<TState>> configuration)
        {
            if (this.extenders.ContainsKey(key))
            {
                return this;
            }

            var cores = new Dictionary<string, Func<ServiceFactory, IAsyncExtensionBase>>();
            var builder = new AsyncExtenderBuilder<TState>(cores);

            configuration.Invoke(builder);

            var results = cores.ToConcurrentDictionary();
            this.extenders.Add(key, () => new AsyncExtenderCore<TState>(results));

            return this;
        }
    }

    internal class AsyncExtenderFactoryBuilder<TKey> : IAsyncExtenderFactoryBuilder<TKey>
    {
        private readonly IDictionary<TKey, Func<IAsyncExtenderCore>> extenders;

        internal AsyncExtenderFactoryBuilder(IDictionary<TKey, Func<IAsyncExtenderCore>> extenders) => this.extenders = extenders;

        public IAsyncExtenderFactoryBuilder<TKey> WithExtender(TKey key, Action<IAsyncExtenderBuilder> configuration)
        {
            if (this.extenders.ContainsKey(key))
            {
                return this;
            }

            var cores = new Dictionary<string, Func<ServiceFactory, IAsyncExtensionBase>>();
            var builder = new AsyncExtenderBuilder(cores);

            configuration.Invoke(builder);

            var results = cores.ToConcurrentDictionary();
            this.extenders.Add(key, () => new AsyncExtenderCore(results));

            return this;
        }
    }
}

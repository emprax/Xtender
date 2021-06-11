using System;
using System.Collections.Generic;

namespace Xtender.DependencyInjection
{
    internal class ExtenderFactoryBuilder<TKey, TState> : IExtenderFactoryBuilder<TKey, TState>
    {
        private readonly IDictionary<TKey, Func<IExtenderCore<TState>>> extenders;
        private readonly IServiceProvider provider;

        internal ExtenderFactoryBuilder(IDictionary<TKey, Func<IExtenderCore<TState>>> extenders, IServiceProvider provider) 
        {
            this.extenders = extenders;
            this.provider = provider;
        }

        public IExtenderFactoryBuilder<TKey, TState> WithExtender(TKey key, Action<IExtenderBuilder<TState>> configuration)
        {
            if (this.extenders.ContainsKey(key))
            {
                return this;
            }

            var cores = new Dictionary<string, Func<IExtensionBase>>();
            var builder = new ExtenderBuilder<TState>(cores, this.provider);

            configuration.Invoke(builder);

            var results = cores.ToConcurrentDictionary();
            this.extenders.Add(key, () => new ExtenderCore<TState>(results));

            return this;
        }
    }

    internal class ExtenderFactoryBuilder<TKey> : IExtenderFactoryBuilder<TKey>
    {
        private readonly IDictionary<TKey, Func<IExtenderCore>> extenders;
        private readonly IServiceProvider provider;

        internal ExtenderFactoryBuilder(IDictionary<TKey, Func<IExtenderCore>> extenders, IServiceProvider provider)
        {
            this.extenders = extenders;
            this.provider = provider;
        }

        public IExtenderFactoryBuilder<TKey> WithExtender(TKey key, Action<IExtenderBuilder> configuration)
        {
            if (this.extenders.ContainsKey(key))
            {
                return this;
            }

            var cores = new Dictionary<string, Func<IExtensionBase>>();
            var builder = new ExtenderBuilder(cores, this.provider);

            configuration.Invoke(builder);

            var results = cores.ToConcurrentDictionary();
            this.extenders.Add(key, () => new ExtenderCore(results));

            return this;
        }
    }
}

using System;
using System.Collections.Concurrent;
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

            var cores = new ConcurrentDictionary<string, Func<object>>();
            configuration.Invoke(new ExtenderBuilder<TState>(cores, this.provider));

            this.extenders.Add(key, () => new ExtenderCore<TState>(cores));
            return this;
        }
    }
}

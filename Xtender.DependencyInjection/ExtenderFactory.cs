using System;
using System.Collections.Generic;

namespace Xtender.DependencyInjection
{
    internal class ExtenderFactory<TKey, TState> : IExtenderFactory<TKey, TState>
    {
        private readonly IDictionary<TKey, Func<IExtenderCore<TState>>> extenders;

        public ExtenderFactory(IDictionary<TKey, Func<IExtenderCore<TState>>> extenders) => this.extenders = extenders;

        public IExtender<TState> Create(TKey key) => extenders.TryGetValue(key, out var factory)
            ? new Extender<TState>(factory.Invoke().Provider)
            : null;
    }
}

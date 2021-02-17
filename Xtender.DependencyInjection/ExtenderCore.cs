using System;
using System.Collections.Generic;

namespace Xtender.DependencyInjection
{
    internal class ExtenderCore<TState> : IExtenderCore<TState>
    {
        internal ExtenderCore(IDictionary<string, Func<object>> provider) => this.Provider = provider;

        public IDictionary<string, Func<object>> Provider { get; }
    }
}

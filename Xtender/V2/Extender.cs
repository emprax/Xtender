using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xtender.V2
{
    public class Extender<TState> : IExtender<TState>
    {
        private readonly IDictionary<Type, IExtension> extensions;
        private readonly IExtension<object> defaultExtension;

        public Extender(Func<IExtender<TState>, IDictionary<Type, IExtension>> configuration, IExtension<object> defaultExtension)
        {
            this.extensions = configuration.Invoke(this);
            this.defaultExtension = defaultExtension;
        }

        public TState State { get; set; }

        public Task Extent<TAccepter>(TAccepter accepter) where TAccepter : IAccepter
        {
            if (!this.extensions.TryGetValue(typeof(TAccepter), out var segment) || !(segment is Extension<TState, TAccepter> extension))
            {
                return this.defaultExtension.Extent(accepter);
            }

            return extension.Extent(accepter);
        }
    }
}
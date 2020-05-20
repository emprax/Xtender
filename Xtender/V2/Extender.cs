using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xtender.V2
{
    public class Extender<TState> : IExtender<TState>
    {
        private readonly IDictionary<string, IExtension> extensions;
        private readonly IExtension<object> defaultExtension;

        public Extender(Func<IExtender<TState>, IDictionary<string, IExtension>> configuration, IExtension<object> defaultExtension)
        {
            this.extensions = configuration.Invoke(this);
            this.defaultExtension = defaultExtension;
        }

        public TState State { get; set; }

        public Task Extent<TAccepter>(TAccepter accepter) where TAccepter : IAccepter
        {
            var name = typeof(TAccepter).FullName;
            if (name is null)
            {
                return this.defaultExtension?.Extent(accepter) ?? Task.CompletedTask;
            }

            if (!this.extensions.TryGetValue(name, out var segment) || !(segment is IExtension<TAccepter> extension))
            {
                return this.defaultExtension?.Extent(accepter) ?? Task.CompletedTask;
            }

            return extension.Extent(accepter);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xtender
{
    public class Extender<TState> : IExtender<TState>
    {
        private readonly IDictionary<string, Func<object>> extensions;

        public Extender(IDictionary<string, Func<object>> extensions) => this.extensions = extensions;

        public TState State { get; set; }

        public Task Extent<TAccepter>(TAccepter accepter) where TAccepter : class, IAccepter
        {
            var name = typeof(TAccepter).FullName;
            if (name is null)
            {
                return this.UseDefault(accepter);
            }

            if (!this.extensions.TryGetValue(name, out var segment) || !(segment.Invoke() is IExtension<TState, TAccepter> extension))
            {
                return this.UseDefault(accepter);
            }

            return extension.Extent(accepter, this);
        }

        private Task UseDefault(object accepter)
        {
            return !this.extensions.TryGetValue(typeof(object).FullName, out var value) || !(value?.Invoke() is IExtension<TState, object> defaultExtension)
                ? throw new InvalidOperationException("Default extension could not by found.")
                : defaultExtension?.Extent(accepter, this) ?? Task.CompletedTask;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xtender
{
    /// <summary>
    /// The extender is the visitor itself and contains the extensions that have registered for it. The proxy-extender is used to be passed to the extensions, this enables extandability.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public class Extender<TState> : IExtender<TState>
    {
        private readonly IDictionary<string, Func<object>> extensions;
        private readonly IExtender<TState> proxy;

        public Extender(IDictionary<string, Func<object>> extensions, IExtender<TState> proxy)
        {
            this.extensions = extensions;
            this.proxy = proxy;
        }

        /// <summary>
        /// The preserved state that can be modified to store data requested for by multiple extensions or simply the result that is retieved when the extender done traversing the accepters.
        /// </summary>
        public TState State { get; set; }

        /// <summary>
        /// The visit method, here called Extent, to traverse and extend accepting object with some additional functionality.
        /// </summary>
        /// <typeparam name="TAccepter">The type of the accepter that implements the IAccepter interface.</typeparam>
        /// <param name="accepter">The accepter that implements the IAccepter interface.</param>
        /// <returns>Task.</returns>
        public Task Extend<TAccepter>(TAccepter accepter) where TAccepter : class, IAccepter
        {
            var type = typeof(TAccepter);
            var name = type?.FullName;

            if (string.IsNullOrWhiteSpace(name))
            {
                return this.UseDefault(accepter);
            }

            if (!this.extensions.TryGetValue(name, out var segment) || !(segment?.Invoke() is IExtension<TState, TAccepter> extension))
            {
                return this.UseDefault(accepter);
            }

            return extension.Extend(accepter, this.proxy);
        }

        private Task UseDefault(object accepter)
        {
            return !this.extensions.TryGetValue(typeof(object).FullName, out var value) || !(value?.Invoke() is IExtension<TState, object> defaultExtension)
                ? throw new InvalidOperationException("Default extension could not by found.")
                : defaultExtension?.Extend(accepter, this.proxy) ?? Task.CompletedTask;
        }
    }
}
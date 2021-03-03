using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xtender
{
    /// <summary>
    /// The extender is the visitor itself and contains the extensions that have registered for it.
    ///   - WARNING: Can be setup with an ExtenderAbstractionHandler, which enables the possiblity to visit the concrete implementation of an abstract class, however, it is not guaranteed to work without the right registration and possible eternal looping. Be careful with enabling this.
    ///   - IMPORTANT: The best practice is to start a process by inputting the extender into the accepter, so than the use for ExtenderAbstractionHandler is not needed at all.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public class Extender<TState> : IExtender<TState>
    {
        private readonly IDictionary<string, Func<object>> extensions;
        private readonly IExtenderAbstractionHandler<TState> handler;

        public Extender(IDictionary<string, Func<object>> extensions, IExtenderAbstractionHandler<TState> handler)
        {
            this.extensions = extensions;
            this.handler = handler;
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
        public async Task Extend<TAccepter>(TAccepter accepter) where TAccepter : class, IAccepter
        {
            var type = typeof(TAccepter);
            var name = type?.FullName;

            if (string.IsNullOrWhiteSpace(name))
            {
                await this.UseDefault(accepter);
                return;
            }

            if (!this.extensions.TryGetValue(name, out var segment) || !(segment?.Invoke() is IExtension<TState, TAccepter> extension))
            {
                var successful = await (handler.Handle(accepter, this, this.extensions) ?? Task.FromResult(false));
                if (successful) 
                {
                    return;
                }

                await this.UseDefault(accepter);
                return;
            }

            await extension.Extend(accepter, this);
        }

        private Task UseDefault(object accepter)
        {
            return !this.extensions.TryGetValue(typeof(object).FullName, out var value) || !(value?.Invoke() is IExtension<TState, object> defaultExtension)
                ? throw new InvalidOperationException("Default extension could not by found.")
                : defaultExtension?.Extend(accepter, this) ?? Task.CompletedTask;
        }
    }
}
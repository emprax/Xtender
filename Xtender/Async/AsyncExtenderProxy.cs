using System;
using System.Threading.Tasks;

namespace Xtender.Async
{
    /// <summary>
    /// The AsyncExtenderProxy class is a proxy for the inner Extender instance and ensures that the AsyncAccepter always first accepts the AsyncExtender before the Extender extends/visits the AsyncAccepter.
    /// The AsyncExtender implementation also has a reference to this proxy so that it calls this when passing it to the AsyncExtensions.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public class AsyncExtenderProxy<TState> : IAsyncExtender<TState>
    {
        private readonly IAsyncExtender<TState> extender;

        /// <summary>
        /// </summary>
        /// <param name="linker">This is a linking factory lambda. It ensures that the inner AsyncExtender is connected and provides the possibility to pass the proxy itself to the inner AsyncExtender to be reused.</param>
        public AsyncExtenderProxy(Func<IAsyncExtender<TState>, IAsyncExtender<TState>> linker)
            => this.extender = linker.Invoke(this);

        /// <summary>
        /// This state is a proxy state in that it refers directly to the inner AsyncExtender instance.
        /// </summary>
        public TState State
        {
            get => this.extender.State;
            set => this.extender.State = value;
        }

        /// <summary>
        /// The proxy Extend method. This method ensures that the AsyncAccepter is always first accepting the AsyncExtender before the AsyncExtender extends/visits the AsyncAccepter. But when the AsyncAccepter is not an interface or abstact class, it is directly passed to the internal AsyncExtender instance.
        /// </summary>
        /// <typeparam name="TAccepter">Type of the AsyncAccepter.</typeparam>
        /// <param name="accepter">The AsyncAccepter instance to be extended/visited.</param>
        /// <returns>Task.</returns>
        public Task Extend<TAccepter>(TAccepter accepter) where TAccepter : class, IAsyncAccepter
        {
            var type = typeof(TAccepter);

            return type.IsAbstract || type.IsInterface
                ? accepter?.Accept(this.extender) ?? Task.CompletedTask
                : this.extender.Extend(accepter);
        }

        /// <summary>
        /// The proxy Extend method. This method ensures that the AsyncAccepter is always first accepting the AsyncExtender before the AsyncExtender extends/visits the AsyncAccepter. But when the AsyncAccepter is not an interface or abstact class, it is directly passed to the internal AsyncExtender instance.
        /// </summary>
        /// <typeparam name="TValue">Type of the AsyncAccepter.</typeparam>
        /// <param name="accepter">The AsyncAccepter instance to be extended/visited.</param>
        /// <returns>Task.</returns>
        public Task Extend<TValue>(AsyncAccepter<TValue> accepter)
        {
            var type = typeof(TValue);

            return type.IsAbstract || type.IsInterface
                ? accepter?.Accept(this.extender) ?? Task.CompletedTask
                : this.extender.Extend(accepter);
        }
    }

    /// <summary>
    /// The AsyncExtenderProxy class is a proxy for the inner Extender instance and ensures that the AsyncAccepter always first accepts the Extender before the AsyncExtender extends/visits the AsyncAccepter.
    /// The AsyncExtender implementation also has a reference to this proxy so that it calls this when passing it to the AsyncExtensions.
    /// </summary>
    public class AsyncExtenderProxy : IAsyncExtender
    {
        private readonly IAsyncExtender extender;

        /// <summary>
        /// </summary>
        /// <param name="linker">This is a linking factory lambda. It ensures that the inner AsyncExtender is connected and provides the possibility to pass the proxy itself to the inner AsyncExtender to be reused.</param>
        public AsyncExtenderProxy(Func<IAsyncExtender, IAsyncExtender> linker)
            => this.extender = linker.Invoke(this);

        /// <summary>
        /// The proxy Extend method. This method ensures that the AsyncAccepter is always first accepting the AsyncExtender before the AsyncExtender extends/visits the AsyncAccepter. But when the AsyncAccepter is not an interface or abstact class, it is directly passed to the internal AsyncExtender instance.
        /// </summary>
        /// <typeparam name="TAccepter">Type of the AsyncAccepter.</typeparam>
        /// <param name="accepter">The AsyncAccepter instance to be extended/visited.</param>
        /// <returns>Task.</returns>
        public Task Extend<TAccepter>(TAccepter accepter) where TAccepter : class, IAsyncAccepter
        {
            var type = typeof(TAccepter);

            return type.IsAbstract || type.IsInterface
                ? accepter?.Accept(this.extender) ?? Task.CompletedTask
                : this.extender.Extend(accepter);
        }

        /// <summary>
        /// The proxy Extend method. This method ensures that the AsyncAccepter is always first accepting the AsyncExtender before the AsyncExtender extends/visits the AsyncAccepter. But when the AsyncAccepter is not an interface or abstact class, it is directly passed to the internal AsyncExtender instance.
        /// </summary>
        /// <typeparam name="TValue">Type of the AsyncAccepter.</typeparam>
        /// <param name="accepter">The AsyncAccepter instance to be extended/visited.</param>
        /// <returns>Task.</returns>
        public Task Extend<TValue>(AsyncAccepter<TValue> accepter)
        {
            var type = typeof(TValue);

            return type.IsAbstract || type.IsInterface
                ? accepter?.Accept(this.extender) ?? Task.CompletedTask
                : this.extender.Extend(accepter);
        }
    }
}
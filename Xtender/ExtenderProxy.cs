using System;
using System.Threading.Tasks;

namespace Xtender
{
    /// <summary>
    /// The ExtenderProxy class is a proxy for the inner Extender instance and ensures that the Accepter always first accepts the Extender before the Extender extends/visits the Accepter.
    /// The Extender implementation also has a reference to this proxy so that it calls this when passing it to the Extensions.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public class ExtenderProxy<TState> : IExtender<TState>
    {
        private readonly IExtender<TState> extender;

        /// <summary>
        /// </summary>
        /// <param name="linker">This is a linking factory lambda. It ensures that the inner Extender is connected and provides the possibility to pass the proxy itself to the inner Extender to be reused.</param>
        public ExtenderProxy(Func<IExtender<TState>, IExtender<TState>> linker)
            => this.extender = linker.Invoke(this);

        /// <summary>
        /// This state is a proxy state in that it refers directly to the inner Extender instance.
        /// </summary>
        public TState State
        {
            get => this.extender.State; 
            set => this.extender.State = value; 
        }

        /// <summary>
        /// The proxy Extend method. This method ensures that the Accepter is always first accepting the Extender before the Extender extends/visits the Accepter. But when the Accepter is not an interface or abstact class, it is directly passed to the internal Extender instance.
        /// </summary>
        /// <typeparam name="TAccepter">Type of the Accepter.</typeparam>
        /// <param name="accepter">The Accepter instance to be extended/visited.</param>
        /// <returns>Task.</returns>
        public Task Extend<TAccepter>(TAccepter accepter) where TAccepter : class, IAccepter
        { 
            var type = typeof(TAccepter);
            
            return type.IsAbstract || type.IsInterface
                ? accepter?.Accept(this.extender) ?? Task.CompletedTask
                : this.extender.Extend(accepter);
        }
    }
}

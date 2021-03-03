using System.Threading.Tasks;

namespace Xtender
{
    /// <summary>
    /// The interface for implementing the extender is the visitor itself and contains the extensions that have registered for it.
    ///   - WARNING: Can be setup with an ExtenderAbstractionHandler, which enables the possiblity to visit the concrete implementation of an abstract class, however, it is not guaranteed to work without the right registration and possible eternal looping. Be careful with enabling this.
    ///   - IMPORTANT: The best practice is to start a process by inputting the extender into the accepter, so than the use for ExtenderAbstractionHandler is not needed at all.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public interface IExtender<TState>
    {
        /// <summary>
        /// The preserved state that can be modified to store data requested for by multiple extensions or simply the result that is retieved when the extender done traversing the accepters.
        /// </summary>
        TState State { get; set; }

        /// <summary>
        /// The visit method, here called Extent, to traverse and extend accepting object with some additional functionality.
        /// </summary>
        /// <typeparam name="TAccepter">The type of the accepter that implements the IAccepter interface.</typeparam>
        /// <param name="accepter">The accepter that implements the IAccepter interface.</param>
        /// <returns>Task.</returns>
        Task Extend<TAccepter>(TAccepter accepter) where TAccepter : class, IAccepter;
    }
}
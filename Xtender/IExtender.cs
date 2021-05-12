﻿using System.Threading.Tasks;

namespace Xtender
{
    /// <summary>
    /// The interface for implementing the extender is the visitor itself and contains the extensions that have registered for it. A proxy-extender is used to be passed to the extensions, this enables extensibility.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public interface IExtender<TState> : IExtender
    {
        /// <summary>
        /// The preserved state that can be modified to store data requested for by multiple extensions or simply the result that is retieved when the extender done traversing the accepters.
        /// </summary>
        TState State { get; set; }
    }

    /// <summary>
    /// The interface for implementing the extender is the visitor itself and contains the extensions that have registered for it. A proxy-extender is used to be passed to the extensions, this enables extensibility.
    /// Notice that there is no state property. This extender is more lean.
    /// </summary>
    public interface IExtender
    {
        /// <summary>
        /// The visit method, here called Extent, to traverse and extend accepting object with some additional functionality.
        /// </summary>
        /// <typeparam name="TAccepter">The type of the accepter that implements the IAccepter interface.</typeparam>
        /// <param name="accepter">The accepter that implements the IAccepter interface.</param>
        /// <returns>Task.</returns>
        Task Extend<TAccepter>(TAccepter accepter) where TAccepter : class, IAccepter;
    }
}
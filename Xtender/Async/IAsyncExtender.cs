﻿using System.Threading.Tasks;

namespace Xtender.Async
{
    /// <summary>
    /// The interface for implementing the async version of the extender is the visitor itself and contains the extensions that have registered for it. A proxy-extender is used to be passed to the extensions, this enables extensibility.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public interface IAsyncExtender<TState> : IAsyncExtender
    {
        /// <summary>
        /// The preserved state that can be modified to store data requested for by multiple extensions or simply the result that is retieved when the extender done traversing the accepters.
        /// </summary>
        TState State { get; set; }
    }

    /// <summary>
    /// The interface for implementing the async version of the extender is the visitor itself and contains the extensions that have registered for it. A proxy-extender is used to be passed to the extensions, this enables extensibility.
    /// Notice that there is no state property. This extender is more lean.
    /// </summary>
    public interface IAsyncExtender
    {
        /// <summary>
        /// The visit method, here called Extend, to traverse and extend accepting object with some additional functionality.
        /// </summary>
        /// <typeparam name="TAccepter">The type of the accepter that implements the IAsyncAccepter interface.</typeparam>
        /// <param name="accepter">The accepter that implements the IAccepter interface.</param>
        /// <returns>Task.</returns>
        Task Extend<TAccepter>(TAccepter accepter) where TAccepter : class, IAsyncAccepter;

        /// <summary>
        /// The visit method, here called Extend, to traverse and extend accepting object with some additional functionality. This version is used with the accepter encapsulation functionality to extend the usage of the Extender to ordinary, non-IAccepter-implementing objects.
        /// </summary>
        /// <typeparam name="TValue">The type of the encapsulated object that does not implement an IAccepter interface within an accepter object.</typeparam>
        /// <param name="accepter">AsyncAccepter object that encapsulates a type that does not implement an IAsyncAccepter interface.</param>
        /// <returns>Task.</returns>
        Task Extend<TValue>(AsyncAccepter<TValue> accepter);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xtender
{
    /// <summary>
    /// The interface for implementing the ExtenderAbstractionHandler implementation.
    ///   - WARNING: This enables the possiblity to visit the concrete implementation of an abstract class, however, it is not guaranteed to work without the right registration and possible eternal looping. Be careful with enabling this.
    ///   - IMPORTANT: The best practice is to start a process by inputting the extender into the accepter, so than the use for ExtenderAbstractionHandler is not needed at all.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public interface IExtenderAbstractionHandler<TState>
    {
        Task<bool> Handle<TAccepter>(TAccepter accepter, IExtender<TState> extender, IDictionary<string, Func<object>> extensions) where TAccepter : class, IAccepter;
    }
}

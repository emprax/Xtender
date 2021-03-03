using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xtender
{
    /// <summary>
    /// The ExtenderAbstractionHandler implementation.
    ///   - WARNING: This enables the possiblity to visit the concrete implementation of an abstract class, however, it is not guaranteed to work without the right registration and possible eternal looping. Be careful with enabling this.
    ///   - IMPORTANT: The best practice is to start a process by inputting the extender into the accepter, so than the use for ExtenderAbstractionHandler is not needed at all.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class ExtenderAbstractionHandler<TState> : IExtenderAbstractionHandler<TState>
    {
        private readonly bool handleAbstractions;

        public ExtenderAbstractionHandler(bool handleAbstractions) => this.handleAbstractions = handleAbstractions;

        public async Task<bool> Handle<TAccepter>(TAccepter accepter, IExtender<TState> extender, IDictionary<string, Func<object>> extensions) where TAccepter : class, IAccepter
        {
            if (!this.handleAbstractions)
            {
                return false;
            }

            var type = typeof(TAccepter);
            if (type.IsInterface)
            {
                await accepter.Accept(extender);
                return true;
            }

            if (type.IsAbstract)
            {
                var method = type.GetMethod("Accept");
                var interfaceMethod = typeof(IAccepter).GetMethod("Accept");
                if (method.Name == interfaceMethod.Name && method.IsAbstract)
                {
                    await accepter.Accept(extender);
                    return true;
                }
            }

            return false;
        }
    }
}

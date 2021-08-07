namespace Xtender.Sync
{
    /// <summary>
    /// Accepter interface. Required for objects that need to be visitor by the extender and its extensions.
    /// </summary>
    public interface IAccepter
    {
        /// <summary>
        /// The Accept method which has to be implemented for each concrete type to offer it to the extender. The extender tries to find the right extension to handle the concrete implementation.
        /// Notice that this method does not explicitly support the state type.
        /// </summary>
        /// <param name="extender">The extender has to be inputted here. From here on out, the extender ensures to call the right extension to handle the concrete implementation.</param>
        void Accept(IExtender extender);
    }
}
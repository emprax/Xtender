namespace Xtender.DependencyInjection
{
    /// <summary>
    /// The first encountered builder which enforces to set the default Extension before attaching other Extensions.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public interface IExtenderBuilder<TState>
    {
        /// <summary>
        /// This method can be used to set a custom default Extension.
        /// </summary>
        /// <typeparam name="TDefaultExtension">Type of the default extension class.</typeparam>
        /// <returns>The next builder for registring the other Extensions.</returns>
        IConnectedExtenderBuilder<TState> Default<TDefaultExtension>() where TDefaultExtension : class, IExtension<TState, object>;

        /// <summary>
        /// To enable the use of the ExtenderAbstractionHandler.
        ///   - WARNING: This enables the possiblity to visit the concrete implementation of an abstract class, however, it is not guaranteed to work without the right registration and possible eternal looping. Be careful with enabling this.
        ///   - IMPORTANT: The best practice is to start a process by inputting the extender into the accepter, so than the use for ExtenderAbstractionHandler is not needed at all.
        /// </summary>
        /// <returns>This same builder.</returns>
        IExtenderBuilder<TState> WithAbstractAccepterHandling();

        /// <summary>
        /// The set the standard default Extension: DefaultExtension.
        /// </summary>
        /// <returns>The next builder for registring the other Extensions.</returns>
        IConnectedExtenderBuilder<TState> Default();
    }
}

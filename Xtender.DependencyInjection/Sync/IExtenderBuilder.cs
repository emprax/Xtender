using Xtender.Sync;

namespace Xtender.DependencyInjection.Sync
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
        IConnectedExtenderBuilder<TState> Default<TDefaultExtension>() where TDefaultExtension : class, IExtensionBase<object>;

        /// <summary>
        /// The set the standard default Extension: DefaultExtension.
        /// </summary>
        /// <returns>The next builder for registring the other Extensions.</returns>
        IConnectedExtenderBuilder<TState> Default();
    }

    /// <summary>
    /// The first encountered builder which enforces to set the default Extension before attaching other Extensions.
    /// Stateless version.
    /// </summary>
    public interface IExtenderBuilder
    {
        /// <summary>
        /// This method can be used to set a custom default Extension.
        /// </summary>
        /// <typeparam name="TDefaultExtension">Type of the default extension class.</typeparam>
        /// <returns>The next builder for registring the other Extensions.</returns>
        IConnectedExtenderBuilder Default<TDefaultExtension>() where TDefaultExtension : class, IExtension<object>;

        /// <summary>
        /// The set the standard default Extension: DefaultExtension.
        /// </summary>
        /// <returns>The next builder for registring the other Extensions.</returns>
        IConnectedExtenderBuilder Default();
    }
}

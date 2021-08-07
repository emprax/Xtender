using Xtender.Async;

namespace Xtender.DependencyInjection.Async
{
    /// <summary>
    /// The first encountered builder which enforces to set the default AsyncExtension before attaching other AsyncExtensions.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public interface IAsyncExtenderBuilder<TState>
    {
        /// <summary>
        /// This method can be used to set a custom default AsyncExtension.
        /// </summary>
        /// <typeparam name="TDefaultExtension">Type of the default extension class.</typeparam>
        /// <returns>The next builder for registring the other AsyncExtensions.</returns>
        IAsyncConnectedExtenderBuilder<TState> Default<TDefaultExtension>() where TDefaultExtension : class, IAsyncExtensionBase<object>;

        /// <summary>
        /// The set the standard default AsyncExtension: AsyncDefaultExtension.
        /// </summary>
        /// <returns>The next builder for registring the other AsyncExtensions.</returns>
        IAsyncConnectedExtenderBuilder<TState> Default();
    }

    /// <summary>
    /// The first encountered builder which enforces to set the default AsyncExtension before attaching other AsyncExtensions.
    /// Stateless version.
    /// </summary>
    public interface IAsyncExtenderBuilder
    {
        /// <summary>
        /// This method can be used to set a custom default AsyncExtension.
        /// </summary>
        /// <typeparam name="TDefaultExtension">Type of the default extension class.</typeparam>
        /// <returns>The next builder for registring the other AsyncExtensions.</returns>
        IAsyncConnectedExtenderBuilder Default<TDefaultExtension>() where TDefaultExtension : class, IAsyncExtension<object>;

        /// <summary>
        /// The set the standard default AsyncExtension: AsyncDefaultExtension.
        /// </summary>
        /// <returns>The next builder for registring the other AsyncExtensions.</returns>
        IAsyncConnectedExtenderBuilder Default();
    }
}

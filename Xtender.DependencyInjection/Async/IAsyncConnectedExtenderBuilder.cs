using System;
using Xtender.Async;

namespace Xtender.DependencyInjection.Async
{
    /// <summary>
    /// The builder to attach new AsyncExtensions (visitor-segments) to the AsyncExtender (visitor). The AsyncExtender is constructed in the internal implementation.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public interface IAsyncConnectedExtenderBuilder<TState>
    {
        /// <summary>
        /// The Attach method to attach an AsyncExtension to the AsyncExtender by means of a configuration lambda, which can be utilized to construct the AsyncExtension, for example, through DI service retrieval.
        /// </summary>
        /// <typeparam name="TContext">The concrete type for what the AsyncExtension is visiting/extending.</typeparam>
        /// <typeparam name="TExtension">The type argument for the AsyncExtension implementation itself.</typeparam>
        /// <param name="configuration">The configuration lambda to resolve an AsyncExtension.</param>
        /// <returns>This same builder.</returns>
        IAsyncConnectedExtenderBuilder<TState> Attach<TContext, TExtension>(Func<ServiceFactory, TExtension> configuration) where TExtension : class, IAsyncExtensionBase;

        /// <summary>
        /// The Attach method to attach an Extension to the Extender by means of the Extension type.
        /// </summary>
        /// <typeparam name="TContext">The concrete type for what the Extension is visiting/extending.</typeparam>
        /// <typeparam name="TExtension">The type argument for the Extension implementation itself.</typeparam>
        /// <returns>This same builder.</returns>
        IAsyncConnectedExtenderBuilder<TState> Attach<TContext, TExtension>() where TExtension : class, IAsyncExtensionBase;
    }

    /// <summary>
    /// The builder to attach new AsyncExtensions (visitor-segments) to the AsyncExtender (visitor). The AsyncExtender is constructed in the internal implementation.
    /// Stateless version.
    /// </summary>
    public interface IAsyncConnectedExtenderBuilder
    {
        /// <summary>
        /// The Attach method to attach an AsyncExtension to the Extender by means of a configuration lambda, which can be utilized to construct the AsyncExtension, for example, through DI service retrieval.
        /// </summary>
        /// <typeparam name="TContext">The concrete type for what the AsyncExtension is visiting/extending.</typeparam>
        /// <typeparam name="TExtension">The type argument for the Extension implementation itself.</typeparam>
        /// <param name="configuration">The configuration lambda to resolve an AsyncExtension.</param>
        /// <returns>This same builder.</returns>
        IAsyncConnectedExtenderBuilder Attach<TContext, TExtension>(Func<ServiceFactory, TExtension> configuration) where TExtension : class, IAsyncExtension<TContext>;

        /// <summary>
        /// The Attach method to attach an AsyncExtension to the AsyncExtender by means of the AsyncExtension type.
        /// </summary>
        /// <typeparam name="TContext">The concrete type for what the AsyncExtension is visiting/extending.</typeparam>
        /// <typeparam name="TExtension">The type argument for the AsyncExtension implementation itself.</typeparam>
        /// <returns>This same builder.</returns>
        IAsyncConnectedExtenderBuilder Attach<TContext, TExtension>() where TExtension : class, IAsyncExtension<TContext>;
    }
}

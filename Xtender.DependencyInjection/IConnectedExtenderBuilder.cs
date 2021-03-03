using System;

namespace Xtender.DependencyInjection
{
    /// <summary>
    /// The builder to attach new Extensions (visitor-segments) to the Extender (visitor). The Extender is constructed in the internal implementation.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public interface IConnectedExtenderBuilder<TState>
    {
        /// <summary>
        /// The Attach method to attach an Extension to the Extender by means of a configuration lambda, which can be utilized to construct the Extension, for example, through DI service retrieval.
        /// </summary>
        /// <typeparam name="TContext">The concrete type for what the Extension is visiting/extending.</typeparam>
        /// <typeparam name="TExtension">The type argument for the Extension implementation itself.</typeparam>
        /// <param name="configuration">The configuration lambda to resolve an Extension.</param>
        /// <returns>This same builder.</returns>
        IConnectedExtenderBuilder<TState> Attach<TContext, TExtension>(Func<TExtension> configuration) where TExtension : class, IExtension<TState, TContext>;

        /// <summary>
        /// The Attach method to attach an Extension to the Extender by means of the Extension type.
        /// </summary>
        /// <typeparam name="TContext">The concrete type for what the Extension is visiting/extending.</typeparam>
        /// <typeparam name="TExtension">The type argument for the Extension implementation itself.</typeparam>
        /// <returns>This same builder.</returns>
        IConnectedExtenderBuilder<TState> Attach<TContext, TExtension>() where TExtension : class, IExtension<TState, TContext>;
    }
}

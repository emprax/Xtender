using System;

namespace Xtender.DependencyInjection.Async
{
    /// <summary>
    /// The AsyncExtenderFactoryBuilder for building an AsyncExtenderFactory.
    /// </summary>
    /// <typeparam name="TKey">Type of the key that is used to identify a particular AsyncExtender.</typeparam>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public interface IAsyncExtenderFactoryBuilder<in TKey, TState>
    {
        /// <summary>
        /// The WithExtender method is used to register and setup a new AsyncExtender. Registered on the provided key.
        /// </summary>
        /// <param name="key">The key that is used to identify a particular AsyncExtender.</param>
        /// <param name="configuration">The configuration builder to setup the AsyncExtender.</param>
        /// <returns>This same builder.</returns>
        IAsyncExtenderFactoryBuilder<TKey, TState> WithExtender(TKey key, Action<IAsyncExtenderBuilder<TState>> configuration);
    }

    /// <summary>
    /// The AsyncExtenderFactoryBuilder for building an AsyncExtenderFactory.
    /// Stateless version.
    /// </summary>
    /// <typeparam name="TKey">Type of the key that is used to identify a particular Extender.</typeparam>
    public interface IAsyncExtenderFactoryBuilder<in TKey>
    {
        /// <summary>
        /// The WithExtender method is used to register and setup a new AsyncExtender. Registered on the provided key.
        /// </summary>
        /// <param name="key">The key that is used to identify a particular AsyncExtender.</param>
        /// <param name="configuration">The configuration builder to setup the AsyncExtender.</param>
        /// <returns>This same builder.</returns>
        IAsyncExtenderFactoryBuilder<TKey> WithExtender(TKey key, Action<IAsyncExtenderBuilder> configuration);
    }
}

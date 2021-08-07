using System;

namespace Xtender.DependencyInjection.Sync
{
    /// <summary>
    /// The ExtenderFactoryBuilder for building an ExtenderFactory.
    /// </summary>
    /// <typeparam name="TKey">Type of the key that is used to identify a particular Extender.</typeparam>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public interface IExtenderFactoryBuilder<in TKey, TState>
    {
        /// <summary>
        /// The WithExtender method is used to register and setup a new Extender. Registered on the provided key.
        /// </summary>
        /// <param name="key">The key that is used to identify a particular Extender.</param>
        /// <param name="configuration">The configuration builder to setup the Extender.</param>
        /// <returns>This same builder.</returns>
        IExtenderFactoryBuilder<TKey, TState> WithExtender(TKey key, Action<IExtenderBuilder<TState>> configuration);
    }

    /// <summary>
    /// The ExtenderFactoryBuilder for building an ExtenderFactory.
    /// Stateless version.
    /// </summary>
    /// <typeparam name="TKey">Type of the key that is used to identify a particular Extender.</typeparam>
    public interface IExtenderFactoryBuilder<in TKey>
    {
        /// <summary>
        /// The WithExtender method is used to register and setup a new Extender. Registered on the provided key.
        /// </summary>
        /// <param name="key">The key that is used to identify a particular Extender.</param>
        /// <param name="configuration">The configuration builder to setup the Extender.</param>
        /// <returns>This same builder.</returns>
        IExtenderFactoryBuilder<TKey> WithExtender(TKey key, Action<IExtenderBuilder> configuration);
    }
}

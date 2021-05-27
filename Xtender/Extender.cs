using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xtender
{
    /// <summary>
    /// The extender is the visitor itself and contains the extensions that have registered for it. The proxy-extender is used to be passed to the extensions, this enables extandability.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public class Extender<TState> : IExtender<TState>
    {
        private readonly IDictionary<string, Func<IExtensionBase>> extensions;
        private readonly IExtender<TState> proxy;

        public Extender(IDictionary<string, Func<IExtensionBase>> extensions, IExtender<TState> proxy)
        {
            this.extensions = extensions;
            this.proxy = proxy;
        }

        /// <summary>
        /// The preserved state that can be modified to store data requested for by multiple extensions or simply the result that is retieved when the extender done traversing the accepters.
        /// </summary>
        public TState State { get; set; }

        /// <summary>
        /// The visit method, here called Extend, to traverse and extend accepting object with some additional functionality.
        /// </summary>
        /// <typeparam name="TAccepter">The type of the accepter that implements the IAccepter interface.</typeparam>
        /// <param name="accepter">The accepter that implements the IAccepter interface.</param>
        /// <returns>Task.</returns>
        public Task Extend<TAccepter>(TAccepter accepter) where TAccepter : class, IAccepter
        {
            var type = typeof(TAccepter);
            var name = type.FullName;

            if (string.IsNullOrWhiteSpace(name) || !this.extensions.TryGetValue(name, out var segment))
            {
                return this.UseDefault(accepter);
            }

            var extensionBase = segment?.Invoke();
            switch (extensionBase)
            {
                case IExtension<TAccepter> extension: return extension.Extend(accepter, this.proxy);
                case IExtension<TState, TAccepter> statedExtension: return statedExtension.Extend(accepter, this.proxy);
                default: return this.UseDefault(accepter);
            }
        }

        /// <summary>
        /// The visit method, here called Extend, to traverse and extend accepting object with some additional functionality. This version is used with the accepter encapsulation functionality to extend the usage of the Extender to ordinary, non-IAccepter-implementing objects.
        /// </summary>
        /// <typeparam name="TValue">The type of the encapsulated object that does not implement an IAccepter interface within an accepter object.</typeparam>
        /// <param name="accepter">Accepter object that encapsulates a type that does not implement an IAccepter interface.</param>
        /// <returns>Task.</returns>
        public Task Extend<TValue>(Accepter<TValue> accepter)
        {
            var type = typeof(TValue);
            var name = type.FullName;

            var value = accepter is null ? default : accepter.Value;
            if (string.IsNullOrWhiteSpace(name) || !this.extensions.TryGetValue(name, out var segment))
            {
                return this.UseDefault(value);
            }

            var extensionBase = segment?.Invoke();
            switch (extensionBase)
            {
                case IExtension<TValue> extension: return extension.Extend(value, this.proxy);
                case IExtension<TState, TValue> statedExtension: return statedExtension.Extend(value, this.proxy);
                default: return this.UseDefault(value);
            }
        }

        private Task UseDefault(object accepter)
        {
            return !this.extensions.TryGetValue(typeof(object).FullName, out var value) || !(value?.Invoke() is IExtension<TState, object> defaultExtension)
                ? throw new InvalidOperationException("Default extension could not by found.")
                : defaultExtension.Extend(accepter, this.proxy) ?? Task.CompletedTask;
        }
    }

    public interface IAccepter<TValue> : IAccepter
    {
        TValue Value { get; }
    }

    /// <summary>
    /// The extender is the visitor itself and contains the extensions that have registered for it. The proxy-extender is used to be passed to the extensions, this enables extandability.
    /// </summary>
    public class Extender : IExtender
    {
        private readonly IDictionary<string, Func<IExtensionBase>> extensions;
        private readonly IExtender proxy;

        public Extender(IDictionary<string, Func<IExtensionBase>> extensions, IExtender proxy)
        {
            this.extensions = extensions;
            this.proxy = proxy;
        }
        
        /// <summary>
        /// The visit method, here called Extend, to traverse and extend accepting object with some additional functionality.
        /// </summary>
        /// <typeparam name="TAccepter">The type of the accepter that implements the IAccepter interface.</typeparam>
        /// <param name="accepter">The accepter that implements the IAccepter interface.</param>
        /// <returns>Task.</returns>
        public Task Extend<TAccepter>(TAccepter accepter) where TAccepter : class, IAccepter
        {
            var type = typeof(TAccepter);
            var name = type.FullName;

            if (string.IsNullOrWhiteSpace(name))
            {
                return this.UseDefault(accepter);
            }

            if (!this.extensions.TryGetValue(name, out var segment) || !(segment?.Invoke() is IExtension<TAccepter> extension))
            {
                return this.UseDefault(accepter);
            }

            return extension.Extend(accepter, this.proxy);
        }

        /// <summary>
        /// The visit method, here called Extend, to traverse and extend accepting object with some additional functionality. This version is used with the accepter encapsulation functionality to extend the usage of the Extender to ordinary, non-IAccepter-implementing objects.
        /// </summary>
        /// <typeparam name="TValue">The type of the encapsulated object that does not implement an IAccepter interface within an accepter object.</typeparam>
        /// <param name="accepter">Accepter object that encapsulates a type that does not implement an IAccepter interface.</param>
        /// <returns>Task.</returns>
        public Task Extend<TValue>(Accepter<TValue> accepter)
        {
            var type = typeof(TValue);
            var name = type.FullName;

            var value = accepter is null ? default : accepter.Value;
            if (string.IsNullOrWhiteSpace(name))
            {
                return this.UseDefault(value);
            }

            if (!this.extensions.TryGetValue(name, out var segment) || !(segment?.Invoke() is IExtension<TValue> extension))
            {
                return this.UseDefault(value);
            }

            return extension.Extend(value, this.proxy);
        }

        private Task UseDefault(object accepter)
        {
            return !this.extensions.TryGetValue(typeof(object).FullName, out var value) || !(value?.Invoke() is IExtension<object> defaultExtension)
                ? throw new InvalidOperationException("Default extension could not by found.")
                : defaultExtension.Extend(accepter, this.proxy) ?? Task.CompletedTask;
        }
    }
}
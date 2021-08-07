using System;
using System.Threading.Tasks;

namespace Xtender.Async
{
    /// <summary>
    /// The extender is the visitor itself and contains the extensions that have registered for it. The proxy-extender is used to be passed to the extensions, this enables extandability.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public class AsyncExtender<TState> : IAsyncExtender<TState>
    {
        private readonly IAsyncExtenderCore<TState> extensions;
        private readonly IAsyncExtender<TState> proxy;
        private readonly ServiceFactory factory;

        public AsyncExtender(IAsyncExtenderCore<TState> extensions, IAsyncExtender<TState> proxy, ServiceFactory factory)
        {
            this.extensions = extensions;
            this.proxy = proxy;
            this.factory = factory;
        }

        /// <summary>
        /// The preserved state that can be modified to store data requested for by multiple extensions or simply the result that is retieved when the extender done traversing the accepters.
        /// </summary>
        public TState State { get; set; }

        /// <summary>
        /// The visit method, here called Extend, to traverse and extend accepting object with some additional functionality.
        /// </summary>
        /// <typeparam name="TAccepter">The type of the accepter that implements the IAsyncAccepter interface.</typeparam>
        /// <param name="accepter">The accepter that implements the IAsyncAccepter interface.</param>
        /// <returns>Task.</returns>
        public Task Extend<TAccepter>(TAccepter accepter) where TAccepter : class, IAsyncAccepter
        {
            var type = typeof(TAccepter);
            var name = type.FullName;

            if (string.IsNullOrWhiteSpace(name))
            {
                return this.UseDefault(accepter);
            }

            var extensionBase = this.extensions
                .GetExtensionType<TAccepter>()?
                .Invoke(this.factory);

            return extensionBase switch
            {
                IAsyncExtension<TAccepter> extension => extension.Extend(accepter, this.proxy),
                IAsyncExtension<TState, TAccepter> statedExtension => statedExtension.Extend(accepter, this.proxy),
                IUnitExtension<TAccepter> extension => Task.Run(() => extension.Extend(accepter)),
                IUnitExtension<TState, TAccepter> extension => Task.Run(() => extension.Extend(accepter, this.State)),
                _ => this.UseDefault(accepter)
            };
        }

        /// <summary>
        /// The visit method, here called Extend, to traverse and extend accepting object with some additional functionality. This version is used with the accepter encapsulation functionality to extend the usage of the Extender to ordinary, non-IAccepter-implementing objects.
        /// </summary>
        /// <typeparam name="TValue">The type of the encapsulated object that does not implement an IAsyncAccepter interface within an accepter object.</typeparam>
        /// <param name="accepter">AsyncAccepter object that encapsulates a type that does not implement an IAsyncAccepter interface.</param>
        /// <returns>Task.</returns>
        public Task Extend<TValue>(AsyncAccepter<TValue> accepter)
        {
            var type = typeof(TValue);
            var name = type.FullName;

            var value = accepter is null ? default : accepter.Value;
            if (string.IsNullOrWhiteSpace(name))
            {
                return this.UseDefault(accepter);
            }

            var extensionBase = this.extensions
                .GetExtensionType<TValue>()?
                .Invoke(this.factory);

            return extensionBase switch
            {
                IAsyncExtension<TValue> extension => extension.Extend(value, this.proxy),
                IAsyncExtension<TState, TValue> statedExtension => statedExtension.Extend(value, this.proxy),
                IUnitExtension<TValue> extension => Task.Run(() => extension.Extend(value)),
                IUnitExtension<TState, TValue> extension => Task.Run(() => extension.Extend(value, this.State)),
                _ => this.UseDefault(value)
            };
        }

        private Task UseDefault(object accepter)
        {
            var extension = this.extensions
                .GetExtensionType<object>()?
                .Invoke(this.factory);

            return extension is not IAsyncExtension<TState, object> defaultExtension
                ? throw new InvalidOperationException("Default extension could not by found.")
                : defaultExtension.Extend(accepter, this.proxy) ?? Task.CompletedTask;
        }
    }

    /// <summary>
    /// The (async) extender is the visitor itself and contains the extensions that have registered for it. The proxy-extender is used to be passed to the extensions, this enables extandability.
    /// </summary>
    public class AsyncExtender : IAsyncExtender
    {
        private readonly IAsyncExtenderCore extensions;
        private readonly IAsyncExtender proxy;
        private readonly ServiceFactory factory;

        public AsyncExtender(IAsyncExtenderCore extensions, IAsyncExtender proxy, ServiceFactory factory)
        {
            this.extensions = extensions;
            this.proxy = proxy;
            this.factory = factory;
        }

        /// <summary>
        /// The visit method, here called Extend, to traverse and extend accepting object with some additional functionality.
        /// </summary>
        /// <typeparam name="TAccepter">The type of the accepter that implements the IAsyncAccepter interface.</typeparam>
        /// <param name="accepter">The accepter that implements the IAsyncAccepter interface.</param>
        /// <returns>Task.</returns>
        public Task Extend<TAccepter>(TAccepter accepter) where TAccepter : class, IAsyncAccepter
        {
            var type = typeof(TAccepter);
            var name = type.FullName;

            if (string.IsNullOrWhiteSpace(name))
            {
                return this.UseDefault(accepter);
            }

            var extensionBase = this.extensions
                .GetExtensionType<TAccepter>()?
                .Invoke(this.factory);

            return extensionBase switch
            {
                IAsyncExtension<TAccepter> extension => extension.Extend(accepter, this.proxy),
                IUnitExtension<TAccepter> extension => Task.Run(() => extension.Extend(accepter)),
                _ => this.UseDefault(accepter)
            };
        }

        /// <summary>
        /// The visit method, here called Extend, to traverse and extend accepting object with some additional functionality. This version is used with the accepter encapsulation functionality to extend the usage of the Extender to ordinary, non-IAccepter-implementing objects.
        /// </summary>
        /// <typeparam name="TValue">The type of the encapsulated object that does not implement an IAsyncAccepter interface within an accepter object.</typeparam>
        /// <param name="accepter">AsyncAccepter object that encapsulates a type that does not implement an IAsyncAccepter interface.</param>
        /// <returns>Task.</returns>
        public Task Extend<TValue>(AsyncAccepter<TValue> accepter)
        {
            var type = typeof(TValue);
            var name = type.FullName;

            var value = accepter is null ? default : accepter.Value;
            if (string.IsNullOrWhiteSpace(name))
            {
                return this.UseDefault(value);
            }

            var extensionBase = this.extensions
                .GetExtensionType<TValue>()?
                .Invoke(this.factory);

            return extensionBase switch
            {
                IAsyncExtension<TValue> extension => extension.Extend(value, this.proxy),
                IUnitExtension<TValue> extension => Task.Run(() => extension.Extend(value)),
                _ => this.UseDefault(value)
            };
        }

        private Task UseDefault(object accepter)
        {
            var extension = this.extensions
                .GetExtensionType<object>()?
                .Invoke(this.factory);

            return extension is not IAsyncExtension<object> defaultExtension
                ? throw new InvalidOperationException("Default extension could not by found.")
                : defaultExtension.Extend(accepter, this.proxy) ?? Task.CompletedTask;
        }
    }
}
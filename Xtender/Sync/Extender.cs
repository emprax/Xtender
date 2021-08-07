using System;

namespace Xtender.Sync
{
    /// <summary>
    /// The extender is the visitor itself and contains the extensions that have registered for it. The proxy-extender is used to be passed to the extensions, this enables extandability.
    /// </summary>
    /// <typeparam name="TState">Type of the visitor-state.</typeparam>
    public class Extender<TState> : IExtender<TState>
    {
        private readonly IExtenderCore<TState> extensions;
        private readonly IExtender<TState> proxy;
        private readonly ServiceFactory factory;

        public Extender(IExtenderCore<TState> extensions, IExtender<TState> proxy, ServiceFactory factory)
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
        /// <typeparam name="TAccepter">The type of the accepter that implements the IAccepter interface.</typeparam>
        /// <param name="accepter">The accepter that implements the IAccepter interface.</param>
        public void Extend<TAccepter>(TAccepter accepter) where TAccepter : class, IAccepter
        {
            var type = typeof(TAccepter);
            var name = type.FullName;

            if (string.IsNullOrWhiteSpace(name))
            {
                this.UseDefault(accepter);
                return;
            }

            var extensionBase = this.extensions
                .GetExtensionType<TAccepter>()?
                .Invoke(this.factory);

            switch (extensionBase)
            {
                case IExtension<TAccepter> extension: 
                    extension.Extend(accepter, this.proxy);
                    break;
                case IExtension<TState, TAccepter> statedExtension:
                    statedExtension.Extend(accepter, this.proxy);
                    break;
                case IUnitExtension<TAccepter> unitExtension:
                    unitExtension.Extend(accepter);
                    break;
                case IUnitExtension<TState, TAccepter> unitExtension:
                    unitExtension.Extend(accepter, this.State);
                    break;
                default:
                    this.UseDefault(accepter);
                    break;
            }
        }

        /// <summary>
        /// The visit method, here called Extend, to traverse and extend accepting object with some additional functionality. This version is used with the accepter encapsulation functionality to extend the usage of the Extender to ordinary, non-IAccepter-implementing objects.
        /// </summary>
        /// <typeparam name="TValue">The type of the encapsulated object that does not implement an IAccepter interface within an accepter object.</typeparam>
        /// <param name="accepter">Accepter object that encapsulates a type that does not implement an IAccepter interface.</param>
        public void Extend<TValue>(Accepter<TValue> accepter)
        {
            var type = typeof(TValue);
            var name = type.FullName;

            var value = accepter is null ? default : accepter.Value;
            if (string.IsNullOrWhiteSpace(name))
            {
                this.UseDefault(accepter);
                return;
            }

            var extensionBase = this.extensions
                .GetExtensionType<TValue>()?
                .Invoke(this.factory);
            
            switch (extensionBase)
            {
                case IExtension<TValue> extension:
                    extension.Extend(value, this.proxy);
                    break;
                case IExtension<TState, TValue> statedExtension:
                    statedExtension.Extend(value, this.proxy);
                    break;
                case IUnitExtension<TValue> unitExtension:
                    unitExtension.Extend(value);
                    break;
                case IUnitExtension<TState, TValue> unitExtension:
                    unitExtension.Extend(value, this.State);
                    break;
                default:
                    this.UseDefault(value);
                    break;
            }
        }

        private void UseDefault(object accepter)
        {
            var extension = this.extensions
                .GetExtensionType<object>()?
                .Invoke(this.factory);

            if (extension is not IExtension<TState, object> defaultExtension)
            {
                throw new InvalidOperationException("Default extension could not by found.");
            }
            
            defaultExtension.Extend(accepter, this.proxy);
        }
    }

    /// <summary>
    /// The extender is the visitor itself and contains the extensions that have registered for it. The proxy-extender is used to be passed to the extensions, this enables extandability.
    /// </summary>
    public class Extender : IExtender
    {
        private readonly IExtenderCore extensions;
        private readonly IExtender proxy;
        private readonly ServiceFactory factory;

        public Extender(IExtenderCore extensions, IExtender proxy, ServiceFactory factory)
        {
            this.extensions = extensions;
            this.proxy = proxy;
            this.factory = factory;
        }
        
        /// <summary>
        /// The visit method, here called Extend, to traverse and extend accepting object with some additional functionality.
        /// </summary>
        /// <typeparam name="TAccepter">The type of the accepter that implements the IAccepter interface.</typeparam>
        /// <param name="accepter">The accepter that implements the IAccepter interface.</param>
        /// <returns>Task.</returns>
        public void Extend<TAccepter>(TAccepter accepter) where TAccepter : class, IAccepter
        {
            var type = typeof(TAccepter);
            var name = type.FullName;

            if (string.IsNullOrWhiteSpace(name))
            {
                this.UseDefault(accepter);
                return;
            }

            var extensionBase = this.extensions
                .GetExtensionType<TAccepter>()?
                .Invoke(this.factory);

            switch (extensionBase)
            {
                case IExtension<TAccepter> extension:
                    extension.Extend(accepter, this.proxy);
                    break;
                case IUnitExtension<TAccepter> unitExtension:
                    unitExtension.Extend(accepter);
                    break;
                default:
                    this.UseDefault(accepter);
                    break;
            }
        }

        /// <summary>
        /// The visit method, here called Extend, to traverse and extend accepting object with some additional functionality. This version is used with the accepter encapsulation functionality to extend the usage of the Extender to ordinary, non-IAccepter-implementing objects.
        /// </summary>
        /// <typeparam name="TValue">The type of the encapsulated object that does not implement an IAccepter interface within an accepter object.</typeparam>
        /// <param name="accepter">Accepter object that encapsulates a type that does not implement an IAccepter interface.</param>
        /// <returns>Task.</returns>
        public void Extend<TValue>(Accepter<TValue> accepter)
        {
            var type = typeof(TValue);
            var name = type.FullName;

            var value = accepter is null ? default : accepter.Value;
            if (string.IsNullOrWhiteSpace(name))
            {
                this.UseDefault(value);
                return;
            }

            var extensionBase = this.extensions
                .GetExtensionType<TValue>()?
                .Invoke(this.factory);

            switch (extensionBase)
            {
                case IExtension<TValue> extension:
                    extension.Extend(value, this.proxy);
                    break;
                case IUnitExtension<TValue> unitExtension:
                    unitExtension.Extend(value);
                    break;
                default:
                    this.UseDefault(value);
                    break;
            }
        }

        private void UseDefault(object accepter)
        {
            var extension = this.extensions
                .GetExtensionType<object>()?
                .Invoke(this.factory);

            if (extension is not IExtension<object> defaultExtension)
            {
                throw new InvalidOperationException("Default extension could not by found.");
            }
            
            defaultExtension.Extend(accepter, this.proxy);
        }
    }
}
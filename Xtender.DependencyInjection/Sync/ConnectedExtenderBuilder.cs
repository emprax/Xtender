using System;
using System.Collections.Generic;
using System.Linq;
using Xtender.Sync;

namespace Xtender.DependencyInjection.Sync
{
    internal class ConnectedExtenderBuilder<TState> : IConnectedExtenderBuilder<TState>
    {
        private readonly IDictionary<string, Func<ServiceFactory, IExtensionBase>> extensions;

        internal ConnectedExtenderBuilder(IDictionary<string, Func<ServiceFactory, IExtensionBase>> extensions) => this.extensions = extensions;

        public IConnectedExtenderBuilder<TState> Attach<TContext, TExtension>(Func<ServiceFactory, TExtension> configuration) where TExtension : class, IExtensionBase
        {
            var key = typeof(TContext).FullName;
            if (!this.extensions.ContainsKey(key))
            {
                this.extensions.Add(key, configuration.Invoke);
            }

            return this;
        }

        public IConnectedExtenderBuilder<TState> Attach<TContext, TExtension>() where TExtension : class, IExtensionBase
        {
            var key = typeof(TContext).FullName;
            if (!this.extensions.ContainsKey(key))
            {
                this.extensions.Add(key, factory => 
                { 
                    var constructor = typeof(TExtension)
                        .GetConstructors()
                        .FirstOrDefault();

                    var parameters = constructor?
                        .GetParameters()
                        .Select(parameter => factory.Invoke(parameter.ParameterType))
                        .ToArray();

                    return constructor?.Invoke(parameters) as TExtension;
                });
            }

            return this;
        }
    }

    internal class ConnectedExtenderBuilder : IConnectedExtenderBuilder
    {
        private readonly IDictionary<string, Func<ServiceFactory, IExtensionBase>> extensions;

        internal ConnectedExtenderBuilder(IDictionary<string, Func<ServiceFactory, IExtensionBase>> extensions) => this.extensions = extensions;

        public IConnectedExtenderBuilder Attach<TContext, TExtension>(Func<ServiceFactory, TExtension> configuration) where TExtension : class, IExtension<TContext>
        {
            var key = typeof(TContext).FullName;
            if (!this.extensions.ContainsKey(key))
            {
                this.extensions.Add(key, configuration.Invoke);
            }

            return this;
        }

        public IConnectedExtenderBuilder Attach<TContext, TExtension>() where TExtension : class, IExtension<TContext>
        {
            var key = typeof(TContext).FullName;
            if (!this.extensions.ContainsKey(key))
            {
                this.extensions.Add(key, factory =>
                {
                    var constructor = typeof(TExtension)
                        .GetConstructors()
                        .FirstOrDefault();

                    var parameters = constructor?
                        .GetParameters()
                        .Select(parameter => factory.Invoke(parameter.ParameterType))
                        .ToArray();

                    return constructor?.Invoke(parameters) as TExtension;
                });
            }

            return this;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Xtender.Sync;

namespace Xtender.DependencyInjection.Sync
{
    internal class ExtenderBuilder<TState> : IExtenderBuilder<TState>
    {
        private readonly IDictionary<string, Func<ServiceFactory, IExtensionBase>> extensions;

        internal ExtenderBuilder(IDictionary<string, Func<ServiceFactory, IExtensionBase>> extensions) => this.extensions = extensions;

        public IConnectedExtenderBuilder<TState> Default<TDefaultExtension>() where TDefaultExtension : class, IExtensionBase<object>
        {
            var key = typeof(object).FullName;
            if (!this.extensions.ContainsKey(key))
            {
                this.extensions.Add(key, factory => 
                { 
                    var constructor = typeof(TDefaultExtension)
                        .GetConstructors()
                        .FirstOrDefault();

                    var parameters = constructor?
                        .GetParameters()
                        .Select(parameter => factory.Invoke(parameter?.ParameterType))
                        .ToArray();

                    return constructor?.Invoke(parameters) as TDefaultExtension;
                });
            }

            return new ConnectedExtenderBuilder<TState>(this.extensions);
        }

        public IConnectedExtenderBuilder<TState> Default()
        {
            this.extensions.Add(typeof(object).FullName, _ => new DefaultExtension<TState>());
            return new ConnectedExtenderBuilder<TState>(this.extensions);
        }
    }

    internal class ExtenderBuilder : IExtenderBuilder
    {
        private readonly IDictionary<string, Func<ServiceFactory, IExtensionBase>> extensions;

        internal ExtenderBuilder(IDictionary<string, Func<ServiceFactory, IExtensionBase>> extensions) => this.extensions = extensions;

        public IConnectedExtenderBuilder Default<TDefaultExtension>() where TDefaultExtension : class, IExtension<object>
        {
            var key = typeof(object).FullName;
            if (!this.extensions.ContainsKey(key))
            {
                this.extensions.Add(key, factory =>
                {
                    var constructor = typeof(TDefaultExtension)
                        .GetConstructors()
                        .FirstOrDefault();

                    var parameters = constructor?
                        .GetParameters()
                        .Select(parameter => factory.Invoke(parameter?.ParameterType))
                        .ToArray();

                    return constructor?.Invoke(parameters) as TDefaultExtension;
                });
            }

            return new ConnectedExtenderBuilder(this.extensions);
        }

        public IConnectedExtenderBuilder Default()
        {
            this.extensions.Add(typeof(object).FullName, _ => new DefaultExtension());
            return new ConnectedExtenderBuilder(this.extensions);
        }
    }
}
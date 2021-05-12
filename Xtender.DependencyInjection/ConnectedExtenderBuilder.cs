using System;
using System.Collections.Generic;
using System.Linq;

namespace Xtender.DependencyInjection
{
    internal class ConnectedExtenderBuilder<TState> : IConnectedExtenderBuilder<TState>
    {
        private readonly IDictionary<string, Func<IExtensionBase>> extensions;
        private readonly IServiceProvider provider;

        internal ConnectedExtenderBuilder(IDictionary<string, Func<IExtensionBase>> extensions, IServiceProvider provider)
        {
            this.extensions = extensions;
            this.provider = provider;
        }

        public IConnectedExtenderBuilder<TState> Attach<TContext, TExtension>(Func<TExtension> configuration) where TExtension : class, IExtensionBase
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
                this.extensions.Add(key, () => 
                { 
                    var constructor = typeof(TExtension)
                        .GetConstructors()
                        .FirstOrDefault();

                    var parameters = constructor?
                        .GetParameters()?
                        .Select(parameter => this.provider.GetService(parameter.ParameterType))?
                        .ToArray();

                    return constructor?.Invoke(parameters) as TExtension;
                });
            }

            return this;
        }
    }

    internal class ConnectedExtenderBuilder : IConnectedExtenderBuilder
    {
        private readonly IDictionary<string, Func<IExtensionBase>> extensions;
        private readonly IServiceProvider provider;

        internal ConnectedExtenderBuilder(IDictionary<string, Func<IExtensionBase>> extensions, IServiceProvider provider)
        {
            this.extensions = extensions;
            this.provider = provider;
        }

        public IConnectedExtenderBuilder Attach<TContext, TExtension>(Func<TExtension> configuration) where TExtension : class, IExtension<TContext>
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
                this.extensions.Add(key, () =>
                {
                    var constructor = typeof(TExtension)
                        .GetConstructors()
                        .FirstOrDefault();

                    var parameters = constructor?
                        .GetParameters()?
                        .Select(parameter => this.provider.GetService(parameter.ParameterType))?
                        .ToArray();

                    return constructor?.Invoke(parameters) as TExtension;
                });
            }

            return this;
        }
    }
}
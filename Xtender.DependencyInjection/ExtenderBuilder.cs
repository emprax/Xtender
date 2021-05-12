using System;
using System.Collections.Generic;
using System.Linq;

namespace Xtender.DependencyInjection
{
    internal class ExtenderBuilder<TState> : IExtenderBuilder<TState>
    {
        private readonly IDictionary<string, Func<IExtensionBase>> extensions;
        private readonly IServiceProvider provider;

        internal ExtenderBuilder(IDictionary<string, Func<IExtensionBase>> extensions, IServiceProvider provider)
        {
            this.extensions = extensions;
            this.provider = provider;
        }

        public IConnectedExtenderBuilder<TState> Default<TDefaultExtension>() where TDefaultExtension : class, IExtensionBase<object>
        {
            var key = typeof(object).FullName;
            if (!this.extensions.ContainsKey(key))
            {
                this.extensions.Add(key, () => 
                { 
                    var constructor = typeof(TDefaultExtension)
                        .GetConstructors()
                        .FirstOrDefault();

                    var parameters = constructor?
                        .GetParameters()?
                        .Select(parameter => this.provider.GetService(parameter?.ParameterType))?
                        .ToArray();

                    return constructor?.Invoke(parameters) as TDefaultExtension;
                });
            }

            return new ConnectedExtenderBuilder<TState>(this.extensions, this.provider);
        }

        public IConnectedExtenderBuilder<TState> Default()
        {
            this.extensions.Add(typeof(object).FullName, () => new DefaultExtension<TState>());
            return new ConnectedExtenderBuilder<TState>(this.extensions, this.provider);
        }
    }

    internal class ExtenderBuilder : IExtenderBuilder
    {
        private readonly IDictionary<string, Func<IExtensionBase>> extensions;
        private readonly IServiceProvider provider;

        internal ExtenderBuilder(IDictionary<string, Func<IExtensionBase>> extensions, IServiceProvider provider)
        {
            this.extensions = extensions;
            this.provider = provider;
        }

        public IConnectedExtenderBuilder Default<TDefaultExtension>() where TDefaultExtension : class, IExtension<object>
        {
            var key = typeof(object).FullName;
            if (!this.extensions.ContainsKey(key))
            {
                this.extensions.Add(key, () =>
                {
                    var constructor = typeof(TDefaultExtension)
                        .GetConstructors()
                        .FirstOrDefault();

                    var parameters = constructor?
                        .GetParameters()?
                        .Select(parameter => this.provider.GetService(parameter?.ParameterType))?
                        .ToArray();

                    return constructor?.Invoke(parameters) as TDefaultExtension;
                });
            }

            return new ConnectedExtenderBuilder(this.extensions, this.provider);
        }

        public IConnectedExtenderBuilder Default()
        {
            this.extensions.Add(typeof(object).FullName, () => new DefaultExtension());
            return new ConnectedExtenderBuilder(this.extensions, this.provider);
        }
    }
}
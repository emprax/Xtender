using System;
using System.Collections.Generic;
using System.Linq;

namespace Xtender.DependencyInjection
{
    internal class ExtenderBuilder<TState> : IExtenderBuilder<TState>
    {
        private readonly IDictionary<string, Func<object>> extensions;
        private readonly IServiceProvider provider;

        private bool handleAbstraction;

        internal ExtenderBuilder(IDictionary<string, Func<object>> extensions, IServiceProvider provider)
        {
            this.extensions = extensions;
            this.provider = provider;
            this.handleAbstraction = false;
        }

        public IConnectedExtenderBuilder<TState> Default<TDefaultExtension>() where TDefaultExtension : class, IExtension<TState, object>
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

        public IExtenderBuilder<TState> WithAbstractAccepterHandling()
        {
            this.handleAbstraction = true;
            return this;
        }

        internal IExtenderAbstractionHandler<TState> CreateAbstractHandler() => new ExtenderAbstractionHandler<TState>(this.handleAbstraction);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Xtender.Async;

namespace Xtender.DependencyInjection.Async
{
    internal class AsyncConnectedExtenderBuilder<TState> : IAsyncConnectedExtenderBuilder<TState>
    {
        private readonly IDictionary<string, Func<ServiceFactory, IAsyncExtensionBase>> extensions;

        internal AsyncConnectedExtenderBuilder(IDictionary<string, Func<ServiceFactory, IAsyncExtensionBase>> extensions) => this.extensions = extensions;

        public IAsyncConnectedExtenderBuilder<TState> Attach<TContext, TExtension>(Func<ServiceFactory, TExtension> configuration) where TExtension : class, IAsyncExtensionBase
        {
            var key = typeof(TContext).FullName;
            if (!this.extensions.ContainsKey(key))
            {
                this.extensions.Add(key, configuration.Invoke);
            }

            return this;
        }

        public IAsyncConnectedExtenderBuilder<TState> Attach<TContext, TExtension>() where TExtension : class, IAsyncExtensionBase
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

    internal class AsyncConnectedExtenderBuilder : IAsyncConnectedExtenderBuilder
    {
        private readonly IDictionary<string, Func<ServiceFactory, IAsyncExtensionBase>> extensions;

        internal AsyncConnectedExtenderBuilder(IDictionary<string, Func<ServiceFactory, IAsyncExtensionBase>> extensions) => this.extensions = extensions;

        public IAsyncConnectedExtenderBuilder Attach<TContext, TExtension>(Func<ServiceFactory, TExtension> configuration) where TExtension : class, IAsyncExtension<TContext>
        {
            var key = typeof(TContext).FullName;
            if (!this.extensions.ContainsKey(key))
            {
                this.extensions.Add(key, configuration.Invoke);
            }

            return this;
        }

        public IAsyncConnectedExtenderBuilder Attach<TContext, TExtension>() where TExtension : class, IAsyncExtension<TContext>
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
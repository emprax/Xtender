using System;
using System.Collections.Generic;
using System.Linq;
using Xtender.Async;

namespace Xtender.DependencyInjection.Async
{
    internal class AsyncExtenderBuilder<TState> : IAsyncExtenderBuilder<TState>
    {
        private readonly IDictionary<string, Func<ServiceFactory, IAsyncExtensionBase>> extensions;

        internal AsyncExtenderBuilder(IDictionary<string, Func<ServiceFactory, IAsyncExtensionBase>> extensions) => this.extensions = extensions;

        public IAsyncConnectedExtenderBuilder<TState> Default<TDefaultExtension>() where TDefaultExtension : class, IAsyncExtensionBase<object>
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

            return new AsyncConnectedExtenderBuilder<TState>(this.extensions);
        }

        public IAsyncConnectedExtenderBuilder<TState> Default()
        {
            this.extensions.Add(typeof(object).FullName, _ => new AsyncDefaultExtension<TState>());
            return new AsyncConnectedExtenderBuilder<TState>(this.extensions);
        }
    }

    internal class AsyncExtenderBuilder : IAsyncExtenderBuilder
    {
        private readonly IDictionary<string, Func<ServiceFactory, IAsyncExtensionBase>> extensions;

        internal AsyncExtenderBuilder(IDictionary<string, Func<ServiceFactory, IAsyncExtensionBase>> extensions) => this.extensions = extensions;

        public IAsyncConnectedExtenderBuilder Default<TDefaultExtension>() where TDefaultExtension : class, IAsyncExtension<object>
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

            return new AsyncConnectedExtenderBuilder(this.extensions);
        }

        public IAsyncConnectedExtenderBuilder Default()
        {
            this.extensions.Add(typeof(object).FullName, _ => new AsyncDefaultExtension());
            return new AsyncConnectedExtenderBuilder(this.extensions);
        }
    }
}
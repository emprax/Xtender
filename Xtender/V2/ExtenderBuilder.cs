using System;
using System.Collections.Generic;
using System.Linq;

namespace Xtender.V2
{
    public class ExtenderBuilder<TState> : IExtenderBuilder<TState>
    {
        private readonly IDictionary<Type, Func<IExtender<TState>, IExtension>> extensions;

        public ExtenderBuilder() => this.extensions = new Dictionary<Type, Func<IExtender<TState>, IExtension>>();

        public IExtenderBuilder<TState> Attach<TContext>(Func<IExtender<TState>, IExtension<TContext>> configuration)
        {
            this.extensions.Add(typeof(TContext), configuration);
            return this;
        }

        public IExtender<TState> Build(IExtension<object> defaultExtension)
        {
            return new Extender<TState>(extender => extensions.ToDictionary(
                x => x.Key,
                x => x.Value.Invoke(extender)),
                defaultExtension);
        }
    }
}
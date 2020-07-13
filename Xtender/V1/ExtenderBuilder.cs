using System;
using System.Collections.Generic;
using System.Linq;

namespace Xtender.V1
{
    public class ExtenderBuilder<TBaseValue, TState> : IExtenderBuilder<TBaseValue, TState> where TBaseValue : IAccepter<TBaseValue>
    {
        private readonly IList<Func<IExtender<TBaseValue, TState>, IExtension<TBaseValue>>> segmentConfigurations;

        public ExtenderBuilder() => segmentConfigurations = new List<Func<IExtender<TBaseValue, TState>, IExtension<TBaseValue>>>();

        public IExtenderBuilder<TBaseValue, TState> Attach(Func<IExtender<TBaseValue, TState>, IExtension<TBaseValue>> configuration)
        {
            this.segmentConfigurations.Add(configuration);
            return this;
        }

        public IExtender<TBaseValue, TState> Build()
        {
            return new Extender<TBaseValue, TState>(extender => this.segmentConfigurations
                .Select(segment => segment.Invoke(extender))
                .Aggregate((a, b) =>
                {
                    a.SetNext(b);
                    return a;
                }));
        }
    }
}
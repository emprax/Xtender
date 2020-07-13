using System;
using System.Threading.Tasks;

namespace Xtender.V1
{
    public class Extender<TBaseValue, TState> : IExtender<TBaseValue, TState> where TBaseValue : IAccepter<TBaseValue>
    {
        private readonly IExtension<TBaseValue> rootSegment;

        public Extender(Func<IExtender<TBaseValue, TState>, IExtension<TBaseValue>> configuration)
        {
            this.rootSegment = configuration.Invoke(this);
        }

        public TState State { get; set; }

        public Task Extent(TBaseValue value)
        {
            return this.rootSegment?.Extent(value) ?? Task.CompletedTask;
        }
    }
}
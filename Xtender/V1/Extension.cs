using System.Threading.Tasks;
using Xtender.V1;

namespace Xtender.v1
{
    public abstract class Extension<TState, TBaseValue, TContext> : IExtension<TBaseValue> where TBaseValue : IAccepter<TBaseValue>
    {
        protected readonly IExtender<TBaseValue, TState> extender;
        private IExtension<TBaseValue> next;

        protected Extension(IExtender<TBaseValue, TState> extender) => this.extender = extender;

        protected abstract Task Extent(TContext context);

        public Task Extent(TBaseValue value)
        {
            return value is TContext context 
                ? this.Extent(context)
                : this.next?.Extent(value) ?? Task.CompletedTask;
        }

        public void SetNext(IExtension<TBaseValue> segment)
        {
            if (this.next is null)
            {
                this.next = segment;
                return;
            }

            this.next.SetNext(segment);
        }
    }
}
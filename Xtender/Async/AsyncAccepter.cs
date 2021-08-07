using System.Threading.Tasks;

namespace Xtender.Async
{
    public class AsyncAccepter<TValue> : IAsyncAccepter
    {
        public AsyncAccepter(TValue value) => this.Value = value;

        public TValue Value { get; }

        public Task Accept(IAsyncExtender extender) => extender.Extend(this);
    }
}
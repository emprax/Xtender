using System.Threading.Tasks;

namespace Xtender
{
    public class Accepter<TValue> : IAccepter
    {
        public Accepter(TValue value) => this.Value = value;

        public TValue Value { get; }

        public Task Accept(IExtender extender) => extender.Extend(this);
    }
}

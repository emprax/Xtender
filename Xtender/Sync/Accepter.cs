namespace Xtender.Sync
{
    public class Accepter<TValue> : IAccepter
    {
        public Accepter(TValue value) => this.Value = value;

        public TValue Value { get; }

        public void Accept(IExtender extender) => extender.Extend(this);
    }
}

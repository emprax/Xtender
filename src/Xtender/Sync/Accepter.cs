namespace Xtender.Sync;

public class Accepter<TContext> : IAccepter
{
    public TContext Context { get; }

    public Accepter(TContext context) => this.Context = context;

    public void Accept(IExtender extender) => extender.Extend(this);
}

using System.Collections.Generic;

namespace Xtender.Sync;

internal class Extender(IDictionary<long, IExtension> extensions) : IExtender
{
    public void Extend<TAccepter>(Accepter<TAccepter> accepter) => this.ExtendBase(accepter.Context);

    public void Extend<TAccepter>(TAccepter accepter) where TAccepter : IAccepter => this.ExtendBase(accepter);

    private void ExtendBase<TAccepter>(TAccepter accepter)
    {
        var key = TypeId.Get<TAccepter>();
        if (extensions.TryGetValue(key, out var value) && value is IExtension<TAccepter> extension)
        {
            extension.Extend(accepter, new ExtenderProxy(this));
            return;
        }

        if (extensions.TryGetValue(-1, out var defaultValue) && defaultValue is IExtension<object> defaultExtension)
        {
            defaultExtension.Extend(accepter!, new ExtenderProxy(this));
        }
    }
}

internal class Extender<TState>(IDictionary<long, IExtension> extensions) : IExtender<TState>
{
    public TState? State { get; set; }

    public void Extend<TAccepter>(Accepter<TAccepter> accepter) => this.ExtendBase(accepter.Context);

    public void Extend<TAccepter>(TAccepter accepter) where TAccepter : IAccepter => this.ExtendBase(accepter);

    private void ExtendBase<TAccepter>(TAccepter accepter)
    {
        var key = TypeId.Get<TAccepter>();
        if (extensions.TryGetValue(key, out var value) && value is IExtension<TState, TAccepter> extension)
        {
            extension.Extend(accepter, new ExtenderProxy<TState>(this));
            return;
        }

        if (extensions.TryGetValue(-1, out var defaultValue) && defaultValue is IExtension<TState, object> defaultExtension)
        {
            defaultExtension.Extend(accepter!, new ExtenderProxy<TState>(this));
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xtender.Async;

internal class AsyncExtender(IDictionary<long, IAsyncExtension> extensions) : IAsyncExtender
{
    public Task Extend<TAccepter>(TAccepter accepter) where TAccepter : IAsyncAccepter => this.ExtendBase(accepter);

    public Task Extend<TAccepter>(AsyncAccepter<TAccepter> accepter) => this.ExtendBase(accepter.Context);

    private Task ExtendBase<TAccepter>(TAccepter accepter)
    {
        var key = TypeId.Get<TAccepter>();
        return extensions.TryGetValue(key, out var value) && value is IAsyncExtension<TAccepter> extension
            ? extension.Extend(accepter, new AsyncExtenderProxy(this))
            : extensions.TryGetValue(-1, out var defaultValue) && defaultValue is IAsyncExtension<object> defaultExtension
                ? defaultExtension.Extend(accepter!, new AsyncExtenderProxy(this))
                : Task.CompletedTask;
    }
}

internal class AsyncExtender<TState>(IDictionary<long, IAsyncExtension> extensions) : IAsyncExtender<TState>
{
    public TState? State { get; set; }

    public Task Extend<TAccepter>(TAccepter accepter) where TAccepter : IAsyncAccepter => this.ExtendBase(accepter);

    public Task Extend<TAccepter>(AsyncAccepter<TAccepter> accepter) => this.ExtendBase(accepter.Context);

    private Task ExtendBase<TAccepter>(TAccepter accepter)
    {
        var key = TypeId.Get<TAccepter>();
        return extensions.TryGetValue(key, out var value) && value is IAsyncExtension<TState, TAccepter> extension
            ? extension.Extend(accepter, new AsyncExtenderProxy<TState>(this))
            : extensions.TryGetValue(-1, out var defaultValue) && defaultValue is IAsyncExtension<TState, object> defaultExtension
                ? defaultExtension.Extend(accepter!, new AsyncExtenderProxy<TState>(this))
                : Task.CompletedTask;
    }
}

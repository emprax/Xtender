using System.Collections.Generic;

namespace Xtender.Async.Builders;

public class AsyncExtenderBuilder : IAsyncExtenderBuilder
{
    private readonly IDictionary<long, IAsyncExtension> extensions;

    public AsyncExtenderBuilder() : this(new Dictionary<long, IAsyncExtension>()) { }

    public AsyncExtenderBuilder(IDictionary<long, IAsyncExtension> extensions)
    {
        this.extensions = extensions;
        this.Default(new AsyncDefaultExtension());
    }

    public IAsyncExtender Build() => new AsyncExtenderProxy(new AsyncExtender(this.extensions.ToConcurrentDictionary()));

    public IAsyncExtenderBuilder Attach<TItem>(IAsyncExtension<TItem> extension)
    {
        var key = TypeId.Get<TItem>();
        if (!this.extensions.TryAdd(key, extension))
        {
            this.extensions[key] = extension;
        }

        return this;
    }

    public IAsyncExtenderBuilder Default(IAsyncExtension<object> extension)
    {
        if (!this.extensions.TryAdd(-1, extension))
        {
            this.extensions[-1] = extension;
        }

        return this;
    }
}

public class AsyncExtenderBuilder<TState> : IAsyncExtenderBuilder<TState>
{
    private readonly IDictionary<long, IAsyncExtension> extensions;

    public AsyncExtenderBuilder() : this(new Dictionary<long, IAsyncExtension>()) { }

    public AsyncExtenderBuilder(IDictionary<long, IAsyncExtension> extensions)
    {
        this.extensions = extensions;
        this.Default(new AsyncDefaultExtension<TState>());
    }

    public IAsyncExtender<TState> Build() => new AsyncExtenderProxy<TState>(new AsyncExtender<TState>(this.extensions.ToConcurrentDictionary()));

    public IAsyncExtenderBuilder<TState> Attach<TItem>(IAsyncExtension<TState, TItem> extension)
    {
        var key = TypeId.Get<TItem>();
        if (!this.extensions.TryAdd(key, extension))
        {
            this.extensions[key] = extension;
        }

        return this;
    }

    public IAsyncExtenderBuilder<TState> Default(IAsyncExtension<TState, object> extension)
    {
        if (!this.extensions.TryAdd(-1, extension))
        {
            this.extensions[-1] = extension;
        }

        return this;
    }
}

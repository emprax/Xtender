using System.Collections.Generic;

namespace Xtender.Sync.Builders;

public class ExtenderBuilder : IExtenderBuilder
{
    private readonly IDictionary<long, IExtension> extensions;

    public ExtenderBuilder() : this(new Dictionary<long, IExtension>()) { }

    public ExtenderBuilder(IDictionary<long, IExtension> extensions)
    {
        this.extensions = extensions;
        this.Default(new DefaultExtension());
    }

    public IExtender Build() => new ExtenderProxy(new Extender(this.extensions.ToConcurrentDictionary()));

    public IExtenderBuilder Attach<TItem>(IExtension<TItem> extension)
    {
        var key = TypeId.Get<TItem>();
        if (!this.extensions.TryAdd(key, extension))
        {
            this.extensions[key] = extension;
        }

        return this;
    }

    public IExtenderBuilder Default(IExtension<object> extension)
    {
        if (!this.extensions.TryAdd(-1, extension))
        {
            this.extensions[-1] = extension;
        }

        return this;
    }
}

public class ExtenderBuilder<TState> : IExtenderBuilder<TState>
{
    private readonly IDictionary<long, IExtension> extensions;

    public ExtenderBuilder() : this(new Dictionary<long, IExtension>()) { }

    public ExtenderBuilder(IDictionary<long, IExtension> extensions)
    {
        this.extensions = extensions;
        this.Default(new DefaultExtension<TState>());
    }

    public IExtender<TState> Build() => new ExtenderProxy<TState>(new Extender<TState>(this.extensions.ToConcurrentDictionary()));

    public IExtenderBuilder<TState> Attach<TItem>(IExtension<TState, TItem> extension)
    {
        var key = TypeId.Get<TItem>();
        if (!this.extensions.TryAdd(key, extension))
        {
            this.extensions[key] = extension;
        }

        return this;
    }

    public IExtenderBuilder<TState> Default(IExtension<TState, object> extension)
    {
        if (!this.extensions.TryAdd(-1, extension))
        {
            this.extensions[-1] = extension;
        }

        return this;
    }
}

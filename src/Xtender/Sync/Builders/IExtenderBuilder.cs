namespace Xtender.Sync.Builders;

public interface IExtenderBuilder
{
    IExtenderBuilder Attach<TItem>(IExtension<TItem> extension);

    IExtenderBuilder Default(IExtension<object> extension);

    IExtender Build();
}

public interface IExtenderBuilder<TState>
{
    IExtenderBuilder<TState> Attach<TItem>(IExtension<TState, TItem> extension);

    IExtenderBuilder<TState> Default(IExtension<TState, object> extension);

    IExtender<TState> Build();
}

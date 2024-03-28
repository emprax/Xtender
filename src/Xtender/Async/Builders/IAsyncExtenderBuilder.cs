namespace Xtender.Async.Builders;

public interface IAsyncExtenderBuilder
{
    IAsyncExtenderBuilder Attach<TItem>(IAsyncExtension<TItem> extension);

    IAsyncExtenderBuilder Default(IAsyncExtension<object> extension);

    IAsyncExtender Build();
}

public interface IAsyncExtenderBuilder<TState>
{
    IAsyncExtenderBuilder<TState> Attach<TItem>(IAsyncExtension<TState, TItem> extension);

    IAsyncExtenderBuilder<TState> Default(IAsyncExtension<TState, object> extension);

    IAsyncExtender<TState> Build();
}

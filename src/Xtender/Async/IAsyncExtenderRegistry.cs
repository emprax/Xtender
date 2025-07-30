namespace Xtender.Async;

public interface IAsyncExtenderRegistry
{
    bool TryGet(string key, out IAsyncExtender? extender);

    bool TryGet<TState>(string key, out IAsyncExtender<TState>? extender);
}

namespace Xtender.Sync;

public interface IExtenderRegistry
{
    bool TryGet(string key, out IExtender? extender);

    bool TryGet<TState>(string key, out IExtender<TState>? extender);
}

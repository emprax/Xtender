using System.Collections.Generic;

namespace Xtender.Async;

internal class AsyncExtenderRegistry(IDictionary<string, IAsyncExtender> extenders) : IAsyncExtenderRegistry
{
    public bool TryGet(string key, out IAsyncExtender? extender) => extenders.TryGetValue(key, out extender);

    public bool TryGet<TState>(string key, out IAsyncExtender<TState>? extender)
    {
        if (!extenders.TryGetValue(key, out var value) || value is not IAsyncExtender<TState> result)
        {
            extender = null;
            return false;
        }

        extender = result;
        return true;
    }
}

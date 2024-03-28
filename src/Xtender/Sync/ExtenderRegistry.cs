using System.Collections.Generic;

namespace Xtender.Sync;

internal class ExtenderRegistry(IDictionary<string, IExtender> extenders) : IExtenderRegistry
{
    public bool TryGet(string key, out IExtender? extender) => extenders.TryGetValue(key, out extender);

    public bool TryGet<TState>(string key, out IExtender<TState>? extender)
    {
        if (!extenders.TryGetValue(key, out var value) || value is not IExtender<TState> result)
        {
            extender = null;
            return false;
        }

        extender = result;
        return true;
    }
}

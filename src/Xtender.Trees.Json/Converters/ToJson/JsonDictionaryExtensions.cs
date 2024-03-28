using System.Collections.Generic;
using System.Text.Json.Nodes;
using Xtender.Olds.Sync;

namespace Xtender.Trees.Json.Converters.ToJson;

public static class JsonDictionaryExtensions
{
    public static IEnumerable<KeyValuePair<string, JsonNode>> ToJsonNodeDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> values, IExtender<JsonNode> extender)
        where TValue : class, IAccepter
    {
        foreach (var (key, value) in values)
        {
            value.Accept(extender);
            yield return new(key.ToString(), extender.State);
        }
    }
}
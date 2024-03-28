using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;

namespace Xtender.Trees.Json;

public static class DictionaryExtensions
{
    public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> values) where TKey : notnull => values.ToDictionary(x => x.Key, x => x.Value);

    public static bool TryGetValue<TValue>(this IReadOnlyDictionary<string, JsonNode> nodes, string key, out TValue node)
    {
        node = default;
        return nodes.TryGetValue(key, out var result) && (result?.AsValue().TryGetValue(out node) ?? false);
    }
}

using System.Text.Json;
using System.Text.Json.Nodes;
using Xtender.Trees.Abstractions.Converters;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json;

public class JsonNodeConverter<TId, TValue> : INodeConverter<TId, JsonObject>
    where TId : notnull
    where TValue : class
{
    public INode<TId> Convert(TId id, string partitionKey, JsonObject customObject)
    {
        var value = JsonSerializer.Deserialize<TValue>(customObject.ToJsonString());
        return new IdCollection<TId, TValue>(id, partitionKey, value);
    }
}
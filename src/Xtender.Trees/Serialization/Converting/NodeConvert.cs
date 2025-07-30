using System.Text.Json;
using System.Text.Json.Nodes;
using Xtender.Trees.Nodes;
using Xtender.Trees.Serialization.Abstractions.Converters;
using Xtender.Trees.Serialization.XML;

namespace Xtender.Trees.Serialization.Converting;

internal class NodeConvert<TId, TValue> : INodeConverter<TId>
    where TId : notnull
    where TValue : class
{
    public INode<TId> Convert(TId id, string partitionKey, JsonNode customObject)
    {
        var json = ObjectPropertyRegulator.RegulateProperties(customObject.ToJsonString()!, typeof(TValue));
        var value = JsonSerializer.Deserialize<TValue>(json);

        return new IdCollection<TId, TValue>(id, partitionKey, value!);
    }
}
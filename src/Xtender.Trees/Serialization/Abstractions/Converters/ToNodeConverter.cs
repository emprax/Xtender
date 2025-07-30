using System.Text.Json.Nodes;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Serialization.Abstractions.Converters;

public class ToNodeConverter<TId>(INodeConverterRegistry<TId> registry) : IToNodeConverter<TId> where TId : notnull
{
    public TId GetId(JsonNode node) => node["_id"]!.GetValue<TId>();

    public string GetPartitionKey(JsonNode node) => node["_partitionKey"]!.GetValue<string>();

    public JsonNode GetCustomObject(JsonNode node) => node["_customObject"]!.AsObject();

    public string GetType(JsonNode node) => node["_type"]!.GetValue<string>();

    public INode<TId> Convert(JsonNode node)
    {
        var id = this.GetId(node);
        var partitionKey = this.GetPartitionKey(node);
        var customObject = this.GetCustomObject(node);
        var type = this.GetType(node);

        return registry
            .Get(type)
            .Convert(id, partitionKey, customObject);
    }
}
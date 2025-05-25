using System.Text.Json.Nodes;
using Xtender.Trees.Serialization.Abstractions.Converters;

namespace Xtender.Trees.Serialization.Json;

public class JsonToNodeConverter<TId>(INodeConverterRegistry<TId, JsonObject> registry) : ToNodeConverter<TId, JsonObject>(registry) where TId : notnull
{
    public override JsonObject GetCustomObject(JsonObject node) => node["_customObject"].AsObject();

    public override TId GetId(JsonObject node) => node["_id"].GetValue<TId>();

    public override string GetPartitionKey(JsonObject node) => node["_partitionKey"].GetValue<string>();

    public override string GetType(JsonObject node) => node["_type"].GetValue<string>();
}
using System.Text.Json;
using System.Text.Json.Nodes;
using Xtender.Trees.Abstractions.Converters;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json;

//public delegate IMappingHandler<TId> MappingRegistry<TId>(IMappingHandlerRegistry<TId> registry);

//public class JsonMappingHandler<TId, TValue>(IMappingHandlerRegistry<TId> registry) : IMappingHandler<TId>
//    where TId : notnull
//    where TValue : class
//{
//    public INode<TId> Map(JsonObject json)
//    {
//        var id = json[NodePropertyConstants.Id].GetValue<TId>();
//        var type = json[NodePropertyConstants.Type].GetValue<string>();
//        var partitionKey = json[NodePropertyConstants.PartitionKey].GetValue<string>();
//        var value = json[NodePropertyConstants.Value].GetValue<TValue>();

//        return type switch
//        {
//            NodeTypes.Node => new Node<TId, TValue>(id, partitionKey, value),
//            NodeTypes.IdCollection => GetIdCollection(json, id, partitionKey, value),
//			NodeTypes.NodeCollection => this.GetNodeCollection(json, id, partitionKey, value),
//            _ => throw new KeyNotFoundException($"Could not find node-type for '{type}'.")
//        };
//	}

//    private static INode<TId> GetIdCollection(JsonObject json, TId id, string partitionKey, TValue value)
//    {
//        var children = GetIds(json[NodePropertyConstants.Children].AsArray());
//        return new IdCollection<TId, TValue>(id, partitionKey, value, children);
//    }

//    private INode<TId> GetNodeCollection(JsonObject json, TId id, string partitionKey, TValue value)
//    {
//        var children = this
//            .GetChildren(json[NodePropertyConstants.Children].AsObject())
//            .ToDictionary(x => x.Id, x => x);

//        return new NodeCollection<TId, TValue>(id, partitionKey, value, children);
//    }
	
//	private static IReadOnlyCollection<TId> GetIds(JsonArray array) => [.. array.Select(x => x.GetValue<TId>())];

//    private IEnumerable<INode<TId>> GetChildren(JsonObject json)
//    {
//        foreach (var (_, child) in json)
//        {
//            var type = child[NodePropertyConstants.Value][NodePropertyConstants.Type].GetValue<string>();
//            yield return registry[type].Map(child.AsObject());
//        }
//    }
//}

public class JsonToNodeConverter<TId>(INodeConverterRegistry<TId, JsonObject> registry) : ToNodeConverter<TId, JsonObject>(registry) where TId : JsonNode
{
    public override JsonObject GetCustomObject(JsonObject node) => node["_customObject"].AsObject();

    public override TId GetId(JsonObject node) => node["_id"].GetValue<TId>();

    public override string GetPartitionKey(JsonObject node) => node["_partitionKey"].GetValue<string>();

    public override string GetType(JsonObject node) => node["_type"].GetValue<string>();
}

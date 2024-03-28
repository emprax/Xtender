using System.Text.Json.Nodes;
using Xtender.Olds.Sync;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json.Converters.ToJson;

public class NodeCollectionToJsonExtension<TId> : IExtension<JsonNode, NodeCollection<TId>>
{
    public void Extend(NodeCollection<TId> context, IExtender<JsonNode> extender) => extender.State = new JsonObject
    {
        ["id"] = JsonValue.Create(context.Id),
        ["$type"] = JsonValue.Create(context.Type),
        ["partitionKey"] = JsonValue.Create(context.PartitionKey),
        ["children"] = new JsonObject(context.ToJsonNodeDictionary(extender)),
        ["properties"] = new JsonObject(context.Properties.ToJsonNodeDictionary(extender))
    };
}

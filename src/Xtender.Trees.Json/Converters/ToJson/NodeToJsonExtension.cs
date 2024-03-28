using System.Text.Json.Nodes;
using Xtender.Olds.Sync;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json.Converters.ToJson;

public class NodeToJsonExtension<TId> : IExtension<JsonNode, Node<TId>>
{
    public void Extend(Node<TId> context, IExtender<JsonNode> extender) => extender.State = new JsonObject
    {
        ["id"] = JsonValue.Create(context.Id),
        ["$type"] = JsonValue.Create(context.Type),
        ["partitionKey"] = JsonValue.Create(context.PartitionKey),
        ["properties"] = new JsonObject(context.Properties.ToJsonNodeDictionary(extender))
    };
}

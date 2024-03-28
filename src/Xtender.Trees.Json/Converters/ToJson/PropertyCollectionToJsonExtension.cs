using System.Text.Json.Nodes;
using Xtender.Olds.Sync;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json.Converters.ToJson;

public class PropertyCollectionToJsonExtension : IExtension<JsonNode, NodePropertyCollection>
{
    public void Extend(NodePropertyCollection context, IExtender<JsonNode> extender) => extender.State = new JsonObject
    {
        ["$type"] = JsonValue.Create(context.Type),
        ["properties"] = new JsonObject(context.ToJsonNodeDictionary(extender))
    };
}

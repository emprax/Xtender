using System.Text.Json.Nodes;
using Xtender.Olds.Sync;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json.Converters.ToJson;

public class PropertyToJsonExtension : IExtension<JsonNode, NodeProperty>
{
    public void Extend(NodeProperty context, IExtender<JsonNode> extender) => extender.State = new JsonObject
    {
        ["value"] = JsonValue.Create(context.Value),
        ["$type"] = JsonValue.Create(context.Type)
    };
}

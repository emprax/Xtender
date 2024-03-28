using System.Collections.Generic;
using System.Text.Json.Nodes;
using Xtender.Trees.Json.Converters.ToNode;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json.Converters.Extensions;

public class PropertyConverterExtension : IConverterExtension<INodeProperty>
{
    public INodeProperty Convert(string type, IReadOnlyDictionary<string, JsonNode> nodes) => nodes.TryGetValue<string>("value", out var value)
        ? new NodeProperty(value, type)
        : null;
}

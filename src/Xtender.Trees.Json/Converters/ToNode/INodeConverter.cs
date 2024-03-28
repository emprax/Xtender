using System.Collections.Generic;
using System.Text.Json.Nodes;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json.Converters.ToNode;

public interface INodeConverter<TId>
{
    INode<TId> ConvertNode(string type, IReadOnlyDictionary<string, JsonNode> nodes);

    INodeProperty ConvertProperty(string type, IReadOnlyDictionary<string, JsonNode> nodes);
}

using System.Text.Json.Nodes;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Serialization.Abstractions.Converters;

public interface IToNodeConverter<TId> where TId : notnull
{
    INode<TId> Convert(JsonNode node);
}

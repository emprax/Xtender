using System.Text.Json.Nodes;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Serialization.Abstractions.Converters;

public interface INodeConverter<TId> where TId : notnull
{
    INode<TId> Convert(TId id, string partitionKey, JsonNode customObject);
}

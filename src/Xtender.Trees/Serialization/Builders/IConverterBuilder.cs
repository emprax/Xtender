using System.Text.Json.Nodes;
using Xtender.Trees.Serialization.Abstractions.Converters;

namespace Xtender.Trees.Serialization.Builders;

public interface IConverterBuilder<TId> : IConverterBuildClient<TId> where TId : notnull
{
    INodeConverterClient<TId, JsonNode> Build();
}
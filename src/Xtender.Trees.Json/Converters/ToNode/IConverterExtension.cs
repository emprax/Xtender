using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace Xtender.Trees.Json.Converters.ToNode;

public interface IConverterExtension<TResult>
{
    TResult Convert(string partitionKey, IReadOnlyDictionary<string, JsonNode> nodes);
}
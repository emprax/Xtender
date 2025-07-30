using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace Xtender.Trees.Serialization.Abstractions.Converters;

public class FromNodeConversionState
{
    public FromNodeConversionState(TypeKeyProvider provider) => this.Provider = provider;

    public ICollection<JsonNode> Results { get; } = [];

    public TypeKeyProvider Provider { get; }
}

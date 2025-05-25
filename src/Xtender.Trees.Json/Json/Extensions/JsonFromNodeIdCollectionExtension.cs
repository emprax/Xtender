using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using Xtender.Sync;
using Xtender.Trees.Nodes;
using Xtender.Trees.Serialization.Abstractions.Converters;

namespace Xtender.Trees.Serialization.Json.Extensions;

public class JsonFromNodeIdCollectionExtension<TId, TValue> : IExtension<FromNodeConversionState<JsonObject>, IdCollection<TId, TValue>>
    where TId : notnull
    where TValue : class
{
    public void Extend(IdCollection<TId, TValue> context, IExtender<FromNodeConversionState<JsonObject>> extender)
    {
        var type = extender.State.Provider.Invoke(typeof(TValue));
        var children = context
            .Select(x => JsonValue.Create(x))
            .ToArray();

        extender.State.Results.Add(new JsonObject(new Dictionary<string, JsonNode>
        {
            ["_id"] = JsonValue.Create(context.Id),
            ["_partitionKey"] = JsonValue.Create(context.PartitionKey),
            ["_type"] = JsonValue.Create(type),
            ["_customObject"] = JsonNode.Parse(JsonSerializer.Serialize(context.Value)),
            ["_children"] = new JsonArray(children)
        }));
    }
}
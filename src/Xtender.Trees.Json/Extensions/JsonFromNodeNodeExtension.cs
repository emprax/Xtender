using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using Xtender.Sync;
using Xtender.Trees.Abstractions.Converters;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json.Extensions;

public class JsonFromNodeNodeExtension<TId, TValue> : IExtension<FromNodeConversionState<JsonObject>, Node<TId, TValue>>
    where TId : notnull
    where TValue : class
{
    public void Extend(Node<TId, TValue> context, IExtender<FromNodeConversionState<JsonObject>> extender)
    {
        var type = extender.State.Provider.Invoke(typeof(TValue));
        extender.State.Results.Add(new JsonObject(new Dictionary<string, JsonNode>
        {
            ["_id"] = JsonValue.Create(context.Id),
            ["_partitionKey"] = JsonValue.Create(context.PartitionKey),
            ["_type"] = JsonValue.Create(type),
            ["_customObject"] = JsonNode.Parse(JsonSerializer.Serialize(context.Value)),
            ["_children"] = new JsonArray()
        }));
    }
}

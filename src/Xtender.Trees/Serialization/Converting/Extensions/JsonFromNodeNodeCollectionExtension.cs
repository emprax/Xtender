using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using Xtender.Sync;
using Xtender.Trees.Nodes;
using Xtender.Trees.Serialization.Abstractions.Converters;

namespace Xtender.Trees.Serialization.Converting.Extensions;

internal class JsonFromNodeNodeCollectionExtension<TId, TValue> : IExtension<FromNodeConversionState, NodeCollection<TId, TValue>>
    where TId : notnull
    where TValue : class
{
    public void Extend(NodeCollection<TId, TValue> context, IExtender<FromNodeConversionState> extender)
    {
        var type = extender.State!.Provider.Invoke(typeof(TValue));
        var children = context
            .Select(x =>
            {
                x.Value.Accept(extender);
                return JsonValue.Create(x.Key);
            })
            .ToArray();

        extender.State.Results.Add(new JsonObject(new Dictionary<string, JsonNode?>
        {
            ["_id"] = JsonValue.Create(context.Id),
            ["_partitionKey"] = JsonValue.Create(context.PartitionKey),
            ["_type"] = JsonValue.Create(type),
            ["_customObject"] = JsonNode.Parse(JsonSerializer.Serialize(context.Value)),
            ["_children"] = new JsonArray(children)
        }));
    }
}

using System.Text.Json.Nodes;
using Xtender.Sync;
using Xtender.Trees.Nodes;
using Xtender.Trees.Serialization.Abstractions;
using Xtender.Trees.Serialization.Abstractions.Converters;
using Xtender.Trees.Serialization.Json.Extensions;

namespace Xtender.Trees.Serialization.Json;

public class JsonNodeConverterSchema<TId> : INodeConverterSchema<TId, JsonObject> where TId : notnull
{
    public INodeConverter<TId, JsonObject> CreateConverter<TValue>() where TValue : class => new JsonNodeConverter<TId, TValue>();

    public IToNodeConverter<TId, JsonObject> CreateFrom(INodeConverterRegistry<TId, JsonObject> registry) => new JsonToNodeConverter<TId>(registry);

    public IExtension<FromNodeConversionState<JsonObject>, IdCollection<TId, TValue>> GetIdCollectionExtension<TValue>() where TValue : class => new JsonFromNodeIdCollectionExtension<TId, TValue>();

    public IExtension<FromNodeConversionState<JsonObject>, NodeCollection<TId, TValue>> GetNodeCollectionExtension<TValue>() where TValue : class => new JsonFromNodeNodeCollectionExtension<TId, TValue>();

    public IExtension<FromNodeConversionState<JsonObject>, Node<TId, TValue>> GetNodeExtension<TValue>() where TValue : class => new JsonFromNodeNodeExtension<TId, TValue>();
}

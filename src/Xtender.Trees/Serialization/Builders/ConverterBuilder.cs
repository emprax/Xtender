using System.Collections.Generic;
using System.Text.Json.Nodes;
using Xtender.Sync.Builders;
using Xtender.Trees.Serialization.Abstractions;
using Xtender.Trees.Serialization.Abstractions.Converters;
using Xtender.Trees.Serialization.Json;

namespace Xtender.Trees.Serialization.Builders;

internal class ConverterBuilder<TId>(INodeConverterSchema<TId> schema) : IConverterBuilder<TId> where TId : notnull
{
    private readonly ExtenderBuilder<FromNodeConversionState> builder = new();
    private readonly Dictionary<string, INodeConverter<TId>> converters = [];
    private readonly Dictionary<string, string> keyMappings = [];

    public INodeConverterClient<TId, JsonNode> Build()
    {
        var converter = schema.CreateFrom(new NodeConverterRegistry<TId>(this.converters));
        return new JsonNodeConverterClient<TId>(this.builder.Build(), converter, this.keyMappings);
    }

    public IConverterBuilder<TId> Mapping<TValue>(string key) where TValue : class
    {
        this.converters.TryAdd(key, schema.CreateConverter<TValue>());
        this.keyMappings.TryAdd(typeof(TValue).FullName!, key);
        this.builder
            .Attach(schema.GetNodeExtension<TValue>())
            .Attach(schema.GetIdCollectionExtension<TValue>())
            .Attach(schema.GetNodeCollectionExtension<TValue>());

        return this;
    }
}
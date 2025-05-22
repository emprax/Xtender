using System.Collections.Generic;
using Xtender.Sync.Builders;
using Xtender.Trees.Abstractions;
using Xtender.Trees.Abstractions.Converters;

namespace Xtender.Trees.Builders;

public class ConverterBuilder<TId, TTransferObject>(INodeConverterSchema<TId, TTransferObject> schema) : IConverterBuilder<TId, TTransferObject>
    where TId : notnull
    where TTransferObject : class
{
    private readonly ExtenderBuilder<FromNodeConversionState<TTransferObject>> builder = new();
    private readonly Dictionary<string, INodeConverter<TId, TTransferObject>> converters = [];
    private readonly Dictionary<string, string> keyMappings = [];

    public INodeConverterClient<TId, TTransferObject> Build()
    {
        var converter = schema.CreateFrom(new NodeConverterRegistry<TId, TTransferObject>(this.converters));
        return new NodeConverterClient<TId, TTransferObject>(this.builder.Build(), converter, this.keyMappings);
    }

    public IConverterBuilder<TId, TTransferObject> Mapping<TValue>(string key) where TValue : class
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

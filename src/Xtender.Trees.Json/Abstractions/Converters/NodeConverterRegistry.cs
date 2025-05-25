using System.Collections.Generic;

namespace Xtender.Trees.Serialization.Abstractions.Converters;

public class NodeConverterRegistry<TId, TTransferObject> : INodeConverterRegistry<TId, TTransferObject>
    where TId : notnull
    where TTransferObject : class
{
    private readonly IReadOnlyDictionary<string, INodeConverter<TId, TTransferObject>> converters;

    public NodeConverterRegistry(IReadOnlyDictionary<string, INodeConverter<TId, TTransferObject>> converters) => this.converters = converters;

    public INodeConverter<TId, TTransferObject> Get(string key) => this.converters[key];
}
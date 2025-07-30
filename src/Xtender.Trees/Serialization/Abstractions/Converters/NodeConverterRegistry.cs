using System.Collections.Generic;

namespace Xtender.Trees.Serialization.Abstractions.Converters;

public class NodeConverterRegistry<TId> : INodeConverterRegistry<TId> where TId : notnull
{
    private readonly IReadOnlyDictionary<string, INodeConverter<TId>> converters;

    public NodeConverterRegistry(IReadOnlyDictionary<string, INodeConverter<TId>> converters) => this.converters = converters;

    public INodeConverter<TId> Get(string key) => this.converters[key];
}
using Xtender.Sync;
using Xtender.Trees.Nodes;
using Xtender.Trees.Serialization.Abstractions;
using Xtender.Trees.Serialization.Abstractions.Converters;
using Xtender.Trees.Serialization.Converting.Extensions;

namespace Xtender.Trees.Serialization.Converting;

internal class NodeConverterSchema<TId> : INodeConverterSchema<TId> where TId : notnull
{
    public INodeConverter<TId> CreateConverter<TValue>() where TValue : class => new NodeConvert<TId, TValue>();

    public IToNodeConverter<TId> CreateFrom(INodeConverterRegistry<TId> registry) => new ToNodeConverter<TId>(registry);

    public IExtension<FromNodeConversionState, IdCollection<TId, TValue>> GetIdCollectionExtension<TValue>() where TValue : class => new JsonFromNodeIdCollectionExtension<TId, TValue>();

    public IExtension<FromNodeConversionState, NodeCollection<TId, TValue>> GetNodeCollectionExtension<TValue>() where TValue : class => new JsonFromNodeNodeCollectionExtension<TId, TValue>();

    public IExtension<FromNodeConversionState, Node<TId, TValue>> GetNodeExtension<TValue>() where TValue : class => new JsonFromNodeNodeExtension<TId, TValue>();
}

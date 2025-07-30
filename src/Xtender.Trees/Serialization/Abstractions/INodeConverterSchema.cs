using Xtender.Sync;
using Xtender.Trees.Nodes;
using Xtender.Trees.Serialization.Abstractions.Converters;

namespace Xtender.Trees.Serialization.Abstractions;

internal interface INodeConverterSchema<TId> where TId : notnull
{
    INodeConverter<TId> CreateConverter<TValue>() where TValue : class;

    IExtension<FromNodeConversionState, Node<TId, TValue>> GetNodeExtension<TValue>() where TValue: class;

    IExtension<FromNodeConversionState, NodeCollection<TId, TValue>> GetNodeCollectionExtension<TValue>() where TValue : class;

    IExtension<FromNodeConversionState, IdCollection<TId, TValue>> GetIdCollectionExtension<TValue>() where TValue : class;

    IToNodeConverter<TId> CreateFrom(INodeConverterRegistry<TId> registry);
}
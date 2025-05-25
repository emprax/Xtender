using Xtender.Sync;
using Xtender.Trees.Nodes;
using Xtender.Trees.Serialization.Abstractions.Converters;

namespace Xtender.Trees.Serialization.Abstractions;

public interface INodeConverterSchema<TId, TTransferObject>
    where TId : notnull
    where TTransferObject : class
{
    INodeConverter<TId, TTransferObject> CreateConverter<TValue>() where TValue : class;

    IExtension<FromNodeConversionState<TTransferObject>, Node<TId, TValue>> GetNodeExtension<TValue>() where TValue: class;

    IExtension<FromNodeConversionState<TTransferObject>, NodeCollection<TId, TValue>> GetNodeCollectionExtension<TValue>() where TValue : class;

    IExtension<FromNodeConversionState<TTransferObject>, IdCollection<TId, TValue>> GetIdCollectionExtension<TValue>() where TValue : class;

    IToNodeConverter<TId, TTransferObject> CreateFrom(INodeConverterRegistry<TId, TTransferObject> registry);
}
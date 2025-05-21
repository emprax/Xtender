using Xtender.Trees.Nodes;

namespace Xtender.Trees.Abstractions.Converters;

public interface INodeConverter<TId, TTransferObject>
    where TId : notnull
    where TTransferObject : class
{
    INode<TId> Convert(TId id, string partitionKey, TTransferObject customObject);
}

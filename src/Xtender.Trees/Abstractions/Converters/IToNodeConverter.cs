using Xtender.Trees.Nodes;

namespace Xtender.Trees.Abstractions.Converters;

public interface IToNodeConverter<TId, TTransferObject>
    where TId : notnull
    where TTransferObject : class
{
    INode<TId> Convert(TTransferObject node);
}

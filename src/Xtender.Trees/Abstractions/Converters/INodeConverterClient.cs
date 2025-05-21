using System.Collections.Generic;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Abstractions.Converters;

public interface INodeConverterClient<TId, TTransferObject>
    where TId : notnull
    where TTransferObject : class
{
    ICollection<TTransferObject> FromNode(INode<TId> node);

    INode<TId> ToNode(TTransferObject node);
}
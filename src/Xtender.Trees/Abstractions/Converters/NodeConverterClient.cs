using System;
using System.Collections.Generic;
using Xtender.Sync;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Abstractions.Converters;

public class NodeConverterClient<TId, TTransferObject> : INodeConverterClient<TId, TTransferObject>
    where TId : notnull
    where TTransferObject : class
{
    private readonly IExtender<FromNodeConversionState<TTransferObject>> extender;

    public NodeConverterClient(IExtender<FromNodeConversionState<TTransferObject>> extender) => this.extender = extender;

    public ICollection<TTransferObject> FromNode(INode<TId> node)
    {
        this.extender.State = new();
        node.Accept(this.extender);

        return this.extender.State.Results;
    }

    public INode<TId> ToNode(TTransferObject node) => throw new NotImplementedException();
}

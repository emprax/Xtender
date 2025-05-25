using System.Collections.Generic;
using Xtender.Sync;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Serialization.Abstractions.Converters;

public class NodeConverterClient<TId, TTransferObject> : INodeConverterClient<TId, TTransferObject>
    where TId : notnull
    where TTransferObject : class
{
    private readonly IExtender<FromNodeConversionState<TTransferObject>> extender;
    private readonly IToNodeConverter<TId, TTransferObject> converter;
    private readonly IDictionary<string, string> keys;

    public NodeConverterClient(
        IExtender<FromNodeConversionState<TTransferObject>> extender,
        IToNodeConverter<TId, TTransferObject> converter,
        IDictionary<string, string> keys)
    {
        this.extender = extender;
        this.converter = converter;
        this.keys = keys;
    }

    public INode<TId> ToNode(TTransferObject node) => this.converter.Convert(node);

    public ICollection<TTransferObject> FromNode(INode<TId> node)
    {
        this.extender.State = new(x => this.keys[x.GetType().FullName!]);
        node.Accept(this.extender);

        return this.extender.State.Results;
    }
}

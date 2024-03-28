using System.Collections.Generic;
using System.Text.Json.Nodes;
using Xtender.Trees.Json.Converters.ToNode;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json.Converters.Extensions.Overridables;

public abstract class CustomNodeConverter<TId, TNode> : IConverterExtension<INode<TId>> where TNode : Node<TId>
{
    private readonly NodeConverterExtension<TId> converter;

    protected CustomNodeConverter(INodeConverter<TId> converter) => this.converter = new(converter);

    public INode<TId> Convert(string type, IReadOnlyDictionary<string, JsonNode> nodes)
    {
        var node = this.converter.Convert(type, nodes);
        return this.ConvertExtended((TNode)node, nodes);
    }

    protected abstract TNode ConvertExtended(TNode node, IReadOnlyDictionary<string, JsonNode> nodes);
}

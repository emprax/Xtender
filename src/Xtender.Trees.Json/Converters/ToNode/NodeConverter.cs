using System.Collections.Generic;
using System.Text.Json.Nodes;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json.Converters.ToNode;

public delegate IConverterExtension<TResult> ConverterExtension<TId, TResult>(INodeConverter<TId> converter);

internal class NodeConverter<TId> : INodeConverter<TId>
{
    private readonly IReadOnlyDictionary<string, ConverterExtension<TId, INode<TId>>> convertors;
    private readonly IReadOnlyDictionary<string, ConverterExtension<TId, INodeProperty>> propertyConvertors;

    public NodeConverter(
        IReadOnlyDictionary<string, ConverterExtension<TId, INode<TId>>> convertors,
        IReadOnlyDictionary<string, ConverterExtension<TId, INodeProperty>> propertyConvertors)
    {
        this.convertors = convertors;
        this.propertyConvertors = propertyConvertors;
    }

    public INode<TId> ConvertNode(string type, IReadOnlyDictionary<string, JsonNode> nodes) => this.convertors.TryGetValue(type, out var convertor)
        ? convertor.Invoke(this).Convert(type, nodes)
        : null;

    public INodeProperty ConvertProperty(string type, IReadOnlyDictionary<string, JsonNode> nodes) => this.propertyConvertors.TryGetValue(type, out var convertor)
        ? convertor.Invoke(this).Convert(type, nodes)
        : null;
}
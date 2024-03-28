using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xtender.Trees.Json.Converters.ToNode;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json.Builders;

internal class NodeConverterBuilder<TId> : INodeConverterBuilder<TId>
{
    private readonly IDictionary<string, ConverterExtension<TId, INode<TId>>> nodeConverters;
    private readonly IDictionary<string, ConverterExtension<TId, INodeProperty>> propertyConverters;

    public NodeConverterBuilder(
        IDictionary<string, ConverterExtension<TId, INode<TId>>> nodeConverters,
        IDictionary<string, ConverterExtension<TId, INodeProperty>> propertyConverters)
    {
        this.nodeConverters = nodeConverters;
        this.propertyConverters = propertyConverters;
    }

    public INodeConverterBuilder<TId> WithNodeConverter(ConverterExtension<TId, INode<TId>> extension)
    {
        this.nodeConverters["node"] = extension;
        return this;
    }

    public INodeConverterBuilder<TId> WithNodeCollectionConverter(ConverterExtension<TId, INode<TId>> extension)
    {
        this.nodeConverters["node-collection"] = extension;
        return this;
    }

    public INodeConverterBuilder<TId> WithPropertiesNodeConverter(ConverterExtension<TId, INode<TId>> extension)
    {
        this.nodeConverters["properties-node"] = extension;
        return this;
    }

    public INodeConverterBuilder<TId> WithPropertiesNodeCollectionConverter(ConverterExtension<TId, INode<TId>> extension)
    {
        this.nodeConverters["properties-node-collection"] = extension;
        return this;
    }

    public INodeConverterBuilder<TId> WithPropertyConverter(ConverterExtension<TId, INodeProperty> extension)
    {
        this.propertyConverters["property"] = extension;
        return this;
    }

    public INodeConverterBuilder<TId> WithPropertyCollectionConverter(ConverterExtension<TId, INodeProperty> extension)
    {
        this.propertyConverters["property-collection"] = extension;
        return this;
    }

    internal INodeConverter<TId> Build() => new NodeConverter<TId>(
        new ReadOnlyDictionary<string, ConverterExtension<TId, INode<TId>>>(this.nodeConverters),
        new ReadOnlyDictionary<string, ConverterExtension<TId, INodeProperty>>(this.propertyConverters));
}

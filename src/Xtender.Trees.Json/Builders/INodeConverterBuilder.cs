using Xtender.Trees.Json.Converters.ToNode;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json.Builders;

public interface INodeConverterBuilder<TId>
{
    INodeConverterBuilder<TId> WithNodeConverter(ConverterExtension<TId, INode<TId>> extension);

    INodeConverterBuilder<TId> WithNodeCollectionConverter(ConverterExtension<TId, INode<TId>> extension);

    INodeConverterBuilder<TId> WithPropertiesNodeConverter(ConverterExtension<TId, INode<TId>> extension);

    INodeConverterBuilder<TId> WithPropertiesNodeCollectionConverter(ConverterExtension<TId, INode<TId>> extension);

    INodeConverterBuilder<TId> WithPropertyConverter(ConverterExtension<TId, INodeProperty> extension);

    INodeConverterBuilder<TId> WithPropertyCollectionConverter(ConverterExtension<TId, INodeProperty> extension);
}
using System.Xml;
using Xtender.Sync;
using Xtender.Trees.Nodes;
using Xtender.Trees.Serialization.Abstractions;
using Xtender.Trees.Serialization.Abstractions.Converters;
using Xtender.Trees.Serialization.XML.Extensions;

namespace Xtender.Trees.Serialization.XML;

public class XmlNodeConverterSchema<TId> : INodeConverterSchema<TId, XmlNode> where TId : notnull
{
    public INodeConverter<TId, XmlNode> CreateConverter<TValue>() where TValue : class => new XmlNodeConverter<TId, TValue>();

    public IToNodeConverter<TId, XmlNode> CreateFrom(INodeConverterRegistry<TId, XmlNode> registry) => new XmlToNodeConverter<TId>(registry);

    public IExtension<FromNodeConversionState<XmlNode>, IdCollection<TId, TValue>> GetIdCollectionExtension<TValue>() where TValue : class => new XmlFromNodeIdCollectionExtension<TId, TValue>();

    public IExtension<FromNodeConversionState<XmlNode>, NodeCollection<TId, TValue>> GetNodeCollectionExtension<TValue>() where TValue : class => new XmlFromNodeNodeCollectionExtension<TId, TValue>();

    public IExtension<FromNodeConversionState<XmlNode>, Node<TId, TValue>> GetNodeExtension<TValue>() where TValue : class => new XmlFromNodeNodeExtension<TId, TValue>();
}

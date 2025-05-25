using System;
using System.Xml;
using Xtender.Trees.Serialization.Abstractions.Converters;

namespace Xtender.Trees.Serialization.XML;

public class XmlToNodeConverter<TId>(INodeConverterRegistry<TId, XmlNode> registry) : ToNodeConverter<TId, XmlNode>(registry) where TId : notnull
{
    public override XmlNode GetCustomObject(XmlNode node) => node["_customObject"];

    public override TId GetId(XmlNode node) => System.Convert.ChangeType(node.Attributes["_id"], typeof(TId)) is not TId id
        ? throw new InvalidCastException($"Cannot parse '_id' attribute to type '{nameof(TId)}'")
        : id;

    public override string GetPartitionKey(XmlNode node) => node.Attributes["_partitionKey"].Value;

    public override string GetType(XmlNode node) => node.Attributes["_type"].Value;
}
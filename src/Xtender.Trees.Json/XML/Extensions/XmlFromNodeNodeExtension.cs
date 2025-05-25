using System.Xml;
using Xtender.Sync;
using Xtender.Trees.Nodes;
using Xtender.Trees.Serialization.Abstractions.Converters;

namespace Xtender.Trees.Serialization.XML.Extensions;

public class XmlFromNodeNodeExtension<TId, TValue> : IExtension<FromNodeConversionState<XmlNode>, Node<TId, TValue>>
    where TId : notnull
    where TValue : class
{
    public void Extend(Node<TId, TValue> context, IExtender<FromNodeConversionState<XmlNode>> extender)
    {
        var document = new XmlDocument();
        var node = document.CreateElement("Node");
        var type = extender.State.Provider.Invoke(typeof(TValue));

        node.SetAttribute("_id", context.Id.ToString());
        node.SetAttribute("_partitionKey", context.PartitionKey);
        node.SetAttribute("_type", type);

        var customObject = new XmlDocument().CreateElement("_customObject");
        customObject.AppendChild(context.Value.SerializeXml());

        node.AppendChild(customObject);
        extender.State.Results.Add(node);
    }
}
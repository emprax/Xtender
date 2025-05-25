using System.Xml;
using Xtender.Sync;
using Xtender.Trees.Nodes;
using Xtender.Trees.Serialization.Abstractions.Converters;

namespace Xtender.Trees.Serialization.XML.Extensions;

public class XmlFromNodeNodeCollectionExtension<TId, TValue> : IExtension<FromNodeConversionState<XmlNode>, NodeCollection<TId, TValue>>
    where TId : notnull
    where TValue : class
{
    public void Extend(NodeCollection<TId, TValue> context, IExtender<FromNodeConversionState<XmlNode>> extender)
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

        var children = new XmlDocument().CreateElement("children");
        foreach (var (id, value) in context)
        {
            var doc = new XmlDocument();
            var entity = doc.CreateElement("child");

            entity.InnerText = id.ToString();
            children.AppendChild(entity);

            value.Accept(extender);
        }

        node.AppendChild(children);
        extender.State.Results.Add(node);
    }
}

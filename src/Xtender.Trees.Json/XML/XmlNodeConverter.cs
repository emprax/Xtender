using System.Xml;
using Xtender.Trees.Nodes;
using Xtender.Trees.Serialization.Abstractions.Converters;

namespace Xtender.Trees.Serialization.XML;

public class XmlNodeConverter<TId, TValue> : INodeConverter<TId, XmlNode>
    where TId : notnull
    where TValue : class
{
    public INode<TId> Convert(TId id, string partitionKey, XmlNode customObject)
    {
        var value = customObject.Deserialize<TValue>();
        return new IdCollection<TId, TValue>(id, partitionKey, value);
    }
}
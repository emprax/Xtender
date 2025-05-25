using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Xtender.Trees.Serialization.XML;

public static class XmlNodeExtensions
{
    public static TValue Deserialize<TValue>(this XmlNode node) => (TValue)node.Deserialize(typeof(TValue));

    public static object Deserialize(this XmlNode node, Type type)
    {
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);

        writer.Write(node.OuterXml);
        writer.Flush();

        stream.Position = 0;
        var serializer = new XmlSerializer(type);

        return serializer.Deserialize(stream);
    }

    public static XmlDocument SerializeXml<TValue>(this TValue node)
    {
        using var stream = new MemoryStream();

        var serializer = new XmlSerializer(typeof(TValue));
        serializer.Serialize(stream, node);

        stream.Position = 0;

        var xml = new XmlDocument();
        xml.Load(stream);

        return xml;
    }
}
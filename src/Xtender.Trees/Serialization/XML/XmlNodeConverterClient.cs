using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Xml;
using Xtender.Trees.Nodes;
using Xtender.Trees.Serialization.Abstractions.Converters;

namespace Xtender.Trees.Serialization.XML;

public class XmlNodeConverterClient<TId>(INodeConverterClient<TId, JsonNode> client) : INodeConverterClient<TId, XmlNode> where TId : notnull
{
    public ICollection<XmlNode> FromNode(INode<TId> node) => client
        .FromNode(node)
        .Select(x => JsonConvert
            .DeserializeXmlNode(WithRoot(x).ToJsonString())!
            .FirstChild!)
        .ToList();

    public INode<TId> ToNode(XmlNode node)
    {
        var json = JsonNode.Parse(JsonConvert.SerializeXmlNode(node))!["node"]!;
        return client.ToNode(json);
    }

    private static JsonObject WithRoot(JsonNode node) => new JsonObject(new Dictionary<string, JsonNode?>
    {
        ["node"] = node
    });
}

public static class ObjectPropertyRegulator
{
    public static JsonObject RegulateProperties(JsonNode node, Type type)
    {
        var @object = new JsonObject();
        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.SetProperty))
        {
            if ((property.PropertyType.IsClass || property.PropertyType.IsInterface) && !property.PropertyType.IsAssignableTo(typeof(IEnumerable)))
            {
                var value = RegulateProperties(node[property.Name]!, property.PropertyType);
                @object.Add(property.Name, value);

                continue;
            }

            if (property.PropertyType.IsAssignableTo(typeof(IEnumerable)))
            {
                var value = node[property.Name]?
                    .AsArray()?
                    .Select(x => JsonValue.Create(Convert.ChangeType(x!, property.PropertyType)))?
                    .ToArray() ?? [];

                @object.Add(property.Name, new JsonArray(value));
                continue;
            }

            var result = Convert.ChangeType(node[property.Name]!, property.PropertyType);
            @object.Add(property.Name, JsonValue.Create(result));
        }

        return @object;
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json.Nodes;
using Xtender.Trees.Json.Converters.ToNode;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json.Converters.Extensions;

public class NodeConverterExtension<TId> : IConverterExtension<INode<TId>>
{
    public static readonly string Key = "node";

    private readonly INodeConverter<TId> converter;

    public NodeConverterExtension(INodeConverter<TId> converter) => this.converter = converter;

    public INode<TId> Convert(string type, IReadOnlyDictionary<string, JsonNode> nodes)
    {
        var hasFoundId = nodes.TryGetValue<TId>("id", out var id);
        if (!hasFoundId)
        {
            return null;
        }

        var properties = nodes.TryGetValue<IDictionary<string, JsonNode>>("properties", out var propertiesValues)
            ? this.GetProperties(propertiesValues).ToArray()
            : Array.Empty<KeyValuePair<string, INodeProperty>>();

        var propertiesResult = new NodePropertyCollection(properties);
        return new Node<TId>(id, type, propertiesResult);
    }

    private IEnumerable<KeyValuePair<string, INodeProperty>> GetProperties(IDictionary<string, JsonNode> properties)
    {
        foreach (var (key, property) in properties)
        {
            var propertyResult = GetFields(property, (type, values) => this.converter.ConvertProperty(type, new ReadOnlyDictionary<string, JsonNode>(values)));
            yield return new(key, propertyResult);
        }
    }

    private static TResult GetFields<TResult>(JsonNode json, Func<string, IDictionary<string, JsonNode>, TResult> converter)
    {
        var values = json
            .AsObject()
            .AsEnumerable()
            .ToDictionary();

        var hasFound = values["$type"]
            .AsValue()
            .TryGetValue<string>(out var type);

        return !hasFound
            ? throw new InvalidCastException("Should have $type property")
            : converter.Invoke(type, values);
    }
}

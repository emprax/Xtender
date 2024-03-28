using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json.Nodes;
using Xtender.Trees.Json.Converters.ToNode;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json.Converters.Extensions;

public class NodeCollectionConverterExtension<TId> : IConverterExtension<INode<TId>>
{
    public static readonly string Key = "node-collection";

    private readonly INodeConverter<TId> converter;

    public NodeCollectionConverterExtension(INodeConverter<TId> converter) => this.converter = converter;

    public INode<TId> Convert(string partitionKey, IReadOnlyDictionary<string, JsonNode> nodes)
    {
        var hasFoundId = nodes.TryGetValue<TId>("id", out var id);
        if (!hasFoundId)
        {
            return null;
        }

        var children = nodes.TryGetValue<IDictionary<TId, JsonNode>>("children", out var childrenValues)
            ? this.GetChildren(childrenValues).ToArray()
            : Array.Empty<KeyValuePair<TId, INode<TId>>>();

        var properties = nodes.TryGetValue<IDictionary<string, JsonNode>>("properties", out var propertiesValues)
            ? this.GetProperties(propertiesValues).ToArray()
            : Array.Empty<KeyValuePair<string, INodeProperty>>();

        var propertiesResult = new NodePropertyCollection(properties);
        var childrenResult = children.Select(x => x.Value);

        return new NodeCollection<TId>(id, partitionKey, propertiesResult, childrenResult);
    }

    private IEnumerable<KeyValuePair<string, INodeProperty>> GetProperties(IDictionary<string, JsonNode> properties)
    {
        foreach (var (key, property) in properties)
        {
            var propertyResult = GetFields(property, (type, values) => this.converter.ConvertProperty(type, new ReadOnlyDictionary<string, JsonNode>(values)));
            yield return new(key, propertyResult);
        }
    }

    private IEnumerable<KeyValuePair<TId, INode<TId>>> GetChildren(IDictionary<TId, JsonNode> children)
    {
        foreach (var (id, child) in children!)
        {
            var childResult = GetFields(child, (type, values) => this.converter.ConvertNode(type, new ReadOnlyDictionary<string, JsonNode>(values)));
            yield return new(id, childResult);
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

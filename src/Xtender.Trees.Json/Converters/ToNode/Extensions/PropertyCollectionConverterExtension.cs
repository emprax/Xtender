using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json.Nodes;
using Xtender.Trees.Json.Converters.ToNode;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json.Converters.Extensions;

internal class PropertyCollectionConverterExtension<TId> : IConverterExtension<INodeProperty>
{
    private readonly INodeConverter<TId> converter;

    public PropertyCollectionConverterExtension(INodeConverter<TId> converter) => this.converter = converter;

    public INodeProperty Convert(string type, IReadOnlyDictionary<string, JsonNode> nodes)
    {
        var properties = nodes.TryGetValue<IDictionary<string, JsonNode>>("properties", out var propertiesValues)
            ? this.GetProperties(propertiesValues).ToArray()
            : Array.Empty<KeyValuePair<string, INodeProperty>>();

        var propertiesResult = properties.ToDictionary();
        return new NodePropertyCollection(propertiesResult);
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
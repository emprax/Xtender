using System.Collections.Generic;
using System.Text.Json.Nodes;
using Xtender.Trees.Json.Converters.ToNode;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json.Converters.Extensions.Overridables;

public abstract class CustomPropertyCollectionConverter<TId, TProperty> : IConverterExtension<INodeProperty> where TProperty : NodePropertyCollection
{
    private readonly PropertyCollectionConverterExtension<TId> converter;

    protected CustomPropertyCollectionConverter(INodeConverter<TId> converter) => this.converter = new(converter);

    public INodeProperty Convert(string type, IReadOnlyDictionary<string, JsonNode> nodes)
    {
        var property = this.converter.Convert(type, nodes);
        return this.ConvertExtended((TProperty)property, nodes);
    }

    protected abstract TProperty ConvertExtended(TProperty property, IReadOnlyDictionary<string, JsonNode> nodes);
}

using System.Collections.Generic;
using System.Text.Json.Nodes;
using Xtender.Sync;
using Xtender.Trees.Nodes;
using Xtender.Trees.Serialization.Abstractions.Converters;

namespace Xtender.Trees.Serialization.Json;

internal class JsonNodeConverterClient<TId> : INodeConverterClient<TId, JsonNode> where TId : notnull
{
    private readonly IExtender<FromNodeConversionState> extender;
    private readonly IToNodeConverter<TId> converter;
    private readonly IDictionary<string, string> keys;

    public JsonNodeConverterClient(
        IExtender<FromNodeConversionState> extender,
        IToNodeConverter<TId> converter,
        IDictionary<string, string> keys)
    {
        this.extender = extender;
        this.converter = converter;
        this.keys = keys;
    }

    public INode<TId> ToNode(JsonNode node) => this.converter.Convert(node);

    public ICollection<JsonNode> FromNode(INode<TId> node)
    {
        this.extender.State = new(x => this.keys[x.FullName!]);
        node.Accept(this.extender);

        return this.extender.State.Results;
    }
}
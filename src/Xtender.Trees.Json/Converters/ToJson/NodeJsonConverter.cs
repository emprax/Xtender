using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xtender.Trees.Json.Converters.ToNode;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json.Converters.ToJson;

public class NodeJsonConverter<TId> : JsonConverter<INode<TId>>
{
    private readonly INodeConverterClient<TId> converter;

    public NodeJsonConverter(INodeConverterClient<TId> converter) => this.converter = converter;

    public override INode<TId> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var bytes = reader.ValueSpan.ToArray();
        return this.converter.Convert(bytes);
    }

    public override void Write(Utf8JsonWriter writer, INode<TId> value, JsonSerializerOptions options)
    {
        var bytes = this.converter.Convert(value);
        writer.WriteStringValue(bytes);
    }
}
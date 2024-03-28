using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using Xtender.Olds.Sync;
using Xtender.Trees.Json.Converters.ToNode;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json.Converters;

internal class NodeConverterClient<TId> : INodeConverterClient<TId>
{
    private readonly INodeConverter<TId> converter;
    private readonly IExtender<JsonNode> extender;

    public NodeConverterClient(INodeConverter<TId> converter, IExtender<JsonNode> extender)
    {
        this.converter = converter;
        this.extender = extender;
    }

    public INode<TId> Convert(byte[] source)
    {
        using var stream = new MemoryStream();
        stream.Write(source);

        var values = JsonNode
            .Parse(stream)
            .AsObject()
            .AsEnumerable()
            .ToDictionary();

        return values["$type"]?.AsValue().TryGetValue<string>(out var type) ?? false
            ? this.converter.ConvertNode(type, new ReadOnlyDictionary<string, JsonNode>(values))
            : throw new InvalidDataException("The root object should have a $type property");
    }

    public byte[] Convert(INode<TId> node)
    {
        node.Accept(this.extender);
        return Encoding.UTF8.GetBytes(this.extender.State.ToJsonString());
    }
}
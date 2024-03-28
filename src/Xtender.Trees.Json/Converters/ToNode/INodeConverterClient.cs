using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json.Converters.ToNode;

public interface INodeConverterClient<TId>
{
    INode<TId> Convert(byte[] source);

    byte[] Convert(INode<TId> node);
}

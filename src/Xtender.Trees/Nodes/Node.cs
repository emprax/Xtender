using System.Threading.Tasks;
using Xtender.Async;
using Xtender.Sync;

namespace Xtender.Trees.Nodes;

public class Node<TId> : INode<TId>
{
    public Node(TId id, string partitionKey)
    {
        this.Id = id;
        this.Type = "node";
        this.PartitionKey = partitionKey;
    }

    public TId Id { get; }

    public string PartitionKey { get; }

    public string Type { get; protected set; }

    public virtual void Accept(IExtender extender) => extender.Extend(this);

    public virtual Task Accept(IAsyncExtender extender) => extender.Extend(this);
}

public class Node<TId, TValue> : Node<TId>, INode<TId, TValue>
{
    public Node(TId id, string partitionKey, TValue value) : base(id, partitionKey) => this.Value = value;

    public TValue Value { get; }
}

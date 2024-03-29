using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xtender.Async;
using Xtender.Sync;

namespace Xtender.Trees.Nodes;

public class NodeCollection<TId> : Node<TId>, IEnumerable<KeyValuePair<TId, INode<TId>>> where TId : notnull
{
    private readonly IDictionary<TId, INode<TId>> children;

    public NodeCollection(TId id, string partitionKey) : this(id, partitionKey, Enumerable.Empty<INode<TId>>()) { }

    public NodeCollection(TId id, string partitionKey, IEnumerable<INode<TId>> children) : base(id, partitionKey)
    {
        this.children = children.ToDictionary(x => x.Id, x => x);
        this.Type = "node-collection";
    }

    public NodeCollection(TId id, string partitionKey, IDictionary<TId, INode<TId>> children) : base(id, partitionKey)
    {
        this.children = children;
        this.Type = "node-collection";
    }

    public INode<TId> this[TId id]
    {
        get => this.children[id];
        set => this.children[id] = value;
    }

    public bool TryGetNode(TId id, out INode<TId>? node) => this.children.TryGetValue(id, out node);

    public bool Add(INode<TId> node) => this.children.TryAdd(node.Id, node);

    public bool Remove(TId id) => this.children.Remove(id);

    public bool Remove(INode<TId> node) => this.Remove(node.Id);

    public override void Accept(IExtender extender) => extender.Extend(this);

    public override Task Accept(IAsyncExtender extender) => extender.Extend(this);

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    public IEnumerator<KeyValuePair<TId, INode<TId>>> GetEnumerator() => this.children.GetEnumerator();
}

public class NodeCollection<TId, TValue> : NodeCollection<TId>, INode<TId, TValue> where TId : notnull
{
    public NodeCollection(TId id, string partitionKey, TValue value) : base(id, partitionKey, Enumerable.Empty<INode<TId>>()) => this.Value = value;

    public NodeCollection(TId id, string partitionKey, IEnumerable<INode<TId>> children, TValue value) : base(id, partitionKey, children) => this.Value = value;

    public NodeCollection(TId id, string partitionKey, IDictionary<TId, INode<TId>> children, TValue value) : base(id, partitionKey, children) => this.Value = value;

    public TValue Value { get; }
}

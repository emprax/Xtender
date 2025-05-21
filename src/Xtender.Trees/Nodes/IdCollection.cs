using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xtender.Async;
using Xtender.Sync;

namespace Xtender.Trees.Nodes;

public class IdCollection<TId> : Node<TId>, IEnumerable<TId> where TId : notnull
{
    private readonly ISet<TId> children;

    public IdCollection(TId id, string partitionKey) : this(id, partitionKey, Enumerable.Empty<TId>()) { }

    public IdCollection(TId id, string partitionKey, IEnumerable<TId> children) : base(id, partitionKey)
    {
        this.children = children.ToHashSet();
        this.Type = "id-collection";
    }

    public int Count => this.children.Count;

    public bool Contains(TId id) => this.children.Contains(id);

    public bool Add(TId id) => this.children.Add(id);

    public bool Remove(TId id) => this.children.Remove(id);

    public override void Accept(IExtender extender) => extender.Extend(this);

    public override Task Accept(IAsyncExtender extender) => extender.Extend(this);

    public IEnumerator<TId> GetEnumerator() => this.children.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}

public class IdCollection<TId, TValue> : IdCollection<TId>, INode<TId, TValue> where TId : notnull
{
    public IdCollection(TId id, string partitionKey, TValue value) : base(id, partitionKey) => this.Value = value;

    public IdCollection(TId id, string partitionKey, TValue value, IEnumerable<TId> children) : base(id, partitionKey, children) => this.Value = value;

    public TValue Value { get; }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xtender.Trees.Builders;

namespace Xtender.Trees
{
    public class Node<TItem> : INode<TItem>
    {
        private readonly List<INode> children;

        public Node(INode parent, TItem item) : this(Guid.NewGuid().ToString(), parent, item) { }

        public Node(string id, INode parent, TItem item) : this(id, parent, item, new List<INode>()) { }

        public Node(INode parent, TItem item, List<INode> children) : this(Guid.NewGuid().ToString(), parent, item, children) { }

        public Node(INode parent, TItem item, Action<INodeBuilder> builder) : this(Guid.NewGuid().ToString(), parent, item, builder) { }

        public Node(string id, INode parent, TItem item, List<INode> children)
        {
            this.Id = id;
            this.Value = item;
            this.Parent = parent;
            this.children = children;
        }

        public Node(string id, INode parent, TItem item, Action<INodeBuilder> builder)
        {
            this.Id = id;
            this.Value = item;
            this.Parent = parent;
            this.children = new List<INode>();

            builder.Invoke(new NodeBuilder(this));
        }

        public string Id { get; }

        public TItem Value { get; }

        public INode Parent { get; }

        public IReadOnlyCollection<INode> Children => this.children.AsReadOnly();

        public INode<TValue> AddValue<TValue>(TValue value)
        {
            var node = new Node<TValue>(this, value);
            this.children.Add(node);
            return node;
        }

        public INode<TValue> AddValue<TValue>(string id, TValue value)
        {
            var node = new Node<TValue>(id, this, value);
            this.children.Add(node);
            return node;
        }

        public void Add(INode node) => this.children.Add(node);

        public void Add(Action<INodeBuilder> builder) => builder.Invoke(new NodeBuilder(this));

        public void AddRange(params INode[] values)
        {
            foreach (var value in values)
            {
                this.children.Add(value);
            }
        }

        public bool Remove(INode node) => this.children.Remove(node);

        public bool Remove(string id)
        {
            var node = this.children.FirstOrDefault(n => n.Id == id);
            return this.children.Remove(node);
        }

        IEnumerator<INode> IEnumerable<INode>.GetEnumerator()
        {
            yield return this;
            foreach (var node in children.SelectMany(child => child))
            {
                yield return node;
            }
        }

        public IEnumerator GetEnumerator() => ((IEnumerable<INode>)this).GetEnumerator();

        public Task Accept(IExtender extender) => extender.Extend(this);
    }
}
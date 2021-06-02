using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xtender.Trees
{
    public class Tree : ITree
    {
        private INode rootNode;

        public Tree() { }

        public Tree(INode root)
        {
            this.Root = root;
            this.Root.Parent = null;
        }

        public INode Root
        {
            get => this.rootNode;
            set
            {
                this.rootNode = value;
                this.rootNode.Parent = null;
            }
        }

        IEnumerator<INode> IEnumerable<INode>.GetEnumerator() => this.Root.GetEnumerator();

        public IEnumerator GetEnumerator() => this.Root.GetEnumerator();

        public INode GetNode(string id)
            => this.Root.FirstOrDefault(x => x.Id == id);

        public INode<TValue> InsertValue<TValue>(string parentId, TValue value)
        {
            if (!string.IsNullOrWhiteSpace(parentId))
            {
                return this.GetNode(parentId)?.AddValue(value);
            }

            this.Root = new Node<TValue>(null, value);
            return this.Root as INode<TValue>;
        }

        public INode<TValue> InsertValue<TValue>(string parentId, string id, TValue value)
        {
            if (!string.IsNullOrWhiteSpace(parentId))
            {
                return this.GetNode(parentId)?.AddValue(id, value);
            }

            this.Root = new Node<TValue>(id, null, value);
            return this.Root as INode<TValue>;
        }

        public bool InsertRange(string parentId, params INode[] nodes)
        {
            if (string.IsNullOrWhiteSpace(parentId))
            {
                return false;
            }

            var node = this.GetNode(parentId);
            if (node is null)
            {
                return false;
            }

            node.AddRange(nodes);
            return true;
        }

        public bool Insert(string parentId, INode node)
        {
            if (string.IsNullOrWhiteSpace(parentId))
            {
                this.Root = node;
                return true;
            }

            var result = this.GetNode(parentId);
            if (result is null)
            {
                return false;
            }

            result.Add(node);
            return true;
        }

        public bool Remove(string id)
        {
            var node = this.GetNode(id);
            return !(node?.Parent is null) && node.Parent.Remove(node);
        }

        public virtual Task Accept(IExtender extender) => extender.Extend(this);
    }
}

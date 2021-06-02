using System;

namespace Xtender.Trees.Builders
{
    public class NodeBuilder : INodeBuilder
    {
        private readonly INode parentNode;

        public NodeBuilder(INode parentNode) => this.parentNode = parentNode;

        public INodeBuilder Add(INode node)
        {
            this.parentNode.Add(node);
            return this;
        }

        public INodeBuilder Add(INode node, Action<INodeBuilder> innerBuilder)
        {
            innerBuilder.Invoke(new NodeBuilder(node));
            this.parentNode.Add(node);
            return this;
        }

        public INodeBuilder AddByValue<TValue>(TValue value)
        {
            this.parentNode.AddValue(value);
            return this;
        }

        public INodeBuilder AddByValue<TValue>(TValue value, Action<INodeBuilder> innerBuilder)
        {
            var node = new Node<TValue>(this.parentNode, value);
            innerBuilder.Invoke(new NodeBuilder(node));
            this.parentNode.Add(node);

            return this;
        }

        public INodeBuilder AddByValue<TValue>(string id, TValue value)
        {
            this.parentNode.AddValue(id, value);
            return this;
        }

        public INodeBuilder AddByValue<TValue>(string id, TValue value, Action<INodeBuilder> innerBuilder)
        {
            var node = new Node<TValue>(id, this.parentNode, value);
            innerBuilder.Invoke(new NodeBuilder(node));
            this.parentNode.Add(node);

            return this;
        }
    }
}

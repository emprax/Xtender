using System;

namespace Xtender.Trees.Builders
{
    public class TreeBuilder : ITreeBuilder
    {
        public static ITreeBuilder New() => new TreeBuilder();

        public ITree Create(INode rootNode) => new Tree(rootNode);

        public ITree Create(INode rootNode, Action<INodeBuilder> builder)
        {
            rootNode.Add(builder);
            return new Tree(rootNode);
        }

        public ITree CreateByValue<TValue>(TValue value) => new Tree(new Node<TValue>(null, value));

        public ITree CreateByValue<TValue>(TValue value, Action<INodeBuilder> builder) => new Tree(new Node<TValue>(null, value, builder));

        public ITree CreateByValue<TValue>(string id, TValue value) => new Tree(new Node<TValue>(id, null, value));

        public ITree CreateByValue<TValue>(string id, TValue value, Action<INodeBuilder> builder) => new Tree(new Node<TValue>(id, null, value, builder));
    }
}

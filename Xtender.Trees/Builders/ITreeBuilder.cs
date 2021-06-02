using System;

namespace Xtender.Trees.Builders
{
    public interface ITreeBuilder
    {
        ITree Create(INode rootNode);

        ITree Create(INode rootNode, Action<INodeBuilder> builder);

        ITree CreateByValue<TValue>(TValue value);

        ITree CreateByValue<TValue>(TValue value, Action<INodeBuilder> builder);

        ITree CreateByValue<TValue>(string id, TValue value);

        ITree CreateByValue<TValue>(string id, TValue value, Action<INodeBuilder> builder);
    }
}
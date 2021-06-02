using System;

namespace Xtender.Trees.Builders
{
    public interface INodeBuilder
    {
        INodeBuilder Add(INode node);

        INodeBuilder Add(INode node, Action<INodeBuilder> innerBuilder);

        INodeBuilder AddByValue<TValue>(TValue value);

        INodeBuilder AddByValue<TValue>(TValue value, Action<INodeBuilder> innerBuilder);

        INodeBuilder AddByValue<TValue>(string id, TValue value);

        INodeBuilder AddByValue<TValue>(string id, TValue value, Action<INodeBuilder> innerBuilder);
    }
}
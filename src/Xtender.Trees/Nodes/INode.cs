using Xtender.Async;
using Xtender.Sync;

namespace Xtender.Trees.Nodes;

public interface INode<TId> : IAccepter, IAsyncAccepter where TId : notnull
{
    TId Id { get; }

    string Type { get; }

    string PartitionKey { get; }
}

public interface INode<TId, TValue> : INode<TId> where TId : notnull
{
    TValue Value { get; }
}
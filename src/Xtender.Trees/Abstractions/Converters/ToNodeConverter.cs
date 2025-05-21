using Xtender.Trees.Nodes;

namespace Xtender.Trees.Abstractions.Converters;

public abstract class ToNodeConverter<TId, TTransferObject> : IToNodeConverter<TId, TTransferObject>
    where TId : notnull
    where TTransferObject : class
{
    private readonly INodeConverterRegistry<TId, TTransferObject> registry;

    protected ToNodeConverter(INodeConverterRegistry<TId, TTransferObject> registry) => this.registry = registry;

    public abstract TId GetId(TTransferObject node);

    public abstract string GetPartitionKey(TTransferObject node);

    public abstract TTransferObject GetCustomObject(TTransferObject node);

    public abstract string GetType(TTransferObject node);

    public INode<TId> Convert(TTransferObject node)
    {
        var id = this.GetId(node);
        var partitionKey = this.GetPartitionKey(node);
        var customObject = this.GetCustomObject(node);
        var type = this.GetType(node);

        return this.registry
            .Get(type)
            .Convert(id, partitionKey, customObject);
    }
}

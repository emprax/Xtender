namespace Xtender.Trees.Serialization.Abstractions.Converters;

public interface INodeConverterRegistry<TId, TTransferObject>
    where TId : notnull
    where TTransferObject : class
{
    INodeConverter<TId, TTransferObject> Get(string key);
}

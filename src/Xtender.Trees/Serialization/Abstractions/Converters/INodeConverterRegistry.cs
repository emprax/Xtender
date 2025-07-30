namespace Xtender.Trees.Serialization.Abstractions.Converters;

public interface INodeConverterRegistry<TId> where TId : notnull
{
    INodeConverter<TId> Get(string key);
}

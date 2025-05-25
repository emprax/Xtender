namespace Xtender.Trees.Serialization.Builders;

public interface IConverterBuildClient<TId, TTransferObject>
    where TId : notnull
    where TTransferObject : class
{
    IConverterBuilder<TId, TTransferObject> Mapping<TValue>(string key) where TValue : class;
}

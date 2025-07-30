namespace Xtender.Trees.Serialization.Builders;

public interface IConverterBuildClient<TId> where TId : notnull
{
    IConverterBuilder<TId> Mapping<TValue>(string key) where TValue : class;
}

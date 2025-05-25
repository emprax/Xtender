using Xtender.Trees.Serialization.Abstractions.Converters;

namespace Xtender.Trees.Serialization.Builders;

public interface IConverterBuilder<TId, TTransferObject> : IConverterBuildClient<TId, TTransferObject>
    where TId : notnull
    where TTransferObject : class
{
    INodeConverterClient<TId, TTransferObject> Build();
}
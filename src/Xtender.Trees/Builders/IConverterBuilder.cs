using Xtender.Trees.Abstractions.Converters;

namespace Xtender.Trees.Builders;

public interface IConverterBuilder<TId, TTransferObject> : IConverterBuildClient<TId, TTransferObject>
    where TId : notnull
    where TTransferObject : class
{
    INodeConverterClient<TId, TTransferObject> Build();
}
using Microsoft.Extensions.DependencyInjection;
using Xtender.Trees.Serialization.Abstractions.Converters;

namespace Xtender.Trees.Serialization;

public interface INodeConverterRegistration<TId> where TId : notnull
{
    IServiceCollection Services { get; }

    INodeConverterRegistration<TId> WithOtherConverter<TTransferObject, TNodeConverterClient>()
        where TTransferObject : class
        where TNodeConverterClient : class, INodeConverterClient<TId, TTransferObject>;
}

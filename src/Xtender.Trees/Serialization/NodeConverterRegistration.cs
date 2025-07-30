using Microsoft.Extensions.DependencyInjection;
using Xtender.Trees.Serialization.Abstractions.Converters;

namespace Xtender.Trees.Serialization;

internal class NodeConverterRegistration<TId>(IServiceCollection services) : INodeConverterRegistration<TId> where TId : notnull
{
    public IServiceCollection Services => services;

    public INodeConverterRegistration<TId> WithOtherConverter<TTransferObject, TNodeConverterClient>()
        where TTransferObject : class
        where TNodeConverterClient : class, INodeConverterClient<TId, TTransferObject>
    {
        services.AddSingleton<INodeConverterClient<TId, TTransferObject>, TNodeConverterClient>();
        return this;
    }
}

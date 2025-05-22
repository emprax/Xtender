using Microsoft.Extensions.DependencyInjection;
using System;
using Xtender.Trees.Abstractions;
using Xtender.Trees.Builders;

namespace Xtender.Trees;

public static class ModuleInitializer
{
    public static IServiceCollection AddNodeConverter<TId, TTransferObject>(this IServiceCollection services, INodeConverterSchema<TId, TTransferObject> schema, Action<IConverterBuildClient<TId, TTransferObject>> build)
        where TId : notnull
        where TTransferObject : class
    {
        var builder = new ConverterBuilder<TId, TTransferObject>(schema);
        build.Invoke(builder);

        return services.AddSingleton(builder.Build());
    }
}

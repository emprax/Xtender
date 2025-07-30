using Microsoft.Extensions.DependencyInjection;
using System;
using System.Xml;
using Xtender.Trees.Serialization.Builders;
using Xtender.Trees.Serialization.Converting;
using Xtender.Trees.Serialization.XML;

namespace Xtender.Trees.Serialization;

public static class ModuleInitializer
{
    public static INodeConverterRegistration<TId> AddNodeConverter<TId>(this IServiceCollection services, Action<IConverterBuildClient<TId>> build) where TId : notnull
    {
        var builder = new ConverterBuilder<TId>(new NodeConverterSchema<TId>());
        build.Invoke(builder);

        return new NodeConverterRegistration<TId>(services.AddSingleton(builder.Build()));
    }

    public static INodeConverterRegistration<TId> WithXmlConverter<TId>(this INodeConverterRegistration<TId> registration) where TId : notnull
        => registration.WithOtherConverter<XmlNode, XmlNodeConverterClient<TId>>();
}
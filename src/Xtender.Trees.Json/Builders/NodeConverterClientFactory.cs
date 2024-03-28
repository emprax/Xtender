using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using Xtender.DependencyInjection.Old.Sync;
using Xtender.Olds.Sync;
using Xtender.Trees.Json.Converters;
using Xtender.Trees.Json.Converters.Extensions;
using Xtender.Trees.Json.Converters.ToJson;
using Xtender.Trees.Json.Converters.ToNode;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Json.Builders;

public static class NodeConverterClientFactory
{
    public static INodeConverterClient<TId> Create<TId>() => new NodeConverterClient<TId>(new NodeConverter<TId>(
        new ReadOnlyDictionary<string, ConverterExtension<TId, INode<TId>>>(GetNodeConverters<TId>()),
        new ReadOnlyDictionary<string, ConverterExtension<TId, INodeProperty>>(GetPropertyConverters<TId>())),
        GetExtender<TId>((_, _) => { }));

    public static INodeConverterClient<TId> Create<TId>(Action<INodeConverterBuilder<TId>> build, Action<IConnectedExtenderBuilder<JsonNode>, IServiceProvider> extendedBuilder)
    {
        var builder = new NodeConverterBuilder<TId>(GetNodeConverters<TId>(), GetPropertyConverters<TId>());
        build.Invoke(builder);

        var extenderBuilder = GetExtender<TId>(extendedBuilder);
        return new NodeConverterClient<TId>(builder.Build(), extenderBuilder);
    }

    private static IExtender<JsonNode> GetExtender<TId>(Action<IConnectedExtenderBuilder<JsonNode>, IServiceProvider> extendedAction) => new ServiceCollection()
        .AddXtender<JsonNode>((builder, provider) =>
        {
            var extendedBuilder = builder.Default()
                .Attach<Node<TId>, NodeToJsonExtension<TId>>()
                .Attach<NodeProperty, PropertyToJsonExtension>()
                .Attach<NodeCollection<TId>, NodeCollectionToJsonExtension<TId>>()
                .Attach<NodePropertyCollection, PropertyCollectionToJsonExtension>();

            extendedAction.Invoke(extendedBuilder, provider);
        })
        .BuildServiceProvider()
        .GetRequiredService<IExtender<JsonNode>>();

    private static IDictionary<string, ConverterExtension<TId, INode<TId>>> GetNodeConverters<TId>() => new Dictionary<string, ConverterExtension<TId, INode<TId>>>()
    {
        ["node-collection"] = new(x => new NodeCollectionConverterExtension<TId>(x)),
        ["properties-node-collection"] = new(x => new NodeCollectionConverterExtension<TId>(x))
    };

    private static IDictionary<string, ConverterExtension<TId, INodeProperty>> GetPropertyConverters<TId>() => new Dictionary<string, ConverterExtension<TId, INodeProperty>>()
    {
        ["property"] = new(_ => new PropertyConverterExtension()),
        ["property-collection"] = new(x => new PropertyCollectionConverterExtension<TId>(x))
    };
}

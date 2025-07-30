using Microsoft.Extensions.DependencyInjection;
using System.Xml;
using Xtender.Trees.Nodes;
using Xtender.Trees.Serialization;
using Xtender.Trees.Serialization.Abstractions.Converters;
using Xtender.Trees.Tests.Models;

namespace Xtender.Trees.Tests;

public class XmlParserTests
{
    [Fact]
    public void ShouldConvertNodeToXmlComponents()
    {
        // Arrange
        var converter = new ServiceCollection()
            .AddNodeConverter<Guid>(b => b
                .Mapping<TestClass1>("test1")
                .Mapping<TestClass2>("test2")
                .Mapping<TestClass3>("test3"))
            .WithXmlConverter()
            .Services
            .BuildServiceProvider()
            .GetRequiredService<INodeConverterClient<Guid, XmlNode>>();

        var collection = new NodeCollection<Guid, TestClass3>(Guid.NewGuid(), "v1", new("V1", DateTime.UtcNow))
        {
            new NodeCollection<Guid, TestClass2>(Guid.NewGuid(), "v1.1", new(1))
            {
                new Node<Guid, TestClass1>(Guid.NewGuid(), "v1.1.1", new("V1.1.1")),
                new IdCollection<Guid, TestClass1>(Guid.NewGuid(), "v1.1.2", new("V1.1.2"))
                {
                    Guid.NewGuid(),
                    Guid.NewGuid()
                },
                new IdCollection<Guid, TestClass1>(Guid.NewGuid(), "v1.1.3", new("V1.1.3"))
                {
                    Guid.NewGuid(),
                    Guid.NewGuid()
                }
            },
            new NodeCollection<Guid, TestClass2>(Guid.NewGuid(), "v1.2", new(2))
            {
                new Node< Guid, TestClass3>(Guid.NewGuid(), "v1.2.1", new("V1.2.1", DateTime.UtcNow)),
                new IdCollection<Guid, TestClass2>(Guid.NewGuid(), "v1.2.2", new(3))
                {
                    Guid.NewGuid(),
                    Guid.NewGuid()
                },
                new IdCollection<Guid, TestClass2>(Guid.NewGuid(), "v1.2.3", new(4))
                {
                    Guid.NewGuid(),
                    Guid.NewGuid()
                }
            }
        };

        // Act
        var result = converter
            .FromNode(collection)
            .Select(x => x.OuterXml)
            .Where(x => x is not null)
            .ToArray();

        // Assert
        Assert.Equal(9, result.Length);
        Assert.Contains("_id", result[0]);
        Assert.Contains("_partitionKey", result[0]);
        Assert.Contains("_type", result[0]);
        Assert.Contains("_customObject", result[0]);
        Assert.Contains("<Name>V1.1.1</Name>", result[0]);
    }

    [Fact]
    public void ShouldConvertXmlToNodes()
    {
        // Arrange
        var doc = new XmlDocument();
        doc.LoadXml(File.ReadAllText(Path.Combine("Files", "nodes.xml")));

        var nodes = doc
            .SelectSingleNode("nodes")!
            .Cast<XmlNode>()
            .ToArray();

        var converter = new ServiceCollection()
            .AddNodeConverter<Guid>(b => b
                .Mapping<LearningYear>("learning-year")
                .Mapping<SchoolYear>("school-year")
                .Mapping<Course>("course")
                .Mapping<Material>("material"))
            .WithXmlConverter()
            .Services
            .BuildServiceProvider()
            .GetRequiredService<INodeConverterClient<Guid, XmlNode>>();

        // Act
        var results = nodes
            .Select(converter.ToNode)
            .Cast<IdCollection<Guid>>()
            .ToArray();

        // Assert
        Assert.Equal(nodes.Length, results.Length);
        for (var index = 0; index < nodes.Length; index++)
        {
            Assert.Equal(Guid.Parse(nodes[index]!.SelectSingleNode("_id")!.FirstChild!.Value!), results[index].Id);
            Assert.Equal(nodes[index]!.SelectSingleNode("_partitionKey")!.FirstChild!.Value!, results[index].PartitionKey);
            Assert.Equal("id-collection", results[index].Type);

            var counter = 0;
            foreach (var id in results[index])
            {
                Assert.Equal(Guid.Parse(nodes[index]!.SelectSingleNode("_children")!.ChildNodes[counter]!.Value!), id);
            }
        }
    }
}
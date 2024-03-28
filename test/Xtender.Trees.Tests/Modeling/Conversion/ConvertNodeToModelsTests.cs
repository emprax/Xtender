using Microsoft.Extensions.DependencyInjection;
using Xtender.DependencyInjection.Old.Sync;
using Xtender.Olds.Sync;
using Xtender.Trees.Modeling;
using Xtender.Trees.Modeling.Conversion.NoteToModels;
using Xtender.Trees.Modeling.Conversion.NoteToModels.Extensions;
using Xtender.Trees.Modeling.Models;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Tests.Modeling.Conversion;

public class ConvertNodeToModelsTests
{
    private readonly IExtender<NodeToModelsConversionState<Guid>> extender;
    private readonly INodeToModelsConverter<Guid> converter;

    public ConvertNodeToModelsTests()
    {
        this.extender = new ServiceCollection()
            .AddXtender<NodeToModelsConversionState<Guid>>((builder, provider) =>
            {
                builder.Default()
                    .Attach<NodeCollection<Guid>, NodeToModelsConversionNodeExtension<Guid>>()
                    .Attach<NodePropertyCollection, NodeToModelsConversionNodePropertiesExtension<Guid>>()
                    .Attach<ValueNodeProperty, NodeToModelsConversionValueNodePropertyExtension<Guid>>();
            })
            .BuildServiceProvider()
            .GetRequiredService<IExtender<NodeToModelsConversionState<Guid>>>();

        this.converter = new NodeToModelsConverter<Guid>(this.extender);
    }

    [Fact]
    public void ShouldConvert()
    {
        // Arrange
        var node = new NodeCollection<Guid>(Guid.NewGuid());
        node.Properties.Add("1", new NodePropertyCollection("1")
        {
            ["11"] = new ValueNodeProperty("11", "hello"),
            ["12"] = new ValueNodeProperty("12", "goodbye")
        });

        node.Properties.Add("2", new NodePropertyCollection("2")
        {
            ["21"] = new ValueNodeProperty("21", "Something")
        });

        var node1 = new NodeCollection<Guid>(Guid.NewGuid());
        node1.Properties.Add("A1", new ValueNodeProperty("A1", "A1-Value"));

        var node2 = new NodeCollection<Guid>(Guid.NewGuid());
        node2.Properties.Add("B1", new ValueNodeProperty("B1", "B1-Value"));

        var node3 = new NodeCollection<Guid>(Guid.NewGuid());
        node3.Properties.Add("C1", new ValueNodeProperty("C1", "C1-Value"));

        var node11 = new NodeCollection<Guid>(Guid.NewGuid());
        node11.Properties.Add("AA", new ValueNodeProperty("AA", "AA-Value"));

        var node12 = new NodeCollection<Guid>(Guid.NewGuid());
        node12.Properties.Add("AB", new ValueNodeProperty("AB", "AB-Value"));

        node1.Add(node11);
        node1.Add(node12);

        node.Add(node1);
        node.Add(node2);
        node.Add(node3);

        // Act
        var models = this.converter.Convert(node);

        // Assert
        Assert.Equal(6, models.Count);

        var nodeModel = models.FirstOrDefault(x => x.Id == node.Id);
        Assert.NotNull(nodeModel);

        Assert.True(nodeModel?.Properties["1"] is IPropertiesNodePropertyModel p1Model
            && p1Model.Properties["11"] is IValueNodePropertyModel p11Value && p11Value.Value == "hello"
            && p1Model.Properties["12"] is IValueNodePropertyModel p12Value && p12Value.Value == "goodbye");

        Assert.True(nodeModel?.Properties["2"] is IPropertiesNodePropertyModel p2Model
            && p2Model.Properties["21"] is IValueNodePropertyModel p21Model
            && p21Model.Value == "Something");

        Assert.True(new[] { node1.Id, node2.Id, node3.Id }.All(x => nodeModel?.Children.Contains(x) ?? false));

        var node1Model = models.FirstOrDefault(x => x.Id == node1.Id);
        var node2Model = models.FirstOrDefault(x => x.Id == node2.Id);
        var node3Model = models.FirstOrDefault(x => x.Id == node3.Id);

        Assert.Single(node1Model?.Properties);
        Assert.True(node1Model?.Properties["A1"] is IValueNodePropertyModel valueA1 && valueA1.Value == "A1-Value");

        Assert.Single(node2Model?.Properties);
        Assert.True(node2Model?.Properties["B1"] is IValueNodePropertyModel valueB1 && valueB1.Value == "B1-Value");

        Assert.Single(node3Model?.Properties);
        Assert.True(node3Model?.Properties["C1"] is IValueNodePropertyModel valueC1 && valueC1.Value == "C1-Value");

        var node11Model = models.FirstOrDefault(x => x.Id == node11.Id);
        var node12Model = models.FirstOrDefault(x => x.Id == node12.Id);

        Assert.Single(node11Model?.Properties);
        Assert.True(node11Model?.Properties["AA"] is IValueNodePropertyModel valueAA && valueAA.Value == "AA-Value");

        Assert.Single(node12Model?.Properties);
        Assert.True(node12Model?.Properties["AB"] is IValueNodePropertyModel valueAB && valueAB.Value == "AB-Value");
    }
}

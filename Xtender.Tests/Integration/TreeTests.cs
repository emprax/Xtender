using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xtender.DependencyInjection;
using Xtender.Trees;
using Xtender.Trees.Builders;
using Xtender.Trees.Extensions;
using Xunit;

namespace Xtender.Tests.Integration
{
    public class TreeTests
    {
        [Fact]
        public void ShouldVerifyContinuousEnumerationThroughTree()
        {
            // Arrange
            var tree = TreeBuilder.New()
                .CreateByValue("root", 1, builder => builder
                .AddByValue("1A", "value-1A", inner => inner
                    .AddByValue("1B", "value-1B")
                    .AddByValue("2B", 3))
                .AddByValue("2A", "value-2A", inner => inner
                    .AddByValue("1C", "value-1C")
                    .AddByValue("2C", 4))
                .AddByValue("3A", 2));

            // Act
            var preOrderResults = tree.PreOrder().ToArray();
            var postOrderResults = tree.PostOrder().ToArray();

            // Assert
            Assert.Equal("root", preOrderResults[0].Id);
            Assert.Equal("1A", preOrderResults[1].Id);
            Assert.Equal("1B", preOrderResults[2].Id);
            Assert.Equal("2B", preOrderResults[3].Id);
            Assert.Equal("2A", preOrderResults[4].Id);
            Assert.Equal("1C", preOrderResults[5].Id);
            Assert.Equal("2C", preOrderResults[6].Id);
            Assert.Equal("3A", preOrderResults[7].Id);

            Assert.Equal("1B", postOrderResults[0].Id);
            Assert.Equal("2B", postOrderResults[1].Id);
            Assert.Equal("1A", postOrderResults[2].Id);
            Assert.Equal("1C", postOrderResults[3].Id);
            Assert.Equal("2C", postOrderResults[4].Id);
            Assert.Equal("2A", postOrderResults[5].Id);
            Assert.Equal("3A", postOrderResults[6].Id);
            Assert.Equal("root", postOrderResults[7].Id);
        }

        [Fact]
        public async Task ShouldVerifyChildTraversal()
        {
            // Arrange
            var extender = new ServiceCollection()
                .AddXtender<uint>((b, _) => b.Default()
                    .Attach<Node<string>, NodeExtension<string>>()
                    .Attach<Node<int>, NodeExtension<int>>())
                .BuildServiceProvider()
                .GetRequiredService<IExtender<uint>>();

            var tree = TreeBuilder.New()
                .CreateByValue("root", 1, builder => builder
                .AddByValue("1A", "value-1A", inner => inner
                    .AddByValue("1B", "value-1B")
                    .AddByValue("2B", 3))
                .AddByValue("2A", "value-2A", inner => inner
                    .AddByValue("1C", "value-1C")
                    .AddByValue("2C", 4))
                .AddByValue("3A", 2));

            extender.State = 0;

            // Act
            await tree.Root.Accept(extender);

            // Assert
            Assert.Equal(8U, extender.State);
        }
    }

    public class NodeExtension<TValue> : IExtension<uint, Node<TValue>>
    {
        public async Task Extend(Node<TValue> context, IExtender<uint> extender)
        {
            extender.State++;
            foreach (var child in context.Children)
            {
                await child.Accept(extender);
            }
        }
    }
}

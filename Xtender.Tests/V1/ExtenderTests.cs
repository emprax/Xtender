using System.Reflection;
using System.Threading.Tasks;
using Moq;
using Xtender.V1;
using Xunit;

namespace Xtender.Tests.V1
{
    public class ExtenderTests
    {
        [Fact]
        public async Task ShouldCallExtentMethodAndReturnTaskWhenExtensionIsProvided()
        {
            // Arrange
            var extension = Mock.Of<IExtension<Component>>(MockBehavior.Strict);
            var extender = new Extender<Component, Node>(_ => extension);
            var node = new Node();

            Mock.Get(extension)
                .Setup(x => x.Extent(It.IsAny<Node>()))
                .Returns(Task.CompletedTask);

            // Act
            await node.Accept(extender);
            
            // Assert
            Mock.Get(extension)
                .Verify(x => x.Extent(It.IsAny<Node>()), Times.Once);
        }
        
        [Fact]
        public void ShouldReturnTaskByHandlingNullReferenceObjectWhenExtensionIsNull()
        {
            // Arrange
            var extender = new Extender<Component, Node>(_ => null);
            var node = new Node();
            
            // Act
            var result = node.Accept(extender);
            
            // Assert
            Assert.Equal(Task.CompletedTask, result);
        }
        
        public abstract class Component : IAccepter<Component>
        {
            public abstract Task Accept<TState>(IExtender<Component, TState> extender);
        }

        public class Node : Component
        {
            public override Task Accept<TState>(IExtender<Component, TState> extender) => extender.Extent(this);
        }
    }
}
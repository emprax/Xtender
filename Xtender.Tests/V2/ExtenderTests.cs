using System.Threading.Tasks;
using Moq;
using Xtender.V2;
using Xunit;

namespace Xtender.Tests.V2
{
    public class ExtenderTests
    {
        [Fact]
        public async Task ShouldCallExtentMethodAndReturnTaskWhenExtensionIsProvided()
        {
            // Arrange
            var extension = Mock.Of<IExtension<Node>>(MockBehavior.Strict);
            var defaultExtension = Mock.Of<IExtension<object>>(MockBehavior.Strict);

            var extender = new ExtenderBuilder<string>()
                .Attach(_ => extension)
                .Build(defaultExtension);
            
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
            var extender = new ExtenderBuilder<string>()
                .Attach(_ => default(IExtension<string>))
                .Build(null);
            
            var node = new Node();
            
            // Act
            var result = node.Accept(extender);
            
            // Assert
            Assert.Equal(Task.CompletedTask, result);
        }
        
        public abstract class Component : IAccepter
        {
            public abstract Task Accept<TState>(IExtender<TState> extender);
        }

        public class Node : Component
        {
            public override Task Accept<TState>(IExtender<TState> extender) => extender.Extent(this);
        }
    }
}
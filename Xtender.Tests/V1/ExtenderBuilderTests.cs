using System.Threading.Tasks;
using Moq;
using Xtender.V1;
using Xunit;

namespace Xtender.Tests.V1
{
    public class ExtenderBuilderTests
    {
        [Fact]
        public void ShouldSetupAndBuildExtenderStructure()
        {
            // Arrange
            var extension1 = Mock.Of<IExtension<Component>>(MockBehavior.Strict);
            var extension2 = Mock.Of<IExtension<Component>>(MockBehavior.Strict);
            var extension3 = Mock.Of<IExtension<Component>>(MockBehavior.Strict);
            var extension4 = Mock.Of<IExtension<Component>>(MockBehavior.Strict);

            var builder = new ExtenderBuilder<Component, string>();

            Mock.Get(extension1)
                .Setup(x => x.SetNext(It.IsAny<IExtension<Component>>()));

            // Act
            var extender = builder
                .Attach(_ => extension1)
                .Attach(_ => extension2)
                .Attach(_ => extension3)
                .Attach(_ => extension4)
                .Build();
            
            // Assert
            Assert.NotNull(extender);
            
            Mock.Get(extension1)
                .Verify(x => x.SetNext(extension2), Times.Once);

            Mock.Get(extension1)
                .Verify(x => x.SetNext(extension3), Times.Once);

            Mock.Get(extension1)
                .Verify(x => x.SetNext(extension4), Times.Once);
            
            Mock.Get(extension1)
                .Verify(x => x.SetNext(extension4), Times.Once);
        }

        public abstract class Component : IAccepter<Component>
        {
            public abstract Task Accept<TState>(IExtender<Component, TState> extender);
        }
    }
}
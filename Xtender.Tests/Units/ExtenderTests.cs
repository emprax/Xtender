using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xtender.Tests.Utilities;
using Xunit;

namespace Xtender.Tests.Units
{
    public class ExtenderTests
    {
        [Fact]
        public async Task ShouldThrowInvalidOperationExceptionWhenDefaultExtensionCannotBeFound()
        {
            // Arrange
            var extensions = new Dictionary<string, Func<object>>();
            var handler = Mock.Of<IExtenderAbstractionHandler<string>>();

            var extender = new Extender<string>(extensions, handler);
            var component = new TestItem("TEST-ITEM");

            Mock.Get(handler)
                .Setup(h => h.Handle(component, extender, extensions))
                .ReturnsAsync(false);

            // Act
            var exception = await Assert
                .ThrowsAsync<InvalidOperationException>(() => extender
                .Extend(component));

            // Assert
            Assert.Equal("Default extension could not by found.", exception.Message);
        }

        [Fact]
        public async Task ShouldUseDefaultExtensionWhenFactoryIsNull()
        {
            // Arrange
            var defaultExtension = Mock.Of<IExtension<string, object>>(MockBehavior.Strict);
            var handler = Mock.Of<IExtenderAbstractionHandler<string>>();

            var extensions = new Dictionary<string, Func<object>>(new Dictionary<string, Func<object>>
            {
                [typeof(TestItem).FullName] = null,
                [typeof(object).FullName] = () => defaultExtension
            });

            var extender = new Extender<string>(extensions, handler);
            var component = new TestItem("TEST-ITEM");

            Mock.Get(handler)
                .Setup(h => h.Handle(component, extender, extensions))
                .ReturnsAsync(false);

            Mock.Get(defaultExtension)
                .Setup(d => d.Extend(component, extender))
                .Returns(Task.CompletedTask);

            // Act & Assert
            await extender.Extend(component);
        }

        [Fact]
        public async Task ShouldUseDefaultExtensionWhenConcreteExtensionIsNull()
        {
            // Arrange
            var defaultExtension = Mock.Of<IExtension<string, object>>(MockBehavior.Strict);
            var handler = Mock.Of<IExtenderAbstractionHandler<string>>();

            var extensions = new Dictionary<string, Func<object>>(new Dictionary<string, Func<object>>
            {
                [typeof(TestItem).FullName] = () => null,
                [typeof(object).FullName] = () => defaultExtension
            });

            var extender = new Extender<string>(extensions, handler);
            var component = new TestItem("TEST-ITEM");

            Mock.Get(handler)
                .Setup(h => h.Handle(component, extender, extensions))
                .ReturnsAsync(false);

            Mock.Get(defaultExtension)
                .Setup(d => d.Extend(component, extender))
                .Returns(Task.CompletedTask);

            // Act & Assert
            await extender.Extend(component);
        }

        [Fact]
        public async Task ShouldUseDefaultExtensionWhenConcreteExtensionIsNotOfTheRightType()
        {
            // Arrange
            var concreteExtension = Mock.Of<IExtension<string, string>>(MockBehavior.Strict);
            var defaultExtension = Mock.Of<IExtension<string, object>>(MockBehavior.Strict);
            var handler = Mock.Of<IExtenderAbstractionHandler<string>>();

            var extensions = new Dictionary<string, Func<object>>(new Dictionary<string, Func<object>>
            {
                [typeof(TestItem).FullName] = () => concreteExtension,
                [typeof(object).FullName] = () => defaultExtension
            });

            var extender = new Extender<string>(extensions, handler);
            var component = new TestItem("TEST-ITEM");

            Mock.Get(handler)
                .Setup(h => h.Handle(component, extender, extensions))
                .ReturnsAsync(false);

            Mock.Get(defaultExtension)
                .Setup(d => d.Extend(component, extender))
                .Returns(Task.CompletedTask);

            // Act & Assert
            await extender.Extend(component);
        }

        [Fact]
        public async Task ShouldUseDefaultExtensionWhenConcreteExtensionCannotBeFound()
        {
            // Arrange
            var concreteExtension = Mock.Of<IExtension<string, TestCollection>>(MockBehavior.Strict);
            var defaultExtension = Mock.Of<IExtension<string, object>>(MockBehavior.Strict);
            var handler = Mock.Of<IExtenderAbstractionHandler<string>>();

            var extensions = new Dictionary<string, Func<object>>(new Dictionary<string, Func<object>>
            {
                [typeof(TestCollection).FullName] = () => concreteExtension,
                [typeof(object).FullName] = () => defaultExtension
            });

            var extender = new Extender<string>(extensions, handler);
            var component = new TestItem("TEST-ITEM");

            Mock.Get(handler)
                .Setup(h => h.Handle(component, extender, extensions))
                .ReturnsAsync(false);

            Mock.Get(defaultExtension)
                .Setup(d => d.Extend(component, extender))
                .Returns(Task.CompletedTask);

            // Act & Assert
            await extender.Extend(component);
        }

        [Fact]
        public async Task ShouldUseConcreteExtension()
        {
            // Arrange
            var concreteExtension = Mock.Of<IExtension<string, TestItem>>(MockBehavior.Strict);
            var defaultExtension = Mock.Of<IExtension<string, object>>(MockBehavior.Strict);
            var handler = Mock.Of<IExtenderAbstractionHandler<string>>();

            var extensions = new Dictionary<string, Func<object>>(new Dictionary<string, Func<object>>
            {
                [typeof(TestItem).FullName] = () => concreteExtension,
                [typeof(object).FullName] = () => defaultExtension
            });

            var extender = new Extender<string>(extensions, handler);
            var component = new TestItem("TEST-ITEM");

            Mock.Get(handler)
                .Setup(h => h.Handle(component, extender, extensions))
                .ReturnsAsync(false);

            Mock.Get(concreteExtension)
                .Setup(d => d.Extend(component, extender))
                .Returns(Task.CompletedTask);

            // Act & Assert
            await extender.Extend(component);
        }
    }
}

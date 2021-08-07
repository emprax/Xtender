using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xtender.Async;
using Xtender.Tests.Async.Utilities;
using Xunit;

namespace Xtender.Tests.Async.Units
{
    public class ExtenderTests
    {
        [Fact]
        public async Task ShouldThrowInvalidOperationExceptionWhenDefaultExtensionCannotBeFound()
        {
            // Arrange
            var extensions = new Dictionary<string, Func<ServiceFactory, IAsyncExtensionBase>>();
            var extender = new AsyncExtenderProxy<string>(proxy => new AsyncExtender<string>(new AsyncExtenderCore<string>(extensions), proxy, _ => null));
            var component = new TestItem("TEST-ITEM");

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
            var defaultExtension = Mock.Of<IAsyncExtension<string, object>>(MockBehavior.Strict);
            var extensions = new Dictionary<string, Func<ServiceFactory, IAsyncExtensionBase>>(new Dictionary<string, Func<ServiceFactory, IAsyncExtensionBase>>
            {
                [typeof(TestItem).FullName] = null,
                [typeof(object).FullName] = _ => defaultExtension
            });

            var extender = new AsyncExtenderProxy<string>(proxy => new AsyncExtender<string>(new AsyncExtenderCore<string>(extensions), proxy, _ => null));
            var component = new TestItem("TEST-ITEM");

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
            var defaultExtension = Mock.Of<IAsyncExtension<string, object>>(MockBehavior.Strict);
            var extensions = new Dictionary<string, Func<ServiceFactory, IAsyncExtensionBase>>(new Dictionary<string, Func<ServiceFactory, IAsyncExtensionBase>>
            {
                [typeof(TestItem).FullName] = _ => null,
                [typeof(object).FullName] = _ => defaultExtension
            });

            var extender = new AsyncExtenderProxy<string>(proxy => new AsyncExtender<string>(new AsyncExtenderCore<string>(extensions), proxy, _ => null));
            var component = new TestItem("TEST-ITEM");

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
            var concreteExtension = Mock.Of<IAsyncExtension<string, string>>(MockBehavior.Strict);
            var defaultExtension = Mock.Of<IAsyncExtension<string, object>>(MockBehavior.Strict);

            var extensions = new Dictionary<string, Func<ServiceFactory, IAsyncExtensionBase>>(new Dictionary<string, Func<ServiceFactory, IAsyncExtensionBase>>
            {
                [typeof(TestItem).FullName] = _ => concreteExtension,
                [typeof(object).FullName] = _ => defaultExtension
            });

            var extender = new AsyncExtenderProxy<string>(proxy => new AsyncExtender<string>(new AsyncExtenderCore<string>(extensions), proxy, _ => null));
            var component = new TestItem("TEST-ITEM");

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
            var concreteExtension = Mock.Of<IAsyncExtension<string, TestCollection>>(MockBehavior.Strict);
            var defaultExtension = Mock.Of<IAsyncExtension<string, object>>(MockBehavior.Strict);

            var extensions = new Dictionary<string, Func<ServiceFactory, IAsyncExtensionBase>>(new Dictionary<string, Func<ServiceFactory, IAsyncExtensionBase>>
            {
                [typeof(TestCollection).FullName] = _ => concreteExtension,
                [typeof(object).FullName] = _ => defaultExtension
            });

            var extender = new AsyncExtenderProxy<string>(proxy => new AsyncExtender<string>(new AsyncExtenderCore<string>(extensions), proxy, _ => null));
            var component = new TestItem("TEST-ITEM");

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
            var concreteExtension = Mock.Of<IAsyncExtension<string, TestItem>>(MockBehavior.Strict);
            var defaultExtension = Mock.Of<IAsyncExtension<string, object>>(MockBehavior.Strict);
            
            var extensions = new Dictionary<string, Func<ServiceFactory, IAsyncExtensionBase>>(new Dictionary<string, Func<ServiceFactory, IAsyncExtensionBase>>
            {
                [typeof(TestItem).FullName] = _ => concreteExtension,
                [typeof(object).FullName] = _ => defaultExtension
            });

            var extender = new AsyncExtenderProxy<string>(proxy => new AsyncExtender<string>(new AsyncExtenderCore<string>(extensions), proxy, _ => null));
            var component = new TestItem("TEST-ITEM");

            Mock.Get(concreteExtension)
                .Setup(d => d.Extend(component, extender))
                .Returns(Task.CompletedTask);

            // Act & Assert
            await extender.Extend(component);
        }
    }
}

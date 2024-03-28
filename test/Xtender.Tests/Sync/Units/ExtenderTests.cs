using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xtender.Olds;
using Xtender.Olds.Sync;
using Xtender.Tests.Sync.Utilities;
using Xunit;

namespace Xtender.Tests.Sync.Units
{
    public class ExtenderTests
    {
        [Fact]
        public void ShouldThrowInvalidOperationExceptionWhenDefaultExtensionCannotBeFound()
        {
            // Arrange
            var extensions = new Dictionary<string, Func<ServiceFactory, IExtensionBase>>();
            var extender = new ExtenderProxy<string>(proxy => new Extender<string>(new ExtenderCore<string>(extensions), proxy, _ => null));
            var component = new TestItem("TEST-ITEM");

            // Act
            var exception = Assert
                .Throws<InvalidOperationException>(() => extender
                .Extend(component));

            // Assert
            Assert.Equal("Default extension could not by found.", exception.Message);
        }

        [Fact]
        public void ShouldUseDefaultExtensionWhenFactoryIsNull()
        {
            // Arrange
            var defaultExtension = Mock.Of<IExtension<string, object>>(MockBehavior.Strict);
            var extensions = new Dictionary<string, Func<ServiceFactory, IExtensionBase>>(new Dictionary<string, Func<ServiceFactory, IExtensionBase>>
            {
                [typeof(TestItem).FullName] = null,
                [typeof(object).FullName] = _ => defaultExtension
            });

            var extender = new ExtenderProxy<string>(proxy => new Extender<string>(new ExtenderCore<string>(extensions), proxy, _ => null));
            var component = new TestItem("TEST-ITEM");

            Mock.Get(defaultExtension)
                .Setup(d => d.Extend(component, extender));

            // Act & Assert
            extender.Extend(component);
        }

        [Fact]
        public void ShouldUseDefaultExtensionWhenConcreteExtensionIsNull()
        {
            // Arrange
            var defaultExtension = Mock.Of<IExtension<string, object>>(MockBehavior.Strict);
            var extensions = new Dictionary<string, Func<ServiceFactory, IExtensionBase>>(new Dictionary<string, Func<ServiceFactory, IExtensionBase>>
            {
                [typeof(TestItem).FullName] = _ => null,
                [typeof(object).FullName] = _ => defaultExtension
            });

            var extender = new ExtenderProxy<string>(proxy => new Extender<string>(new ExtenderCore<string>(extensions), proxy, _ => null));
            var component = new TestItem("TEST-ITEM");

            Mock.Get(defaultExtension)
                .Setup(d => d.Extend(component, extender));

            // Act & Assert
            extender.Extend(component);
        }

        [Fact]
        public void ShouldUseDefaultExtensionWhenConcreteExtensionIsNotOfTheRightType()
        {
            // Arrange
            var concreteExtension = Mock.Of<IExtension<string, string>>(MockBehavior.Strict);
            var defaultExtension = Mock.Of<IExtension<string, object>>(MockBehavior.Strict);

            var extensions = new Dictionary<string, Func<ServiceFactory, IExtensionBase>>(new Dictionary<string, Func<ServiceFactory, IExtensionBase>>
            {
                [typeof(TestItem).FullName] = _ => concreteExtension,
                [typeof(object).FullName] = _ => defaultExtension
            });

            var extender = new ExtenderProxy<string>(proxy => new Extender<string>(new ExtenderCore<string>(extensions), proxy, _ => null));
            var component = new TestItem("TEST-ITEM");

            Mock.Get(defaultExtension)
                .Setup(d => d.Extend(component, extender));

            // Act & Assert
            extender.Extend(component);
        }

        [Fact]
        public void ShouldUseDefaultExtensionWhenConcreteExtensionCannotBeFound()
        {
            // Arrange
            var concreteExtension = Mock.Of<IExtension<string, TestCollection>>(MockBehavior.Strict);
            var defaultExtension = Mock.Of<IExtension<string, object>>(MockBehavior.Strict);

            var extensions = new Dictionary<string, Func<ServiceFactory, IExtensionBase>>(new Dictionary<string, Func<ServiceFactory, IExtensionBase>>
            {
                [typeof(TestCollection).FullName] = _ => concreteExtension,
                [typeof(object).FullName] = _ => defaultExtension
            });

            var extender = new ExtenderProxy<string>(proxy => new Extender<string>(new ExtenderCore<string>(extensions), proxy, _ => null));
            var component = new TestItem("TEST-ITEM");

            Mock.Get(defaultExtension)
                .Setup(d => d.Extend(component, extender));

            // Act & Assert
            extender.Extend(component);
        }

        [Fact]
        public void ShouldUseConcreteExtension()
        {
            // Arrange
            var concreteExtension = Mock.Of<IExtension<string, TestItem>>(MockBehavior.Strict);
            var defaultExtension = Mock.Of<IExtension<string, object>>(MockBehavior.Strict);
            
            var extensions = new Dictionary<string, Func<ServiceFactory, IExtensionBase>>(new Dictionary<string, Func<ServiceFactory, IExtensionBase>>
            {
                [typeof(TestItem).FullName] = _ => concreteExtension,
                [typeof(object).FullName] = _ => defaultExtension
            });

            var extender = new ExtenderProxy<string>(proxy => new Extender<string>(new ExtenderCore<string>(extensions), proxy, _ => null));
            var component = new TestItem("TEST-ITEM");

            Mock.Get(concreteExtension)
                .Setup(d => d.Extend(component, extender));

            // Act & Assert
            extender.Extend(component);
        }
    }
}

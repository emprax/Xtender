using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace Xtender.Tests.Units
{
    public class ExtenderFactoryTests
    {
        private readonly IExtenderCore<string> core;
        private readonly IExtenderFactory<string, string> factory;

        public ExtenderFactoryTests()
        {
            this.core = Mock.Of<IExtenderCore<string>>(MockBehavior.Strict);
            this.factory = new ExtenderFactory<string, string>(new ConcurrentDictionary<string, Func<IExtenderCore<string>>>(new[]
            {
                new KeyValuePair<string, Func<IExtenderCore<string>>>("TEST-KEY", () => this.core)
            }));
        }

        [Fact]
        public void ShouldReturnNullWhenNotFound()
        {
            // Act & Assert
            Assert.Null(this.factory.Create("HELLO"));
        }

        [Fact]
        public void ShouldReturnExtenderCore()
        {
            // Arrange
            Mock.Get(this.core)
                .SetupGet(c => c.Provider)
                .Returns(new Dictionary<string, Func<object>>());

            Mock.Get(this.core)
                .SetupGet(c => c.Handler)
                .Returns(new ExtenderAbstractionHandler<string>(false));

            // Act 
            var core = this.factory.Create("TEST-KEY");

            // Assert
            Assert.NotNull(core);
        }
    }
}

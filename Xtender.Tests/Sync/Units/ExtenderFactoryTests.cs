using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Moq;
using Xtender.Sync;
using Xunit;

namespace Xtender.Tests.Sync.Units
{
    public class ExtenderFactoryTests
    {
        private readonly IExtenderCore<string> core;
        private readonly IExtenderFactory<string, string> factory;

        public ExtenderFactoryTests()
        {
            this.core = Mock.Of<IExtenderCore<string>>(MockBehavior.Strict);
            this.factory = new ExtenderFactory<string, string>(
                new ExtenderFactoryCore<string, string>(new ConcurrentDictionary<string, Func<IExtenderCore>>(new[]
                {
                    new KeyValuePair<string, Func<IExtenderCore>>("TEST-KEY", () => this.core)
                })),
                () => null);
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
            var extension = Mock.Of<IExtension<string, string>>(MockBehavior.Strict);

            Mock.Get(extension)
                .Setup(e => e.Extend("GO!", It.IsAny<IExtender<string>>()))
                .Callback<string, IExtender<string>>((v, x) =>
                {
                    x.State += $"testable!! By {v}";
                });

            Mock.Get(this.core)
                .Setup(c => c.GetExtensionType<string>())
                .Returns(p => extension);

            // Act 
            var extender = this.factory.Create("TEST-KEY");
            extender.State = "Something ";

            "GO!".Accept(extender);

            // Assert
            Assert.Equal("Something testable!! By GO!", extender.State);
        }
    }
}

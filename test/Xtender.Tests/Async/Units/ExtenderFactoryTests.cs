using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xtender.Async;
using Xtender.Olds;
using Xtender.Olds.Async;
using Xunit;

namespace Xtender.Tests.Async.Units
{
    public class ExtenderFactoryTests
    {
        private readonly IAsyncExtenderCore<string> core;
        private readonly IAsyncExtenderFactory<string, string> factory;

        public ExtenderFactoryTests()
        {
            this.core = Mock.Of<IAsyncExtenderCore<string>>(MockBehavior.Strict);
            this.factory = new AsyncExtenderFactory<string, string>(
                new AsyncExtenderFactoryCore<string, string>(new ConcurrentDictionary<string, Func<IAsyncExtenderCore>>(new[]
                {
                    new KeyValuePair<string, Func<IAsyncExtenderCore>>("TEST-KEY", () => this.core)
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
        public async Task ShouldReturnAsyncExtenderCore()
        {
            // Arrange
            var asyncExtension = Mock.Of<IAsyncExtension<string, string>>(MockBehavior.Strict);

            Mock.Get(asyncExtension)
                .Setup(e => e.Extend("GO!", It.IsAny<IAsyncExtender<string>>()))
                .Callback<string, IAsyncExtender<string>>((v, x) =>
                {
                    x.State += $"testable!! By {v}";
                })
                .Returns(Task.CompletedTask);

            Mock.Get(this.core)
                .Setup(c => c.GetExtensionType<string>())
                .Returns(p => asyncExtension);

            // Act 
            var asyncExtender = this.factory.Create("TEST-KEY");
            asyncExtender.State = "Something ";

            await "GO!".Accept(asyncExtender);

            // Assert
            Assert.Equal("Something testable!! By GO!", asyncExtender.State);
        }
    }
}

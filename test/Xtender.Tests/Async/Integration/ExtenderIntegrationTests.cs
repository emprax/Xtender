using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xtender.DependencyInjection.Old.Async;
using Xtender.Olds.Async;
using Xtender.Tests.Async.Utilities;
using Xunit;

namespace Xtender.Tests.Async.Integration
{
    public class ExtenderIntegrationTests
    {
        private readonly IServiceProvider provider;
        private readonly LoggerMock<TestComponent> logger;

        public ExtenderIntegrationTests()
        {
            this.logger = new LoggerMock<TestComponent>();

            this.provider = new ServiceCollection()
                .AddTransient<ILogger<TestComponent>>(_ => this.logger)
                .AddAsyncXtender<string>((bldr, _) =>
                {
                    bldr.Default()
                        .Attach<TestItem, Test1ExtensionBase>()
                        .Attach<TestCollection, Test2ExtensionBase>();
                })
                .BuildServiceProvider();
        }

        [Fact]
        public async Task ShouldHandleVisitingAbstractComponent()
        {
            // Arrange
            TestComponent component = new TestItem("HELLO");
            var extender = this.provider.GetRequiredService<IAsyncExtender<string>>();

            // Act
            await extender.Extend(component);

            // Assert
            this.logger.Verify(LogLevel.Information, 1, "HELLO");
        }

        [Fact]
        public async Task ShouldProcessChain()
        {
            // Arrange
            var extender = this.provider.GetRequiredService<IAsyncExtender<string>>();
            var structure = new TestCollection("TEST-1", new List<TestComponent>
            {
                new TestItem("TEST-1-1"),
                new TestCollection("TEST-1-2", new List<TestComponent>()
                { 
                    new TestItem("TEST-2-1"),
                    new TestItem("TEST-2-2")
                })
            });

            // Act
            await structure.Accept(extender);

            // Assert
            this.logger.VerifyTimes(LogLevel.Information, 5);
            this.logger.VerifyContains(LogLevel.Information, "TEST-1");
            this.logger.VerifyContains(LogLevel.Information, "TEST-1-1");
            this.logger.VerifyContains(LogLevel.Information, "TEST-1-2");
            this.logger.VerifyContains(LogLevel.Information, "TEST-2-1");
            this.logger.VerifyContains(LogLevel.Information, "TEST-2-2");
        }
    }
}

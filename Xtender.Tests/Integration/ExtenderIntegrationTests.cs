using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xtender.DependencyInjection;
using Xtender.Tests.Utilities;
using Xunit;

namespace Xtender.Tests.Integration
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
                .AddXtender<string>((bldr, _) =>
                {
                    bldr.WithAbstractAccepterHandling()
                        .Default()
                        .Attach<TestItem, Test1Extension>()
                        .Attach<TestCollection, Test2Extension>();
                })
                .BuildServiceProvider();
        }

        [Fact]
        public async Task ShouldHandleVisitingAbstractComponent()
        {
            // Arrange
            TestComponent component = new TestItem("HELLO");
            var extender = this.provider.GetRequiredService<IExtender<string>>();

            // Act
            await extender.Extend(component);

            // Assert
            this.logger.Verify(LogLevel.Information, 1, "HELLO");
        }

        [Fact]
        public async Task ShouldProcessChain()
        {
            // Arrange
            var extender = this.provider.GetRequiredService<IExtender<string>>();
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

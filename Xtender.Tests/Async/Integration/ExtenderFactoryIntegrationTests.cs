using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xtender.Async;
using Xtender.DependencyInjection.Async;
using Xtender.Tests.Async.Utilities;
using Xunit;

namespace Xtender.Tests.Async.Integration
{
    public class ExtenderFactoryIntegrationTests
    {
        private readonly IServiceProvider provider;
        private readonly LoggerMock<TestComponent> logger;

        public ExtenderFactoryIntegrationTests()
        {
            this.logger = new LoggerMock<TestComponent>();

            this.provider = new ServiceCollection()
                .AddTransient<ILogger<TestComponent>>(_ => this.logger)
                .AddAsyncXtenderFactory<string, string>((builder, _) =>
                {
                    builder
                        .WithExtender("TEST1", bldr =>
                        {
                            bldr.Default()
                                .Attach<TestItem, Test1ExtensionBase>()
                                .Attach<TestCollection, Test2ExtensionBase>();
                        })
                        .WithExtender("TEST2", bldr =>
                        {
                            bldr.Default<CustomDefaultExtensionBase>()
                                .Attach<TestCollection, Test2ExtensionBase>();
                        });
                })
                .BuildServiceProvider();
        }

        [Fact]
        public async Task ShouldProcessChainTest1Successfully()
        {
            // Arrange
            var factory = this.provider.GetRequiredService<IAsyncExtenderFactory<string, string>>();
            var extender = factory.Create("TEST1");

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

        [Fact]
        public async Task ShouldProcessChainTest2IncludingDefaults()
        {
            // Arrange
            var factory = this.provider.GetRequiredService<IAsyncExtenderFactory<string, string>>();
            var extender = factory.Create("TEST2");

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
            this.logger.VerifyTimes(LogLevel.Information, 2);
            this.logger.VerifyTimes(LogLevel.Error, 3);

            this.logger.VerifyContains(LogLevel.Error, "DEFAULT!!");
            this.logger.VerifyContains(LogLevel.Information, "TEST-1");
            this.logger.VerifyContains(LogLevel.Information, "TEST-1-2");
        }
    }
}

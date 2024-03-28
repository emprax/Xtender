using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xtender.DependencyInjection.Old.Sync;
using Xtender.Olds.Sync;
using Xtender.Tests.Sync.Utilities;
using Xunit;

namespace Xtender.Tests.Sync.Integration
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
                .AddXtenderFactory<string, string>((builder, _) =>
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
        public void ShouldProcessChainTest1Successfully()
        {
            // Arrange
            var factory = this.provider.GetRequiredService<IExtenderFactory<string, string>>();
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
            structure.Accept(extender);

            // Assert
            this.logger.VerifyTimes(LogLevel.Information, 5);
            this.logger.VerifyContains(LogLevel.Information, "TEST-1");
            this.logger.VerifyContains(LogLevel.Information, "TEST-1-1");
            this.logger.VerifyContains(LogLevel.Information, "TEST-1-2");
            this.logger.VerifyContains(LogLevel.Information, "TEST-2-1");
            this.logger.VerifyContains(LogLevel.Information, "TEST-2-2");
        }

        [Fact]
        public void ShouldProcessChainTest2IncludingDefaults()
        {
            // Arrange
            var factory = this.provider.GetRequiredService<IExtenderFactory<string, string>>();
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
            structure.Accept(extender);

            // Assert
            this.logger.VerifyTimes(LogLevel.Information, 2);
            this.logger.VerifyTimes(LogLevel.Error, 3);

            this.logger.VerifyContains(LogLevel.Error, "DEFAULT!!");
            this.logger.VerifyContains(LogLevel.Information, "TEST-1");
            this.logger.VerifyContains(LogLevel.Information, "TEST-1-2");
        }
    }
}

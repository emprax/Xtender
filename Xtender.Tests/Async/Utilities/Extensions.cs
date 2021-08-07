using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Xtender.Async;

namespace Xtender.Tests.Async.Utilities
{
    public class Test1ExtensionBase : IAsyncExtension<string, TestItem>
    {
        private readonly ILogger<TestComponent> logger;

        public Test1ExtensionBase(ILogger<TestComponent> logger) => this.logger = logger;

        public Task Extend(TestItem context, IAsyncExtender<string> extender)
        {
            this.logger.LogInformation(context.Value);
            return Task.CompletedTask;
        }
    }

    public class Test2ExtensionBase : IAsyncExtension<string, TestCollection>
    {
        private readonly ILogger<TestComponent> logger;

        public Test2ExtensionBase(ILogger<TestComponent> logger) => this.logger = logger;

        public async Task Extend(TestCollection context, IAsyncExtender<string> extender)
        {
            this.logger.LogInformation(context.Value);
            foreach (var component in context.Components)
            {
                await component.Accept(extender);
            }
        }
    }

    public class CustomDefaultExtensionBase : IAsyncExtension<string, object>
    {
        private readonly ILogger<TestComponent> logger;

        public CustomDefaultExtensionBase(ILogger<TestComponent> logger) => this.logger = logger;

        public Task Extend(object context, IAsyncExtender<string> extender)
        {
            this.logger.LogError("DEFAULT!!");
            return Task.CompletedTask;
        }
    }
}

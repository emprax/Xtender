using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Xtender.Tests.Utilities
{
    public class Test1ExtensionBase : IExtension<string, TestItem>
    {
        private readonly ILogger<TestComponent> logger;

        public Test1ExtensionBase(ILogger<TestComponent> logger) => this.logger = logger;

        public Task Extend(TestItem context, IExtender<string> extender)
        {
            this.logger.LogInformation(context.Value);
            return Task.CompletedTask;
        }
    }

    public class Test2ExtensionBase : IExtension<string, TestCollection>
    {
        private readonly ILogger<TestComponent> logger;

        public Test2ExtensionBase(ILogger<TestComponent> logger) => this.logger = logger;

        public async Task Extend(TestCollection context, IExtender<string> extender)
        {
            this.logger.LogInformation(context.Value);
            foreach (var component in context.Components)
            {
                await component.Accept(extender);
            }
        }
    }

    public class CustomDefaultExtensionBase : IExtension<string, object>
    {
        private readonly ILogger<TestComponent> logger;

        public CustomDefaultExtensionBase(ILogger<TestComponent> logger) => this.logger = logger;

        public Task Extend(object context, IExtender<string> extender)
        {
            this.logger.LogError("DEFAULT!!");
            return Task.CompletedTask;
        }
    }
}

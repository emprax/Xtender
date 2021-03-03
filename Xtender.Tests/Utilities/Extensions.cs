using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Xtender.Tests.Utilities
{
    public class Test1Extension : IExtension<string, TestItem>
    {
        private readonly ILogger<TestComponent> logger;

        public Test1Extension(ILogger<TestComponent> logger) => this.logger = logger;

        public Task Extend(TestItem context, IExtender<string> extender)
        {
            this.logger.LogInformation(context.Value);
            return Task.CompletedTask;
        }
    }

    public class Test2Extension : IExtension<string, TestCollection>
    {
        private readonly ILogger<TestComponent> logger;

        public Test2Extension(ILogger<TestComponent> logger) => this.logger = logger;

        public async Task Extend(TestCollection context, IExtender<string> extender)
        {
            this.logger.LogInformation(context.Value);
            foreach (var component in context.Components)
            {
                await component.Accept(extender);
            }
        }
    }

    public class CustomDefaultExtension : IExtension<string, object>
    {
        private readonly ILogger<TestComponent> logger;

        public CustomDefaultExtension(ILogger<TestComponent> logger) => this.logger = logger;

        public Task Extend(object context, IExtender<string> extender)
        {
            this.logger.LogError("DEFAULT!!");
            return Task.CompletedTask;
        }
    }
}

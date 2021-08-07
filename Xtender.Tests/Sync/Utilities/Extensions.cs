using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Xtender.Sync;

namespace Xtender.Tests.Sync.Utilities
{
    public class Test1ExtensionBase : IExtension<string, TestItem>
    {
        private readonly ILogger<TestComponent> logger;

        public Test1ExtensionBase(ILogger<TestComponent> logger) => this.logger = logger;

        public void Extend(TestItem context, IExtender<string> extender)
        {
            this.logger.LogInformation(context.Value);
        }
    }

    public class Test2ExtensionBase : IExtension<string, TestCollection>
    {
        private readonly ILogger<TestComponent> logger;

        public Test2ExtensionBase(ILogger<TestComponent> logger) => this.logger = logger;

        public void Extend(TestCollection context, IExtender<string> extender)
        {
            this.logger.LogInformation(context.Value);
            foreach (var component in context.Components)
            {
                component.Accept(extender);
            }
        }
    }

    public class CustomDefaultExtensionBase : IExtension<string, object>
    {
        private readonly ILogger<TestComponent> logger;

        public CustomDefaultExtensionBase(ILogger<TestComponent> logger) => this.logger = logger;

        public void Extend(object context, IExtender<string> extender)
        {
            this.logger.LogError("DEFAULT!!");
        }
    }
}

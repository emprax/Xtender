using System.Threading.Tasks;
using Xtender.Olds.Async;

namespace Xtender.Example.Console.Async.Extensions
{
    public class StatedCompositeExtensionBase : IAsyncExtension<string, Composite>
    {
        public async Task Extend(Composite context, IAsyncExtender<string> extender)
        {
            System.Console.WriteLine("Entered CompositeExtensionBase");
            if (extender.State is null)
            {
                extender.State = "Encountered an Composite regarding Component.";
            }
            else
            {
                extender.State += "Encountered an Composite regarding Component.";
            }

            foreach (var component in context.Components)
            {
                await component.Accept(extender);
            }
        }
    }
}
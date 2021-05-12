using System.Threading.Tasks;

namespace Xtender.Example.Console.Extensions
{
    public class StatedCompositeExtensionBase : IExtension<string, Composite>
    {
        public async Task Extend(Composite context, IExtender<string> extender)
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
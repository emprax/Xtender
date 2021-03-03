using System.Threading.Tasks;

namespace Xtender.Example.Console
{
    public class CompositeExtension : IExtension<string, Composite>
    {
        public async Task Extend(Composite context, IExtender<string> extender)
        {
            System.Console.WriteLine("Entered CompositeExtension");
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
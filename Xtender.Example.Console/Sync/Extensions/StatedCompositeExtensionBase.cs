using Xtender.Sync;

namespace Xtender.Example.Console.Sync.Extensions
{
    public class StatedCompositeExtensionBase : IExtension<string, Composite>
    {
        public void Extend(Composite context, IExtender<string> extender)
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
                component.Accept(extender);
            }
        }
    }
}
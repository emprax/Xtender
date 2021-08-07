using Xtender.Sync;

namespace Xtender.Example.Console.Sync.Extensions
{
    public class StatedItemExtensionBase : IExtension<string, Item>
    {
        public void Extend(Item _, IExtender<string> extender)
        {
            System.Console.WriteLine("Entered ItemExtensionBase");
            if (extender.State is null)
            {
                extender.State = "Encountered an Item regarding Component.";
                return;
            }

            extender.State += "Encountered an Item regarding Component.";
        }
    }
}
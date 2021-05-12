using System.Threading.Tasks;

namespace Xtender.Example.Console.Extensions
{
    public class StatedItemExtensionBase : IExtension<string, Item>
    {
        public Task Extend(Item _, IExtender<string> extender)
        {
            System.Console.WriteLine("Entered ItemExtensionBase");
            if (extender.State is null)
            {
                extender.State = "Encountered an Item regarding Component.";
                return Task.CompletedTask;
            }

            extender.State += "Encountered an Item regarding Component.";
            return Task.CompletedTask;
        }
    }
}
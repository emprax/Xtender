using System.Threading.Tasks;

namespace Xtender.Example.Console
{
    public class ItemExtension : IExtension<string, Item>
    {
        public Task Extent(Item _, IExtender<string> extender)
        {
            System.Console.WriteLine("Entered ItemExtension");
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
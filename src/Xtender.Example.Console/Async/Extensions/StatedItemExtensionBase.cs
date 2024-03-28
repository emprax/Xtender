using System.Threading.Tasks;
using Xtender.Olds.Async;

namespace Xtender.Example.Console.Async.Extensions
{
    public class StatedItemExtensionBase : IAsyncExtension<string, Item>
    {
        public Task Extend(Item _, IAsyncExtender<string> extender)
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
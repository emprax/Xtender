using System.Threading.Tasks;
using Xtender.Async;

namespace Xtender.Example.Console.Async.Extensions
{
    public class ItemExtensionBase : IAsyncExtension<Item>
    {
        public Task Extend(Item _, IAsyncExtender extender)
        {
            System.Console.WriteLine("Entered ItemExtensionBase");
            return Task.CompletedTask;
        }
    }
}
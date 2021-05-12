using System.Threading.Tasks;

namespace Xtender.Example.Console.Extensions
{
    public class ItemExtensionBase : IExtension<Item>
    {
        public Task Extend(Item _, IExtender extender)
        {
            System.Console.WriteLine("Entered ItemExtensionBase");
            return Task.CompletedTask;
        }
    }
}
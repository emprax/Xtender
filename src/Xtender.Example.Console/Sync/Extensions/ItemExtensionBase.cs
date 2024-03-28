using Xtender.Olds.Sync;

namespace Xtender.Example.Console.Sync.Extensions
{
    public class ItemExtensionBase : IExtension<Item>
    {
        public void Extend(Item _, IExtender extender)
        {
            System.Console.WriteLine("Entered ItemExtensionBase");
        }
    }
}
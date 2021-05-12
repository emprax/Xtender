using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xtender.Example.Console.Extensions
{
    public partial class StatedFactoryCase
    {
        public class ListedItemExtensionBase : IExtension<IList<string>, Item>
        {
            public Task Extend(Item context, IExtender<IList<string>> extender)
            {
                extender.State.Add(" - Welcome to the Item-extension.");
                return Task.CompletedTask;
            }
        }
    }

    public partial class FactoryCase
    {
        public class ListedItemExtensionBase : IExtension<Item>
        {
            public Task Extend(Item context, IExtender extender)
            {
                System.Console.WriteLine(" - Welcome to the Item-extension.");
                return Task.CompletedTask;
            }
        }
    }
}

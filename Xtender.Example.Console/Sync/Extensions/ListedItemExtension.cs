using System.Collections.Generic;
using Xtender.Sync;

namespace Xtender.Example.Console.Sync.Extensions
{
    public partial class StatedFactoryCase
    {
        public class ListedItemExtensionBase : IExtension<IList<string>, Item>
        {
            public void Extend(Item context, IExtender<IList<string>> extender)
            {
                extender.State.Add(" - Welcome to the Item-extension.");
            }
        }
    }

    public partial class FactoryCase
    {
        public class ListedItemExtensionBase : IExtension<Item>
        {
            public void Extend(Item context, IExtender extender)
            {
                System.Console.WriteLine(" - Welcome to the Item-extension.");
            }
        }
    }
}

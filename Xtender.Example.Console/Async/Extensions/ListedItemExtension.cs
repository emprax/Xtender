using System.Collections.Generic;
using System.Threading.Tasks;
using Xtender.Async;

namespace Xtender.Example.Console.Async.Extensions
{
    public partial class StatedFactoryCase
    {
        public class ListedItemExtensionBase : IAsyncExtension<IList<string>, Item>
        {
            public Task Extend(Item context, IAsyncExtender<IList<string>> extender)
            {
                extender.State.Add(" - Welcome to the Item-extension.");
                return Task.CompletedTask;
            }
        }
    }

    public partial class FactoryCase
    {
        public class ListedItemExtensionBase : IAsyncExtension<Item>
        {
            public Task Extend(Item context, IAsyncExtender extender)
            {
                System.Console.WriteLine(" - Welcome to the Item-extension.");
                return Task.CompletedTask;
            }
        }
    }
}

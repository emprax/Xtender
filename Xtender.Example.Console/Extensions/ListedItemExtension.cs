using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xtender.Example.Console
{
    public partial class FactoryCase
    {
        public class ListedItemExtension : IExtension<IList<string>, Item>
        {
            public Task Extent(Item context, IExtender<IList<string>> extender)
            {
                extender.State.Add(" - Welcome to the Item-extension.");
                return Task.CompletedTask;
            }
        }
    }
}

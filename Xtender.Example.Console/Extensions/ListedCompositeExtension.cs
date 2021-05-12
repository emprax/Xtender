using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xtender.Example.Console.Extensions
{
    public partial class StatedFactoryCase
    {
        public class ListedCompositeExtensionBase : IExtension<IList<string>, Composite>
        {
            public async Task Extend(Composite context, IExtender<IList<string>> extender)
            {
                extender.State.Add(" - Welcome to the Composite-extension.");
                foreach (var component in context.Components)
                {
                    await component.Accept(extender);
                }
            }
        }
    }

    public partial class FactoryCase
    {
        public class ListedCompositeExtensionBase : IExtension<Composite>
        {
            public async Task Extend(Composite context, IExtender extender)
            {
                System.Console.WriteLine(" - Welcome to the Composite-extension.");
                foreach (var component in context.Components)
                {
                    await component.Accept(extender);
                }
            }
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xtender.Example.Console
{
    public partial class FactoryCase
    {
        public class ListedCompositeExtension : IExtension<IList<string>, Composite>
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
}

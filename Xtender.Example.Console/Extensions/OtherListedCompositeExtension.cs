using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xtender.Example.Console.Extensions
{
    public partial class StatedFactoryCase
    {
        public class OtherListedCompositeExtensionBase : IExtension<IList<string>, Composite>
        {
            public async Task Extend(Composite context, IExtender<IList<string>> extender)
            {
                extender.State.Add(" - This is something else ;).");
                foreach (var component in context.Components)
                {
                    await component.Accept(extender);
                }
            }
        }
    }

    public partial class FactoryCase
    {
        public class OtherListedCompositeExtensionBase : IExtension<Composite>
        {
            public async Task Extend(Composite context, IExtender extender)
            {
                System.Console.WriteLine(" - This is something else ;).");
                foreach (var component in context.Components)
                {
                    await component.Accept(extender);
                }
            }
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xtender.Example.Console
{
    public partial class FactoryCase
    {
        public class OtherListedCompositeExtension : IExtension<IList<string>, Composite>
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
}

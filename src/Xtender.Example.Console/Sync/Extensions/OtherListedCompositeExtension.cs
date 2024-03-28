using System.Collections.Generic;
using System.Threading.Tasks;
using Xtender.Olds.Sync;

namespace Xtender.Example.Console.Sync.Extensions
{
    public partial class StatedFactoryCase
    {
        public class OtherListedCompositeExtensionBase : IExtension<IList<string>, Composite>
        {
            public void Extend(Composite context, IExtender<IList<string>> extender)
            {
                extender.State.Add(" - This is something else ;).");
                foreach (var component in context.Components)
                {
                    component.Accept(extender);
                }
            }
        }
    }

    public partial class FactoryCase
    {
        public class OtherListedCompositeExtensionBase : IExtension<Composite>
        {
            public void Extend(Composite context, IExtender extender)
            {
                System.Console.WriteLine(" - This is something else ;).");
                foreach (var component in context.Components)
                {
                    component.Accept(extender);
                }
            }
        }
    }
}

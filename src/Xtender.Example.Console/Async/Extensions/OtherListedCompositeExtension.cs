using System.Collections.Generic;
using System.Threading.Tasks;
using Xtender.Olds.Async;

namespace Xtender.Example.Console.Async.Extensions
{
    public partial class StatedFactoryCase
    {
        public class OtherListedCompositeExtensionBase : IAsyncExtension<IList<string>, Composite>
        {
            public async Task Extend(Composite context, IAsyncExtender<IList<string>> extender)
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
        public class OtherListedCompositeExtensionBase : IAsyncExtension<Composite>
        {
            public async Task Extend(Composite context, IAsyncExtender extender)
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

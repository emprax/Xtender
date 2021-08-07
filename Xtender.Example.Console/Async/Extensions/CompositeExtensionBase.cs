using System.Threading.Tasks;
using Xtender.Async;

namespace Xtender.Example.Console.Async.Extensions
{
    public class CompositeExtensionBase : IAsyncExtension<Composite>
    {
        public async Task Extend(Composite context, IAsyncExtender extender)
        {
            System.Console.WriteLine("Entered CompositeExtensionBase");
            foreach (var component in context.Components)
            {
                await component.Accept(extender);
            }
        }
    }
}
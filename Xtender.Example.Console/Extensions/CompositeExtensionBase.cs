using System.Threading.Tasks;

namespace Xtender.Example.Console.Extensions
{
    public class CompositeExtensionBase : IExtension<Composite>
    {
        public async Task Extend(Composite context, IExtender extender)
        {
            System.Console.WriteLine("Entered CompositeExtensionBase");
            foreach (var component in context.Components)
            {
                await component.Accept(extender);
            }
        }
    }
}
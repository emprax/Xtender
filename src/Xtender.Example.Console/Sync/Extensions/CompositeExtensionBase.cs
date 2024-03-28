using Xtender.Olds.Sync;

namespace Xtender.Example.Console.Sync.Extensions
{
    public class CompositeExtensionBase : IExtension<Composite>
    {
        public void Extend(Composite context, IExtender extender)
        {
            System.Console.WriteLine("Entered CompositeExtensionBase");
            foreach (var component in context.Components)
            {
                component.Accept(extender);
            }
        }
    }
}
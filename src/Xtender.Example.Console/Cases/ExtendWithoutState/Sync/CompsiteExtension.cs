using Xtender.Sync;

namespace Xtender.Example.Console.Cases.ExtendWithoutState.Sync;

internal class CompsiteExtension : IExtension<Composite>
{
    public void Extend(Composite context, IExtender extender)
    {
        System.Console.Out.WriteLine($"\t - Entered composite '{context.Name}'");
        foreach (var (_, element) in context.Elements)
        {
            element.Accept(extender);
        }
    }
}

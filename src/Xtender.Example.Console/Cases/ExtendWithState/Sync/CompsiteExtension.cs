using Xtender.Sync;

namespace Xtender.Example.Console.Cases.ExtendWithState.Sync;

internal class CompsiteExtension : IExtension<int, Composite>
{
    public void Extend(Composite context, IExtender<int> extender)
    {
        System.Console.Out.WriteLine($"\t - Entered composite '{context.Name}'. Number: '{extender.State}'.");
        foreach (var (_, element) in context.Elements)
        {
            element.Accept(extender);
        }
    }
}

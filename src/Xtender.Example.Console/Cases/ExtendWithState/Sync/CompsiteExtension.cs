using Xtender.Sync;

namespace Xtender.Example.Console.Cases.ExtendWithState.Sync;

internal class CompsiteExtension : IExtension<State, Composite>
{
    public void Extend(Composite context, IExtender<State> extender)
    {
        extender.State.Messages.Add($"\t - Entered composite '{context.Name}'");
        foreach (var (_, element) in context.Elements)
        {
            element.Accept(extender);
        }
    }
}

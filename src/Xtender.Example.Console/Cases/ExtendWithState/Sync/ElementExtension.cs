using Xtender.Sync;

namespace Xtender.Example.Console.Cases.ExtendWithState.Sync;

internal class ElementExtension : IExtension<State, Element>
{
    public void Extend(Element context, IExtender<State> extender) => extender.State.Messages.Add($"\t - Entered element '{context.Name}'");
}
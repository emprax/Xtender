using Xtender.Sync;

namespace Xtender.Example.Console.Cases.ExtendWithState.Sync;

internal class ElementExtension : IExtension<int, Element>
{
    public void Extend(Element context, IExtender<int> extender) => System.Console.Out.WriteLine($"\t - Entered element '{context.Name}'. Number: '{extender.State}'.");
}
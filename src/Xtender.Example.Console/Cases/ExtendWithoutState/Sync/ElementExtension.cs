using Xtender.Sync;

namespace Xtender.Example.Console.Cases.ExtendWithoutState.Sync;

internal class ElementExtension : IExtension<Element>
{
    public void Extend(Element context, IExtender extender) => System.Console.Out.WriteLine($"\t - Entered element '{context.Name}'");
}
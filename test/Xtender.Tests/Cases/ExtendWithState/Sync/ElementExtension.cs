using Xtender.Sync;

namespace Xtender.Tests.Cases.ExtendWithState.Sync;

internal class ElementExtension(Writer writer) : IExtension<int, Element>
{
    public void Extend(Element context, IExtender<int> extender) => writer.Write($"#{extender.State}:{context.Name}");
}
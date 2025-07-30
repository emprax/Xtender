using Xtender.Sync;

namespace Xtender.Tests.Cases.ExtendWithoutState.Sync;

internal class ElementExtension(Writer writer) : IExtension<Element>
{
    public void Extend(Element context, IExtender extender) => writer.Write(context.Name);
}
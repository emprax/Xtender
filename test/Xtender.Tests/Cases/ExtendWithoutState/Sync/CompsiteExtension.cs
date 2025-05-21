using Xtender.Sync;

namespace Xtender.Tests.Cases.ExtendWithoutState.Sync;

internal class CompsiteExtension(Writer writer) : IExtension<Composite>
{
    public void Extend(Composite context, IExtender extender)
    {
        writer.Write(context.Name);
        foreach (var (_, element) in context.Elements)
        {
            element.Accept(extender);
        }
    }
}

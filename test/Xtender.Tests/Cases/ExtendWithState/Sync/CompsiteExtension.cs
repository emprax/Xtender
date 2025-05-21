using Xtender.Sync;

namespace Xtender.Tests.Cases.ExtendWithState.Sync;

internal class CompsiteExtension(Writer writer) : IExtension<int, Composite>
{
    public void Extend(Composite context, IExtender<int> extender)
    {
        writer.Write($"#{extender.State}:{context.Name}");
        foreach (var (_, element) in context.Elements)
        {
            element.Accept(extender);
        }
    }
}

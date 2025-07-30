using System.Threading.Tasks;
using Xtender.Async;

namespace Xtender.Tests.Cases.ExtendWithState.Async;

internal class CompsiteExtension(Writer writer) : IAsyncExtension<int, Composite>
{
    public async Task Extend(Composite context, IAsyncExtender<int> extender)
    {
        writer.Write($"#{extender.State}:{context.Name}");
        foreach (var (_, element) in context.Elements)
        {
            await element.Accept(extender);
        }
    }
}

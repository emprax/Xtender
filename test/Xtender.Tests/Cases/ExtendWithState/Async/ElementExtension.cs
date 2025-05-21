using System.Threading.Tasks;
using Xtender.Async;

namespace Xtender.Tests.Cases.ExtendWithState.Async;

internal class ElementExtension(Writer writer) : IAsyncExtension<int, Element>
{
    public Task Extend(Element context, IAsyncExtender<int> extender)
    {
        writer.Write($"#{extender.State}:{context.Name}");
        return Task.CompletedTask;
    }
}
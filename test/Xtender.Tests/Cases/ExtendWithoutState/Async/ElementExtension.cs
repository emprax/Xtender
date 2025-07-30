using System.Threading.Tasks;
using Xtender.Async;

namespace Xtender.Tests.Cases.ExtendWithoutState.Async;

internal class ElementExtension(Writer writer) : IAsyncExtension<Element>
{
    public Task Extend(Element context, IAsyncExtender extender)
    {
        writer.Write(context.Name);
        return Task.CompletedTask;
    }
}
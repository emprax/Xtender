using System.Threading.Tasks;
using Xtender.Async;

namespace Xtender.Tests.Cases.ExtendWithoutState.Async;

internal class CompsiteExtension(Writer writer) : IAsyncExtension<Composite>
{
    public async Task Extend(Composite context, IAsyncExtender extender)
    {
        writer.Write(context.Name);
        foreach (var (_, element) in context.Elements)
        {
            await element.Accept(extender);
        }
    }
}

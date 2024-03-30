using System.Threading.Tasks;
using Xtender.Async;

namespace Xtender.Example.Console.Cases.ExtendWithoutState.Async;

internal class CompsiteExtension : IAsyncExtension<Composite>
{
    public async Task Extend(Composite context, IAsyncExtender extender)
    {
        await System.Console.Out.WriteLineAsync($"\t - Entered composite '{context.Name}'");
        foreach (var (_, element) in context.Elements)
        {
            await element.Accept(extender);
        }
    }
}

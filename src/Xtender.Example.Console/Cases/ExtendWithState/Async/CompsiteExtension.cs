using System.Threading.Tasks;
using Xtender.Async;

namespace Xtender.Example.Console.Cases.ExtendWithState.Async;

internal class CompsiteExtension : IAsyncExtension<int, Composite>
{
    public async Task Extend(Composite context, IAsyncExtender<int> extender)
    {
        await System.Console.Out.WriteLineAsync($"\t - Entered composite '{context.Name}'. Number: '{extender.State}'.");
        foreach (var (_, element) in context.Elements)
        {
            await element.Accept(extender);
        }
    }
}

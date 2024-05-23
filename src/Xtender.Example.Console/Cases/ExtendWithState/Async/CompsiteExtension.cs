using System.Threading.Tasks;
using Xtender.Async;

namespace Xtender.Example.Console.Cases.ExtendWithState.Async;

internal class CompsiteExtension : IAsyncExtension<State, Composite>
{
    public async Task Extend(Composite context, IAsyncExtender<State> extender)
    {
        extender.State.Messages.Add($"\t - Entered composite '{context.Name}'");
        foreach (var (_, element) in context.Elements)
        {
            await element.Accept(extender);
        }
    }
}

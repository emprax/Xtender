using System.Threading.Tasks;
using Xtender.Async;

namespace Xtender.Example.Console.Cases.ExtendWithState.Async;

internal class ElementExtension : IAsyncExtension<State, Element>
{
    public Task Extend(Element context, IAsyncExtender<State> extender)
    {
        extender.State.Messages.Add($"\t - Entered element '{context.Name}'");
        return Task.CompletedTask;
    }
}
using System.Threading.Tasks;
using Xtender.Async;

namespace Xtender.Example.Console.Cases.ExtendWithState.Async;

internal class ElementExtension : IAsyncExtension<int, Element>
{
    public Task Extend(Element context, IAsyncExtender<int> extender) => System.Console.Out.WriteLineAsync($"\t - Entered element '{context.Name}'. Number: '{extender.State}'.");
}
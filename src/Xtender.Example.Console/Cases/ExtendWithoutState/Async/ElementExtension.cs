using System.Threading.Tasks;
using Xtender.Async;

namespace Xtender.Example.Console.Cases.ExtendWithoutState.Async;

internal class ElementExtension : IAsyncExtension<Element>
{
    public Task Extend(Element context, IAsyncExtender extender) => System.Console.Out.WriteLineAsync($"\t - Entered element '{context.Name}'");
}
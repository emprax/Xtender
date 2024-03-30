using Xtender.Async;
using Xtender.Sync;

namespace Xtender.Example.Console.Cases;

internal interface IElement : IAccepter, IAsyncAccepter
{
    string Name { get; }
}

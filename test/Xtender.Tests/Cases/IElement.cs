using Xtender.Async;
using Xtender.Sync;

namespace Xtender.Tests.Cases;

internal interface IElement : IAccepter, IAsyncAccepter
{
    string Name { get; }
}

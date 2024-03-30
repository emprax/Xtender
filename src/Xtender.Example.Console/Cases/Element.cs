using System;
using System.Threading.Tasks;
using Xtender.Async;
using Xtender.Sync;

namespace Xtender.Example.Console.Cases;

internal record Element(string Name) : IElement
{
    public void Accept(IExtender extender) => extender.Extend(this);

    public Task Accept(IAsyncExtender extender) => extender.Extend(this);
}

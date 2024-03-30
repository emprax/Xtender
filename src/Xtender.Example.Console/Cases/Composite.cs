using System.Collections.Generic;
using System.Threading.Tasks;
using Xtender.Async;
using Xtender.Sync;

namespace Xtender.Example.Console.Cases;

internal class Composite : IElement
{
    private readonly Dictionary<string, IElement> elements;

    public Composite(string name) : this(name, new Dictionary<string, IElement>()) { }

    public Composite(string name, Dictionary<string, IElement> elements)
    {
        this.elements = elements;
        this.Name = name;
    }

    public string Name { get; }

    public IReadOnlyDictionary<string, IElement> Elements => this.elements;

    public void Accept(IExtender extender) => extender.Extend(this);

    public Task Accept(IAsyncExtender extender) => extender.Extend(this);
}

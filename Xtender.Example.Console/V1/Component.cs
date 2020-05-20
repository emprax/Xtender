using System.Collections.Generic;
using System.Threading.Tasks;
using Xtender.V1;

namespace Xtender.Example.Console.V1
{
    public abstract class Component : IAccepter<Component>
    {
        public abstract Task Accept<TState>(IExtender<Component, TState> extender);
    }

    public class Item : Component
    {
        public override Task Accept<TState>(IExtender<Component, TState> extender) => extender.Extent(this);
    }

    public class Composite : Component
    {
        public IList<Component> Components { get; set; }

        public override Task Accept<TState>(IExtender<Component, TState> extender) => extender.Extent(this);
    }
}
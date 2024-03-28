using System.Collections.Generic;
using Xtender.Olds.Sync;

namespace Xtender.Example.Console.Sync
{
    public abstract class Component : IAccepter
    {
        public abstract void Accept(IExtender extender);
    }
    
    public abstract class Accepter<TSelf> : Component where TSelf : Component
    {
        public override void Accept(IExtender extender) => extender.Extend(this as TSelf);
    }

    public class Item : Accepter<Item> { }

    public class Composite : Accepter<Composite>
    {
        public IList<Component> Components { get; set; }
    }
}
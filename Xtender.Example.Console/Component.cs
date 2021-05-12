using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xtender.Example.Console
{
    public abstract class Component : IAccepter
    {
        public abstract Task Accept(IExtender extender);
    }
    
    public abstract class Accepter<TSelf> : Component where TSelf : Component
    {
        public override Task Accept(IExtender extender) => extender.Extend(this as TSelf);
    }

    public class Item : Accepter<Item> { }

    public class Composite : Accepter<Composite>
    {
        public IList<Component> Components { get; set; }
    }
}
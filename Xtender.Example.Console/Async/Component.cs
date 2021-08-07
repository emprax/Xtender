using System.Collections.Generic;
using System.Threading.Tasks;
using Xtender.Async;

namespace Xtender.Example.Console.Async
{
    public abstract class Component : IAsyncAccepter
    {
        public abstract Task Accept(IAsyncExtender extender);
    }
    
    public abstract class Accepter<TSelf> : Component where TSelf : Component
    {
        public override Task Accept(IAsyncExtender extender) => extender.Extend(this as TSelf);
    }

    public class Item : Accepter<Item> { }

    public class Composite : Accepter<Composite>
    {
        public IList<Component> Components { get; set; }
    }
}
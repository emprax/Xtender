using System.Collections.Generic;
using System.Threading.Tasks;
using Xtender.Olds.Async;

namespace Xtender.Tests.Async.Utilities
{
    public abstract class TestComponent : IAsyncAccepter
    {
        protected TestComponent(string value) => this.Value = value;

        public string Value { get; }
        
        public abstract Task Accept(IAsyncExtender extender);
    }

    public abstract class Accepter<TSelf> : TestComponent where TSelf : TestComponent
    {
        protected Accepter(string value) : base(value) { }
        
        public override Task Accept(IAsyncExtender extender) => extender.Extend(this as TSelf);
    }

    public class TestItem : Accepter<TestItem>
    {
        public TestItem(string value) : base(value) { }
    }

    public class TestCollection : Accepter<TestCollection>
    {
        public IReadOnlyCollection<TestComponent> Components { get; }

        public TestCollection(string value, IReadOnlyCollection<TestComponent> components) : base(value)
            => this.Components = components;
    }
}

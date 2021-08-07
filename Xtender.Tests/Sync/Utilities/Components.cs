using System.Collections.Generic;
using Xtender.Sync;

namespace Xtender.Tests.Sync.Utilities
{
    public abstract class TestComponent : IAccepter
    {
        protected TestComponent(string value) => this.Value = value;

        public string Value { get; }
        
        public abstract void Accept(IExtender extender);
    }

    public abstract class Accepter<TSelf> : TestComponent where TSelf : TestComponent
    {
        protected Accepter(string value) : base(value) { }
        
        public override void Accept(IExtender extender) => extender.Extend(this as TSelf);
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

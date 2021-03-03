using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xtender.Tests.Utilities
{
    public abstract class TestComponent : IAccepter
    {
        protected TestComponent(string value) => this.Value = value;

        public string Value { get; }

        public abstract Task Accept<TState>(IExtender<TState> extender);
    }

    public class TestItem : TestComponent
    {
        public TestItem(string value) : base(value) { }

        public override Task Accept<TState>(IExtender<TState> extender) => extender.Extend(this);
    }

    public class TestCollection : TestComponent
    {
        public IReadOnlyCollection<TestComponent> Components { get; }

        public TestCollection(string value, IReadOnlyCollection<TestComponent> components) : base(value)
        {
            this.Components = components;
        }

        public override Task Accept<TState>(IExtender<TState> extender) => extender.Extend(this);
    }
}

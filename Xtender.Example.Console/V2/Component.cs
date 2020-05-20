﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Xtender.V2;

namespace Xtender.Example.Console.V2
{
    public abstract class Component : IAccepter
    {
        public abstract Task Accept<TState>(IExtender<TState> extender);
    }

    public class Item : Component
    {
        public override Task Accept<TState>(IExtender<TState> extender) => extender.Extent(this);
    }

    public class Composite : Component
    {
        public IList<Component> Components { get; set; }

        public override Task Accept<TState>(IExtender<TState> extender) => extender.Extent(this);
    }
}
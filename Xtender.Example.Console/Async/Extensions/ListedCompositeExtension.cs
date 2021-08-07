﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Xtender.Async;

namespace Xtender.Example.Console.Async.Extensions
{
    public partial class StatedFactoryCase
    {
        public class ListedCompositeExtensionBase : IAsyncExtension<IList<string>, Composite>
        {
            public async Task Extend(Composite context, IAsyncExtender<IList<string>> extender)
            {
                extender.State.Add(" - Welcome to the Composite-extension.");
                foreach (var component in context.Components)
                {
                    await component.Accept(extender);
                }
            }
        }
    }

    public partial class FactoryCase
    {
        public class ListedCompositeExtensionBase : IAsyncExtension<Composite>
        {
            public async Task Extend(Composite context, IAsyncExtender extender)
            {
                System.Console.WriteLine(" - Welcome to the Composite-extension.");
                foreach (var component in context.Components)
                {
                    await component.Accept(extender);
                }
            }
        }
    }
}

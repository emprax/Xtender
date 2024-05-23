using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Xtender.DependencyInjection;
using Xtender.Sync;

namespace Xtender.Example.Console.Cases.ExtendWithoutState.Sync;

internal class SyncExtendWithoutState
{
    public static void Execute()
    {
        var extender = new ServiceCollection()
            .AddXtender(builder => builder
                .Attach<Element, ElementExtension>()
                .Attach<Composite, CompsiteExtension>())
            .BuildServiceProvider()
            .GetRequiredService<IExtender>();

        System.Console.Out.WriteLine("Case: SyncExtendWithoutState");
        var item = new Composite("Level 1", new Dictionary<string, IElement>
        {
            ["Level 2.A"] = new Composite("Level 2.A", new Dictionary<string, IElement>
            {
                ["Level 3.A.1"] = new Element("Level 3.A.1"),
                ["Level 3.A.2"] = new Element("Level 3.A.2"),
                ["Level 3.A.3"] = new Element("Level 3.A.3")
            }),
            ["Level 2.B"] = new Composite("Level 2.B", new Dictionary<string, IElement>
            {
                ["Level 3.B.1"] = new Element("Level 3.B.1"),
                ["Level 3.B.2"] = new Element("Level 3.B.2"),
                ["Level 3.B.3"] = new Element("Level 3.B.3")
            }),
            ["Level 2.C"] = new Composite("Level 2.C", new Dictionary<string, IElement>
            {
                ["Level 3.C.1"] = new Element("Level 3.C.1"),
                ["Level 3.C.2"] = new Element("Level 3.C.2"),
                ["Level 3.C.3"] = new Element("Level 3.C.3")
            })
        });

        item.Accept(extender);
    }
}

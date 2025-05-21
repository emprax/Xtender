using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Xtender.DependencyInjection;
using Xtender.Sync;

namespace Xtender.Tests.Cases.ExtendWithState.Sync;

internal class SyncExtendWithState
{
    public static void Execute(Writer writer)
    {
        var extender = new ServiceCollection()
            .AddXtender<int>(builder => builder
                .Attach<Element, ElementExtension>()
                .Attach<Composite, CompsiteExtension>())
            .AddSingleton(writer)
            .BuildServiceProvider()
            .GetRequiredService<IExtender<int>>();

        extender.State = 123;

        System.Console.Out.WriteLine("Case: SyncExtendWithState");
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

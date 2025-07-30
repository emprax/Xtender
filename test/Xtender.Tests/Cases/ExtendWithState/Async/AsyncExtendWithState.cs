using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xtender.Async;
using Xtender.DependencyInjection;
using Xtender.Tests.Cases;

namespace Xtender.Tests.Cases.ExtendWithState.Async;

internal class AsyncExtendWithState
{
    public static async Task Execute(Writer writer)
    {
        var extender = new ServiceCollection()
            .AddAsyncXtender<int>(builder => builder
                .Attach<Element, ElementExtension>()
                .Attach<Composite, CompsiteExtension>())
            .AddSingleton(writer)
            .BuildServiceProvider()
            .GetRequiredService<IAsyncExtender<int>>();

        extender.State = 123;

        await System.Console.Out.WriteLineAsync("Case: AsyncExtendWithState");
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

        await item.Accept(extender);
    }
}

using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Xtender.DependencyInjection.Sync;
using Xtender.Example.Console.Sync.Extensions;
using Xtender.Sync;

namespace Xtender.Example.Console.Sync
{
    public class ExtenderTestCase
    {
        public void Execute()
        {
            var services = new ServiceCollection()
                .AddXtender((builder, provider) =>
                {
                    builder
                        .Default()
                        .Attach<Composite, CompositeExtensionBase>()
                        .Attach<Item, ItemExtensionBase>();
                })
                .BuildServiceProvider();

            var service = services.GetRequiredService<IExtender>();
            var composite = new Composite
            {
                Components = new List<Component>
                {
                    new Item(),
                    new Item(),
                    new Composite
                    {
                        Components = new List<Component>
                        {
                            new Item(),
                            new Item(),
                            new Item()
                        }
                    }
                }
            };
            
            composite.Accept(service);
        }
    }
}

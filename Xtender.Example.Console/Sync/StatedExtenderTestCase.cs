using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Xtender.DependencyInjection.Sync;
using Xtender.Example.Console.Sync.Extensions;
using Xtender.Sync;

namespace Xtender.Example.Console.Sync
{
    public class StatedExtenderTestCase
    {
        public string Execute()
        {
            var services = new ServiceCollection()
                .AddXtender<string>((builder, provider) =>
                {
                    builder
                        .Default()
                        .Attach<Composite, StatedCompositeExtensionBase>()
                        .Attach<Item, StatedItemExtensionBase>();
                })
                .BuildServiceProvider();

            var service = services.GetRequiredService<IExtender<string>>();
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
            BonusExecute();

            return service.State;
        }

        private static void BonusExecute()
        {
            System.Console.WriteLine("\n\nBONUS:\n");

            var services = new ServiceCollection()
                .AddXtender<string>((builder, provider) =>
                {
                    builder
                        .Default()
                        .Attach<Composite, StatedCompositeExtensionBase>()
                        .Attach<Item, ItemExtensionBase>();
                })
                .BuildServiceProvider();

            var service = services.GetRequiredService<IExtender<string>>();
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
            System.Console.WriteLine(service.State);
        }
    }
}

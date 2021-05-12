using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xtender.DependencyInjection;
using Xtender.Example.Console.Extensions;

namespace Xtender.Example.Console
{
    public class StatedExtenderTestCase
    {
        public async Task<string> Execute()
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
            
            await composite.Accept(service);
            await BonusExecute();

            return service.State;
        }

        private static async Task BonusExecute()
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

            await composite.Accept(service);
            System.Console.WriteLine(service.State);
        }
    }
}

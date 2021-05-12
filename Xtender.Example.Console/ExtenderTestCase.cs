using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xtender.DependencyInjection;
using Xtender.Example.Console.Extensions;

namespace Xtender.Example.Console
{
    public class ExtenderTestCase
    {
        public async Task Execute()
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
            
            await composite.Accept(service);
        }
    }
}

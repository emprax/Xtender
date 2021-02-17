using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xtender.DependencyInjection;

namespace Xtender.Example.Console
{
    public class ExtenderTestCase
    {
        public async Task<string> Execute()
        {
            var services = new ServiceCollection()
                .AddXtender<string>((builder, provider) =>
                {
                    builder
                        .Default()
                        .Attach<Composite, CompositeExtension>()
                        .Attach<Item, ItemExtension>();
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
            return service.State;
        }
    }
}

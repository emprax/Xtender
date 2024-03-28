using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xtender.DependencyInjection.Old.Async;
using Xtender.Example.Console.Async.Extensions;
using Xtender.Olds.Async;

namespace Xtender.Example.Console.Async
{
    public class AsyncExtenderTestCase
    {
        public async Task Execute()
        {
            var services = new ServiceCollection()
                .AddAsyncXtender((builder, provider) =>
                {
                    builder
                        .Default()
                        .Attach<Composite, CompositeExtensionBase>()
                        .Attach<Item, ItemExtensionBase>();
                })
                .BuildServiceProvider();

            var service = services.GetRequiredService<IAsyncExtender>();
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

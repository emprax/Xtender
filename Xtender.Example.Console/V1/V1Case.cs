using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xtender.DependencyInjection.V1;
using Xtender.V1;

namespace Xtender.Example.Console.V1
{
    public class V1Case
    {
        public async Task<string> Execute()
        {
            var services = new ServiceCollection()
                .AddXtender<Component, string>((builder, provider) =>
                {
                    return builder
                        .Attach(extender => new ItemExtension(extender))
                        .Attach(extender => new CompositeExtension(extender))
                        .Build();
                })
                .BuildServiceProvider();

            var service = services.GetRequiredService<IExtender<Component, string>>();
            await service.Extent(new Composite
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
            });

            return service.State;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xtender.DependencyInjection.V2;
using Xtender.V2;

namespace Xtender.Example.Console.V2
{
    public class V2Case
    {
        public async Task<string> Execute()
        {
            var services = new ServiceCollection()
                .AddXtender<string>((builder, provider) =>
                {
                    return builder
                        .Attach(extender => new CompositeExtension(extender))
                        .Attach(extender => new ItemExtension(extender))
                        .Build(null);
                })
                .BuildServiceProvider();

            var service = services.GetRequiredService<IExtender<string>>();
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

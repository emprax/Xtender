using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xtender.DependencyInjection;

namespace Xtender.Example.Console
{
    public partial class FactoryCase
    {
        public async Task Execute()
        {
            var provider = new ServiceCollection()
                .AddXtenderFactory<string, IList<string>>((builder, provider) =>
                {
                    builder.WithExtender("first-part", bldr => 
                    { 
                        bldr.Default()
                            .Attach<Item, ListedItemExtension>()
                            .Attach<Composite, ListedCompositeExtension>();
                    });

                    builder.WithExtender("second-part", bldr => 
                    { 
                        bldr.Default()
                            .Attach<Item, ListedItemExtension>()
                            .Attach<Composite, OtherListedCompositeExtension>();
                    });
                })
                .BuildServiceProvider();

            var context = new Composite
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

            var factory = provider.GetRequiredService<IExtenderFactory<string, IList<string>>>();

            System.Console.WriteLine("\nFirst part:");
            await Execute(factory.Create("first-part"), context);

            System.Console.WriteLine("\nSecond part:");
            await Execute(factory.Create("second-part"), context);
        }

        private static async Task Execute(IExtender<IList<string>> extender, Component context)
        {
            extender.State = new List<string>();

            await context.Accept(extender);
            foreach (var message in extender.State)
            {
                System.Console.WriteLine(message);
            }
        }
    }
}

using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Xtender.DependencyInjection.Old.Sync;
using Xtender.Olds.Sync;

namespace Xtender.Example.Console.Sync
{
    public class FactoryCase
    {
        public void Execute()
        {
            var provider = new ServiceCollection()
                .AddXtenderFactory<string, IList<string>>((builder, provider) =>
                {
                    builder.WithExtender("first-part", bldr =>
                    {
                        bldr.Default()
                            .Attach<Item, Extensions.FactoryCase.ListedItemExtensionBase>()
                            .Attach<Composite, Extensions.FactoryCase.ListedCompositeExtensionBase>();
                    });

                    builder.WithExtender("second-part", bldr =>
                    {
                        bldr.Default()
                            .Attach<Item, Extensions.StatedFactoryCase.ListedItemExtensionBase>()
                            .Attach<Composite, Extensions.FactoryCase.OtherListedCompositeExtensionBase>();
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
            Execute(factory.Create("first-part"), context);

            System.Console.WriteLine("\nSecond part:");
            Execute(factory.Create("second-part"), context);
        }

        private static void Execute(IExtender<IList<string>> extender, Component context)
        {
            extender.State = new List<string>();

            context.Accept(extender);
            foreach (var message in extender.State)
            {
                System.Console.WriteLine(message);
            }
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Xtender.DependencyInjection.Sync;
using Xtender.Sync;

namespace Xtender.Example.Console.Sync
{
    public static class RandomObjectAccepterTest
    {
        public static void Run()
        {
            var extender = new ServiceCollection()
                .AddXtender((builder, _) => builder.Default().Attach<int, IntegerExtension>())
                .BuildServiceProvider()
                .GetService(typeof(IExtender)) as IExtender;

            var integer = 1;
            System.Console.WriteLine($"Value is: {integer}.");

            integer.Accept(extender);
            System.Console.WriteLine($"Value is: {++integer}.");
        }

        public class IntegerExtension : IExtension<int>
        {
            public void Extend(int context, IExtender extender)
            {
                System.Console.WriteLine($"Value is: {++context}.");
            }
        }
    }
}

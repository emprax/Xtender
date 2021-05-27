using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xtender.DependencyInjection;

namespace Xtender.Example.Console
{
    public static class RandomObjectAccepterTest
    {
        public static async Task Run()
        {
            var extender = new ServiceCollection()
                .AddXtender((builder, _) => builder.Default().Attach<int, IntegerExtension>())
                .BuildServiceProvider()
                .GetService(typeof(IExtender)) as IExtender;

            var integer = 1;
            System.Console.WriteLine($"Value is: {integer}.");

            await integer.Accept(extender);
            System.Console.WriteLine($"Value is: {++integer}.");
        }

        public class IntegerExtension : IExtension<int>
        {
            public Task Extend(int context, IExtender extender)
            {
                System.Console.WriteLine($"Value is: {++context}.");
                return Task.CompletedTask;
            }
        }
    }
}

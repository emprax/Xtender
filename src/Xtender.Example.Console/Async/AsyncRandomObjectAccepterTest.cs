using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xtender.DependencyInjection.Old.Async;
using Xtender.Olds;
using Xtender.Olds.Async;

namespace Xtender.Example.Console.Async
{
    public static class AsyncRandomObjectAccepterTest
    {
        public static async Task Run()
        {
            var extender = new ServiceCollection()
                .AddAsyncXtender((builder, _) => builder.Default().Attach<int, IntegerExtension>())
                .BuildServiceProvider()
                .GetService(typeof(IAsyncExtender)) as IAsyncExtender;

            var integer = 1;
            System.Console.WriteLine($"Value is: {integer}.");

            await integer.Accept(extender);
            System.Console.WriteLine($"Value is: {++integer}.");
        }

        public class IntegerExtension : IAsyncExtension<int>
        {
            public Task Extend(int context, IAsyncExtender extender)
            {
                System.Console.WriteLine($"Value is: {++context}.");
                return Task.CompletedTask;
            }
        }
    }
}

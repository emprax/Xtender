using System.Threading.Tasks;
using Xtender.Example.Console.Async;
using Xtender.Example.Console.Sync;

namespace Xtender.Example.Console
{
    public class Program
    {
        public static async Task Main(string[] _)
        {
            System.Console.WriteLine("EXTENDER -> SYNC TEST-CASES:\n");
            SyncTests();

            System.Console.WriteLine("\n\n---------------------------------------------------\n\n");

            System.Console.WriteLine("EXTENDER -> ASYNC TEST-CASES:\n");
            await AsyncTests();

            System.Console.WriteLine("\n\nFINISHED CASES.");
        }

        private static void SyncTests()
        {
            System.Console.WriteLine("Extender:");
            var caseResults = new StatedExtenderTestCase().Execute();

            System.Console.WriteLine();
            System.Console.WriteLine(caseResults);
            System.Console.WriteLine();
            System.Console.WriteLine();

            new ExtenderTestCase().Execute();

            System.Console.WriteLine();
            System.Console.WriteLine();

            new StatedFactoryCase().Execute();

            System.Console.WriteLine();
            System.Console.WriteLine();

            new FactoryCase().Execute();
            RandomObjectAccepterTest.Run();
        }

        private static async Task AsyncTests()
        {
            System.Console.WriteLine("Async Extender:");
            var caseResults = await new AsyncStatedExtenderTestCase().Execute();

            System.Console.WriteLine();
            System.Console.WriteLine(caseResults);
            System.Console.WriteLine();
            System.Console.WriteLine();

            await new AsyncExtenderTestCase().Execute();

            System.Console.WriteLine();
            System.Console.WriteLine();

            await new AsyncStatedFactoryCase().Execute();

            System.Console.WriteLine();
            System.Console.WriteLine();

            await new AsyncFactoryCase().Execute();
            await AsyncRandomObjectAccepterTest.Run();
        }
    }
}

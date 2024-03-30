using System.Threading.Tasks;
using Xtender.Example.Console.Cases.ExtendWithoutState.Async;

namespace Xtender.Example.Console
{
    public class Program
    {
        public static async Task Main(string[] _)
        {
            System.Console.WriteLine("EXTENDER -> SYNC TEST-CASES:\n\n");

            System.Console.WriteLine("\n\n---------------------------------------------------\n\n");

            System.Console.WriteLine("EXTENDER -> ASYNC TEST-CASES:\n\n");

            await AsyncExtendWithoutState.Execute();

            System.Console.WriteLine("\n\nFINISHED CASES.");
        }
    }
}

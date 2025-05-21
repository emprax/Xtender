using System.Threading.Tasks;
using Xtender.Example.Console.Cases.ExtendWithoutState.Async;
using Xtender.Example.Console.Cases.ExtendWithoutState.Sync;
using Xtender.Example.Console.Cases.ExtendWithState.Async;
using Xtender.Example.Console.Cases.ExtendWithState.Sync;

namespace Xtender.Example.Console
{
    public class Program
    {
        public static async Task Main(string[] _)
        {
            System.Console.WriteLine("EXTENDER -> SYNC TEST-CASES:\n\n");

            SyncExtendWithoutState.Execute();
            SyncExtendWithState.Execute();

            System.Console.WriteLine("\n\n---------------------------------------------------\n\n");

            System.Console.WriteLine("EXTENDER -> ASYNC TEST-CASES:\n\n");

            await AsyncExtendWithoutState.Execute();
            await AsyncExtendWithState.Execute();

            System.Console.WriteLine("\n\nFINISHED CASES.");
        }
    }
}

using System.Threading.Tasks;
using Xtender.Example.Console.V1;
using Xtender.Example.Console.V2;

namespace Xtender.Example.Console
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var v1Case = new V1Case();
            var v2Case = new V2Case();

            System.Console.WriteLine("V1:");
            var caseV1Result = await v1Case.Execute();
            System.Console.WriteLine();
            System.Console.WriteLine(caseV1Result);
            System.Console.WriteLine();

            System.Console.WriteLine("V2:");
            var caseV2Result = await v2Case.Execute();
            System.Console.WriteLine();
            System.Console.WriteLine(caseV2Result);
            System.Console.WriteLine();
        }
    }
}

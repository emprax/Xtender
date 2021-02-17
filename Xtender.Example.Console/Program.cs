using System.Threading.Tasks;

namespace Xtender.Example.Console
{
    public class Program
    {
        public static async Task Main(string[] _)
        {
            System.Console.WriteLine("Extender:");

            //var caseResults = await new ExtenderTestCase().Execute();
            
            //System.Console.WriteLine();
            //System.Console.WriteLine(caseResults);
            //System.Console.WriteLine();

            await new FactoryCase().Execute();
        }
    }
}

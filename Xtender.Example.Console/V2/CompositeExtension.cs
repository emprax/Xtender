using System.Threading.Tasks;
using Xtender.V2;

namespace Xtender.Example.Console.V2
{
    public class CompositeExtension : IExtension<Composite>
    {
        private readonly IExtender<string> extender;

        public CompositeExtension(IExtender<string> extender)
        {
            this.extender = extender;
        }

        public async Task Extent(Composite context)
        {
            System.Console.WriteLine("Entered CompositeExtension");
            if (this.extender.State is null)
            {
                this.extender.State = "Encountered an Composite regarding Component.";
            }
            else
            {
                this.extender.State += "Encountered an Composite regarding Component.";
            }

            foreach (var component in context.Components)
            {
                await component.Accept(this.extender);
            }
        }
    }
}
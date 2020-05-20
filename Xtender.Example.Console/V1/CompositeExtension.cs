using System.Threading.Tasks;
using Xtender.v1;
using Xtender.V1;

namespace Xtender.Example.Console.V1
{
    public class CompositeExtension : Extension<string, Component, Composite>
    {
        public CompositeExtension(IExtender<Component, string> extender) : base(extender) { }

        protected override async Task Extent(Composite context)
        {
            System.Console.WriteLine("Entered CompositeExtension");
            if (base.extender.State is null)
            {
                base.extender.State = "Encountered an Composite regarding Component.";
            }
            else
            {
                base.extender.State += "Encountered an Composite regarding Component.";
            }

            foreach (var component in context.Components)
            {
                await component.Accept(base.extender);
            }
        }
    }
}
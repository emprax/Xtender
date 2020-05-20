using System.Threading.Tasks;
using Xtender.v1;
using Xtender.V1;

namespace Xtender.Example.Console.V1
{
    public class ItemExtension : Extension<string, Component, Item>
    {
        public ItemExtension(IExtender<Component, string> extender) : base(extender) { }

        protected override Task Extent(Item context)
        {
            System.Console.WriteLine("Entered ItemExtension");
            if (base.extender.State is null)
            {
                base.extender.State = "Encountered an Item regarding Component.";
            }
            else
            {
                base.extender.State += "Encountered an Item regarding Component.";
            }

            return Task.CompletedTask;
        }
    }
}
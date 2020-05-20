using System.Threading.Tasks;
using Xtender.V2;

namespace Xtender.Example.Console.V2
{
    public class ItemExtension : IExtension<Item>
    {
        private readonly IExtender<string> extender;

        public ItemExtension(IExtender<string> extender)
        {
            this.extender = extender;
        }

        public Task Extent(Item context)
        {
            System.Console.WriteLine("Entered ItemExtension");
            if (this.extender.State is null)
            {
                this.extender.State = "Encountered an Item regarding Component.";
            }
            else
            {
                this.extender.State += "Encountered an Item regarding Component.";
            }

            return Task.CompletedTask;
        }
    }
}
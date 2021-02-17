using System.Threading.Tasks;

namespace Xtender
{
    public class DefaultExtension<TState> : IExtension<TState, object>
    {
        public Task Extent(object context, IExtender<TState> extender) => Task.CompletedTask;
    }
}

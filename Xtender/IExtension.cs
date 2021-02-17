using System.Threading.Tasks;

namespace Xtender
{
    public interface IExtension<TState, in TContext>
    {
        Task Extent(TContext context, IExtender<TState> extender);
    }
}
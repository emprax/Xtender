using System.Threading.Tasks;

namespace Xtender.V1
{
    public interface IAccepter<out TBaseValue>
    {
        Task Accept<TState>(IExtender<TBaseValue, TState> extender);
    }
}
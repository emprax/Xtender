using System.Threading.Tasks;

namespace Xtender.V1
{
    public interface IAccepter<out TBaseValue> where TBaseValue : IAccepter<TBaseValue>
    {
        Task Accept<TState>(IExtender<TBaseValue, TState> extender);
    }
}
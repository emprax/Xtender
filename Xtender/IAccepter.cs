using System.Threading.Tasks;

namespace Xtender
{
    public interface IAccepter
    {
        Task Accept<TState>(IExtender<TState> extender);
    }
}
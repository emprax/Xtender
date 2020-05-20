using System.Threading.Tasks;

namespace Xtender.V2
{
    public interface IAccepter
    {
        Task Accept<TState>(IExtender<TState> extender);
    }
}
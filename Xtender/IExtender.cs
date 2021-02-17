using System.Threading.Tasks;

namespace Xtender
{
    public interface IExtender<TState>
    {
        TState State { get; set; }

        Task Extent<TAccepter>(TAccepter accepter) where TAccepter : class, IAccepter;
    }
}
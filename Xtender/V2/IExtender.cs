using System.Threading.Tasks;

namespace Xtender.V2
{
    public interface IExtender<TState>
    {
        TState State { get; set; }

        Task Extent<TAccepter>(TAccepter accepter) where TAccepter : IAccepter;
    }
}
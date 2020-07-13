using System.Threading.Tasks;

namespace Xtender.V1
{
    public interface IExtender<in TBaseValue, TState> where TBaseValue : IAccepter<TBaseValue>
    {
        TState State { get; set; }

        Task Extent(TBaseValue value);
    }
}
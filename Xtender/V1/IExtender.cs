using System.Threading.Tasks;

namespace Xtender.V1
{
    public interface IExtender<in TBaseValue, TState>
    {
        TState State { get; set; }

        Task Extent(TBaseValue value);
    }
}
using System.Threading.Tasks;

namespace Xtender.V1
{
    public interface IExtension<TBaseValue> where TBaseValue : IAccepter<TBaseValue>
    {
        Task Extent(TBaseValue value);

        void SetNext(IExtension<TBaseValue> segment);
    }
}
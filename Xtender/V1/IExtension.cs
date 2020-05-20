using System.Threading.Tasks;

namespace Xtender.V1
{
    public interface IExtension<TBaseValue>
    {
        Task Extent(TBaseValue value);

        void SetNext(IExtension<TBaseValue> segment);
    }
}
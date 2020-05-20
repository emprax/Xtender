using System.Threading.Tasks;

namespace Xtender.V2
{
    public interface IExtension { }

    public interface IExtension<in TContext> : IExtension
    {
        Task Extent(TContext context);
    }
}
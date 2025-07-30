using System.Threading.Tasks;

namespace Xtender.Async;

public interface IAsyncAccepter
{
    Task Accept(IAsyncExtender extender);
}

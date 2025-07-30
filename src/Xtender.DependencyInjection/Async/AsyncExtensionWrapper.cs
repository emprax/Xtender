using System.Threading.Tasks;
using Xtender.Async;

namespace Xtender.DependencyInjection.Async;

internal class AsyncExtensionWrapper<TState, TContext>(IAsyncExtension<TContext> extension) : IAsyncExtension<TState, TContext>
{
    public Task Extend(TContext context, IAsyncExtender<TState> extender) => extension.Extend(context, extender);
}

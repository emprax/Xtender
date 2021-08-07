using Xtender.Async;
using Xtender.Sync;

namespace Xtender
{
    public interface IUnitExtension<in TContext> : IExtensionBase, IAsyncExtensionBase
    {
        void Extend(TContext context);
    }

    public interface IUnitExtension<in TState, in TContext> : IExtensionBase, IAsyncExtensionBase
    {
        void Extend(TContext context, TState state);
    }
}

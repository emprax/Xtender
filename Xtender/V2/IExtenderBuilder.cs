using System;

namespace Xtender.V2
{
    public interface IExtenderBuilder<TState>
    {
        IExtenderBuilder<TState> Attach<TContext>(Func<IExtender<TState>, IExtension<TContext>> configuration);

        IExtender<TState> Build(IExtension<object> defaultExtension);
    }
}

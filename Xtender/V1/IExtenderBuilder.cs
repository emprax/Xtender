using System;

namespace Xtender.V1
{
    public interface IExtenderBuilder<TBaseValue, TState> where TBaseValue : IAccepter<TBaseValue>
    {
        IExtenderBuilder<TBaseValue, TState> Attach(Func<IExtender<TBaseValue, TState>, IExtension<TBaseValue>> configuration);

        IExtender<TBaseValue, TState> Build();
    }
}

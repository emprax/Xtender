using System;

namespace Xtender.DependencyInjection
{
    public interface IExtenderFactoryBuilder<TKey, TState>
    {
        IExtenderFactoryBuilder<TKey, TState> WithExtender(TKey key, Action<IExtenderBuilder<TState>> configuration);
    }
}

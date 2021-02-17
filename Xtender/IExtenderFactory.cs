namespace Xtender
{
    public interface IExtenderFactory<TKey, TState>
    {
        IExtender<TState> Create(TKey key);
    }
}

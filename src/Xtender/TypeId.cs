namespace Xtender;

internal static class TypeId
{
    private static readonly object @lock = new();
    private static long Value = 0L;

    internal static long Increment()
    {
        lock (@lock)
        {
            var value = Value;
            Value++;

            return value;
        }
    }

    internal static long Get<T>() => TypeId<T>.Value;
}

internal static class TypeId<T>
{
    internal static readonly long Value = TypeId.Increment();
}

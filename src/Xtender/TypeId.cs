using System.Threading;

namespace Xtender;

internal static class TypeId
{
    private static readonly SemaphoreSlim semaphore = new(1);
    private static long Value = 0L;

    internal static long Increment()
    {
        semaphore.Wait();

        try
        {
            var value = Value;
            Value++;

            return value;
        }
        finally
        {
            semaphore.Release();
        }
    }

    internal static long Get<T>() => TypeId<T>.Value;
}

internal static class TypeId<T>
{
    internal static readonly long Value = TypeId.Increment();
}

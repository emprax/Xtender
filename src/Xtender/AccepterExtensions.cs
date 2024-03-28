using System.Threading.Tasks;
using Xtender.Async;
using Xtender.Sync;

namespace Xtender;

public static class AccepterExtensions
{
    public static void Accept<TValue>(this TValue value, IExtender extender) => extender.Extend(new Accepter<TValue>(value));

    public static Task Accept<TValue>(this TValue value, IAsyncExtender extender) => extender.Extend(new AsyncAccepter<TValue>(value));
}

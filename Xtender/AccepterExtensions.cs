using System.Threading.Tasks;

namespace Xtender
{
    public static class AccepterExtensions
    {
        public static Task Accept<TValue>(this TValue value, IExtender extender) => extender.Extend(new Accepter<TValue>(value));
    }
}

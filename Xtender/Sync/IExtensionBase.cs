namespace Xtender.Sync
{
    /// <summary>
    /// Ultimate Extension base for synchronous extensions.
    /// </summary>
    public interface IExtensionBase { }

    /// <summary>
    /// Extension base. Connects both other extension interfaces.
    /// </summary>
    /// <typeparam name="TContext">The concrete type that this extension is visiting to handle.</typeparam>
    public interface IExtensionBase<in TContext> : IExtensionBase { }
}
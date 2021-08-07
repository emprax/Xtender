namespace Xtender.Async
{
    /// <summary>
    /// Ultimate Extension base for asynchronous extensions.
    /// </summary>
    public interface IAsyncExtensionBase { }

    /// <summary>
    /// AsyncExtension base. Connects both other async extension interfaces.
    /// </summary>
    /// <typeparam name="TContext">The concrete type that this extension is visiting to handle.</typeparam>
    public interface IAsyncExtensionBase<in TContext> : IAsyncExtensionBase { }
}
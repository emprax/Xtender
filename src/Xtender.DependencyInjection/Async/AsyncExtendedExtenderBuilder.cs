using Xtender.Async;
using Xtender.Async.Builders;

namespace Xtender.DependencyInjection.Async;

internal class AsyncExtendedExtenderBuilder(IDependencyFactory factory, IAsyncExtenderBuilder builder) : IAsyncExtendedExtenderBuilder
{
    public IAsyncExtendedExtenderBuilder Attach<TContext, TExtension>() where TExtension : IAsyncExtension<TContext>
    {
        var constructor = typeof(TExtension).GetConstructors()[0];
        var parameters = constructor.GetParameters();

        var functor = new AsyncExtensionFunctor<TContext>(constructor, parameters);
        var handler = new AsyncFactoryHandler<TContext>(factory, functor);

        builder.Attach(handler);
        return this;
    }

    public IAsyncExtendedExtenderBuilder Default<TExtension>() where TExtension : IAsyncExtension<object>
    {
        var constructor = typeof(TExtension).GetConstructors()[0];
        var parameters = constructor.GetParameters();

        var functor = new AsyncExtensionFunctor<object>(constructor, parameters);
        var handler = new AsyncFactoryHandler<object>(factory, functor);

        builder.Default(handler);
        return this;
    }
}

internal class AsyncExtendedExtenderBuilder<TState>(IDependencyFactory factory, IAsyncExtenderBuilder<TState> builder) : IAsyncExtendedExtenderBuilder<TState>
{
    public IAsyncExtendedExtenderBuilder<TState> Attach<TContext, TExtension>() where TExtension : IAsyncExtension<TState, TContext>
    {
        var constructor = typeof(TExtension).GetConstructors()[0];
        var parameters = constructor.GetParameters();

        var functor = new AsyncExtensionFunctor<TState, TContext>(constructor, parameters);
        var handler = new AsyncFactoryHandler<TState, TContext>(factory, functor);

        builder.Attach(handler);
        return this;
    }

    public IAsyncExtendedExtenderBuilder<TState> AttachWithoutState<TContext, TExtension>() where TExtension : IAsyncExtension<TContext>
    {
        var constructor = typeof(TExtension).GetConstructors()[0];
        var parameters = constructor.GetParameters();

        var functor = new AsyncExtensionFunctorWrapper<TState, TContext>(new AsyncExtensionFunctor<TContext>(constructor, parameters));
        var handler = new AsyncFactoryHandler<TState, TContext>(factory, functor);

        builder.Attach(handler);
        return this;
    }

    public IAsyncExtendedExtenderBuilder<TState> Default<TExtension>() where TExtension : IAsyncExtension<TState, object>
    {
        var constructor = typeof(TExtension).GetConstructors()[0];
        var parameters = constructor.GetParameters();

        var functor = new AsyncExtensionFunctor<TState, object>(constructor, parameters);
        var handler = new AsyncFactoryHandler<TState, object>(factory, functor);

        builder.Default(handler);
        return this;
    }
}

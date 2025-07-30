using Xtender.Sync;
using Xtender.Sync.Builders;

namespace Xtender.DependencyInjection.Sync;

internal class ExtendedExtenderBuilder(IDependencyFactory factory, IExtenderBuilder builder) : IExtendedExtenderBuilder
{
    public IExtendedExtenderBuilder Attach<TContext, TExtension>() where TExtension : IExtension<TContext>
    {
        var constructor = typeof(TExtension).GetConstructors()[0];
        var parameters = constructor.GetParameters();

        var functor = new ExtensionFunctor<TContext>(constructor, parameters);
        var handler = new FactoryHandler<TContext>(factory, functor);

        builder.Attach(handler);
        return this;
    }

    public IExtendedExtenderBuilder Default<TExtension>() where TExtension : IExtension<object>
    {
        var constructor = typeof(TExtension).GetConstructors()[0];
        var parameters = constructor.GetParameters();

        var functor = new ExtensionFunctor<object>(constructor, parameters);
        var handler = new FactoryHandler<object>(factory, functor);

        builder.Default(handler);
        return this;
    }
}

internal class ExtendedExtenderBuilder<TState>(IDependencyFactory factory, IExtenderBuilder<TState> builder) : IExtendedExtenderBuilder<TState>
{
    public IExtendedExtenderBuilder<TState> Attach<TContext, TExtension>() where TExtension : IExtension<TState, TContext>
    {
        var constructor = typeof(TExtension).GetConstructors()[0];
        var parameters = constructor.GetParameters();

        var functor = new ExtensionFunctor<TState, TContext>(constructor, parameters);
        var handler = new FactoryHandler<TState, TContext>(factory, functor);

        builder.Attach(handler);
        return this;
    }

    public IExtendedExtenderBuilder<TState> AttachWithoutState<TContext, TExtension>() where TExtension : IExtension<TContext>
    {
        var constructor = typeof(TExtension).GetConstructors()[0];
        var parameters = constructor.GetParameters();

        var functor = new ExtensionFunctorWrapper<TState, TContext>(new ExtensionFunctor<TContext>(constructor, parameters));
        var handler = new FactoryHandler<TState, TContext>(factory, functor);

        builder.Attach(handler);
        return this;
    }

    public IExtendedExtenderBuilder<TState> Default<TExtension>() where TExtension : IExtension<TState, object>
    {
        var constructor = typeof(TExtension).GetConstructors()[0];
        var parameters = constructor.GetParameters();

        var functor = new ExtensionFunctor<TState, object>(constructor, parameters);
        var handler = new FactoryHandler<TState, object>(factory, functor);

        builder.Default(handler);
        return this;
    }
}

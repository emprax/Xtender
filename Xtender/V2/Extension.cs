using System.Threading.Tasks;

namespace Xtender.V2
{
    public abstract class Extension<TState, TContext> : IExtension<TContext>
    {
        protected IExtender<TState> extender;
        protected TState state;

        protected Extension(IExtender<TState> extender, TState state)
        {
            this.extender = extender;
            this.state = state;
        }

        public abstract Task Extent(TContext context);
    }
}
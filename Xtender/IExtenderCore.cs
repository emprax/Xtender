using System;
using System.Collections.Generic;

namespace Xtender
{
    public interface IExtenderCore<TState>
    {
        IDictionary<string, Func<object>> Provider { get; }
    }
}

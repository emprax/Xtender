using System.Collections.Generic;

namespace Xtender.Example.Console.Cases.ExtendWithState.Async;

public class State
{
    public ICollection<string> Messages { get; } = new List<string>();
}
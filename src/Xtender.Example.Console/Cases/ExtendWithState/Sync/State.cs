using System.Collections.Generic;

namespace Xtender.Example.Console.Cases.ExtendWithState.Sync;

public class State
{
    public ICollection<string> Messages { get; } = [];
}
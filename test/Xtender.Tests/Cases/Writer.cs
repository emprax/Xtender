using System.Collections.Generic;

namespace Xtender.Tests.Cases;

public class Writer(List<string> messages)
{
    public void Write(string message) => messages.Add(message);
}
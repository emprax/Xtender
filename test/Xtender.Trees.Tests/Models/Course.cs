using Xtender.Trees.Tests.Models.ValueTypes;

namespace Xtender.Trees.Tests.Models;

public class Course
{
    private FilledString name;
    private FilledString direction;

    public string Name
    {
        get => this.name;
        set => this.name = value;
    }

    public string Direction
    {
        get => this.direction;
        set => this.direction = value;
    }
}
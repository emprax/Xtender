using Xtender.Trees.Tests.Models.ValueTypes;

namespace Xtender.Trees.Tests.Models;

public class SchoolYear
{
    private SchoolYearValue value;

    public string Value
    {
        get => this.value;
        set => this.value = value;
    }
}
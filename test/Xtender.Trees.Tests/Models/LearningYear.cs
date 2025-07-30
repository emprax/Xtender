using Xtender.Trees.Tests.Models.ValueTypes;

namespace Xtender.Trees.Tests.Models;

public class LearningYear
{
    private LearningYearValue value;

    public int Value
    {
        get => this.value;
        set => this.value = value;
    }
}
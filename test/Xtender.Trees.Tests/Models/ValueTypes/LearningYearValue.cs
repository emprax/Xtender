namespace Xtender.Trees.Tests.Models.ValueTypes;

public struct LearningYearValue
{
    public int Value { get; private set; }

    public LearningYearValue(int value) => this.Value = Parse(value).Value;

    public static LearningYearValue Parse(int value) => (!TryParse(value, out var learningYear) || !learningYear.HasValue)
        ? throw new InvalidCastException($"Value '{value}' cannot be parsed to LearningYearValue")
        : learningYear.Value;

    public static bool TryParse(int value, out LearningYearValue? learningYear)
    {
        if (value <= 0 || value > 20)
        {
            learningYear = null;
            return false;
        }

        learningYear = new() { Value = value };
        return true;
    }

    public static implicit operator int(LearningYearValue learningYear) => learningYear.Value;

    public static implicit operator LearningYearValue(int learningYear) => Parse(learningYear);
}
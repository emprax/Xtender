namespace Xtender.Trees.Tests.Models.ValueTypes;

public struct FilledString
{
    public string Value { get; private set; }

    public FilledString(string value) => this.Value = Parse(value).Value;

    public static FilledString Parse(string value) => (!TryParse(value, out var result) || !result.HasValue)
        ? throw new InvalidCastException($"Value '{value}' is not a filled string")
        : result.Value;

    public static bool TryParse(string? value, out FilledString? result)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = null;
            return false;
        }

        result = new FilledString { Value = value };
        return true;
    }

    public static implicit operator string(FilledString value) => value.Value;

    public static implicit operator FilledString(string value) => Parse(value);
}
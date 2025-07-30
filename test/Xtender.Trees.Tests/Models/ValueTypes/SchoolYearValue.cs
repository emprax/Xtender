using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Xtender.Trees.Tests.Models.ValueTypes;

public struct SchoolYearValue
{
    public string Value { get; private set; }

    public uint StartYear { get; private set; }

    public uint EndYear { get; private set; }

    public SchoolYearValue(string value)
    {
        var result = Parse(value);
        this.StartYear = result.StartYear;
        this.EndYear = result.EndYear;
        this.Value = result.Value;
    }

    public static SchoolYearValue Parse(string value) => !TryParse(value, out var schoolYear) || !schoolYear.HasValue
        ? throw new InvalidCastException($"Value '{value}' cannot be parsed to a SchoolYearValue")
        : schoolYear.Value;

    public static bool TryParse(string value, [MaybeNullWhen(false)] out SchoolYearValue? schoolyear)
    {
        var parts = value.Split('/');
        var values = new List<int>();

        foreach (var part in parts)
        {
            if (int.TryParse(part, out var year))
            {
                values.Add(year);
            }
        }

        if (parts.Length != 2 || values.Any(x => x < 1000 && x > 3000) || value[1] != value[0] + 1)
        {
            schoolyear = null;
            return false;
        }

        schoolyear = new()
        {
            Value = value,
            StartYear = value[0],
            EndYear = value[1]
        };

        return true;
    }

    public static implicit operator string(SchoolYearValue schoolYear) => schoolYear.Value;

    public static implicit operator SchoolYearValue(string schoolYear) => Parse(schoolYear);
}
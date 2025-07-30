namespace Xtender.Trees.Tests.Models.ValueTypes;

public struct Sku
{
    public string Value { get; private set; }

    public Sku(string value) => this.Value = Parse(value).Value;

    public static Sku Parse(string value) => (!TryParse(value, out var sku) || sku is null)
        ? throw new InvalidCastException($"Value '{value}' is not a valid SKU")
        : sku.Value;

    public static bool TryParse(string value, out Sku? sku)
    {
        value = value.ToUpper();
        var lettersCounted = value.Count(char.IsLetter);
        var numbersCounted = value.Count(char.IsNumber);

        if (lettersCounted < 0 || numbersCounted < 0 || numbersCounted + lettersCounted != value.Length)
        {
            sku = null;
            return false;
        }

        sku = new() { Value = value };
        return true;
    }

    public static implicit operator string(Sku sku) => sku.Value;

    public static implicit operator Sku(string sku) => Parse(sku);
}
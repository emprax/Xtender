using Xtender.Trees.Tests.Models.ValueTypes;

namespace Xtender.Trees.Tests.Models;

public class Material
{
    private Sku sku;
    private FilledString title;
    private FilledString author;
    private FilledString publisher;

    public string Title
    {
        get => this.title;
        set => this.title = value;
    }

    public string Sku
    {
        get => this.sku;
        set => this.sku = value;
    }

    public decimal Price { get; set; }

    public string Author
    {
        get => this.author;
        set => this.author = value;
    }

    public string Publisher
    {
        get => this.publisher;
        set => this.publisher = value;
    }
}
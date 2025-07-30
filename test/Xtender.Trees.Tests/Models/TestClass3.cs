namespace Xtender.Trees.Tests.Models;

public class TestClass3
{
    public TestClass3() { }

    public TestClass3(string name, DateTime createdAt)
    {
        this.Name = name;
        this.CreatedAt = createdAt;
    }

    public DateTime CreatedAt { get; set; }

    public string Name { get; set; } = string.Empty;
}
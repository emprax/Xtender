using Xtender.Trees.Json;
using Xtender.Trees.Nodes;

namespace Xtender.Trees.Tests;

public class JsonParserTests
{
    [Fact]
    public void ShouldParseObjectToSimilarJsonAsInFile()
    {
        // Arrange
        var parser = new JsonParser<Guid>();
        var collection = new NodeCollection<Guid>(Guid.NewGuid(), "v1")
        {
            new NodeCollection<Guid>(Guid.NewGuid(), "v1.1")
            {
                new Node<Guid>(Guid.NewGuid(), "v1.1.1"),
                new IdCollection<Guid>(Guid.NewGuid(), "v1.1.2")
                {
                    Guid.NewGuid(),
                    Guid.NewGuid()
                },
                new IdCollection<Guid>(Guid.NewGuid(), "v1.1.3")
                {
                    Guid.NewGuid(),
                    Guid.NewGuid()
                }
            },
            new NodeCollection<Guid>(Guid.NewGuid(), "v1.2")
            {
                new Node<Guid>(Guid.NewGuid(), "v1.2.1"),
                new IdCollection<Guid>(Guid.NewGuid(), "v1.2.2")
                {
                    Guid.NewGuid(),
                    Guid.NewGuid()
                },
                new IdCollection<Guid>(Guid.NewGuid(), "v1.2.3")
                {
                    Guid.NewGuid(),
                    Guid.NewGuid()
                }
            }
        };

        // Act
        var result = parser.ToJson(collection);
        var content = result.ToString();

        // Assert

    }
}

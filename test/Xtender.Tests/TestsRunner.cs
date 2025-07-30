using System.Collections.Generic;
using System.Threading.Tasks;
using Xtender.Tests.Cases;
using Xtender.Tests.Cases.ExtendWithoutState.Async;
using Xtender.Tests.Cases.ExtendWithoutState.Sync;
using Xtender.Tests.Cases.ExtendWithState.Async;
using Xtender.Tests.Cases.ExtendWithState.Sync;
using Xunit;

namespace Xtender.Tests;

public class TestsRunner
{
    [Fact]
    public async Task TestCaseAsyncWithoutState()
    {
        // Arrange
        var messages = new List<string>();
        var writer = new Writer(messages);

        // Act
        await AsyncExtendWithoutState.Execute(writer);

        // Assert
        Assert.NotEmpty(messages);
        Assert.Equal("Level 1", messages[0]);
        Assert.Equal("Level 2.A", messages[1]);
        Assert.Equal("Level 3.A.1", messages[2]);
        Assert.Equal("Level 3.A.2", messages[3]);
        Assert.Equal("Level 3.A.3", messages[4]);
        Assert.Equal("Level 2.B", messages[5]);
        Assert.Equal("Level 3.B.1", messages[6]);
        Assert.Equal("Level 3.B.2", messages[7]);
        Assert.Equal("Level 3.B.3", messages[8]);
        Assert.Equal("Level 2.C", messages[9]);
        Assert.Equal("Level 3.C.1", messages[10]);
        Assert.Equal("Level 3.C.2", messages[11]);
        Assert.Equal("Level 3.C.3", messages[12]);
    }

    [Fact]
    public async Task TestCaseAsyncWithState()
    {
        // Arrange
        var messages = new List<string>();
        var writer = new Writer(messages);

        // Act
        await AsyncExtendWithState.Execute(writer);

        // Assert
        Assert.NotEmpty(messages);
        Assert.Equal("#123:Level 1", messages[0]);
        Assert.Equal("#123:Level 2.A", messages[1]);
        Assert.Equal("#123:Level 3.A.1", messages[2]);
        Assert.Equal("#123:Level 3.A.2", messages[3]);
        Assert.Equal("#123:Level 3.A.3", messages[4]);
        Assert.Equal("#123:Level 2.B", messages[5]);
        Assert.Equal("#123:Level 3.B.1", messages[6]);
        Assert.Equal("#123:Level 3.B.2", messages[7]);
        Assert.Equal("#123:Level 3.B.3", messages[8]);
        Assert.Equal("#123:Level 2.C", messages[9]);
        Assert.Equal("#123:Level 3.C.1", messages[10]);
        Assert.Equal("#123:Level 3.C.2", messages[11]);
        Assert.Equal("#123:Level 3.C.3", messages[12]);
    }

    [Fact]
    public void TestCaseSyncWithoutState()
    {
        // Arrange
        var messages = new List<string>();
        var writer = new Writer(messages);

        // Act
        SyncExtendWithoutState.Execute(writer);

        // Assert
        Assert.NotEmpty(messages);
        Assert.Equal("Level 1", messages[0]);
        Assert.Equal("Level 2.A", messages[1]);
        Assert.Equal("Level 3.A.1", messages[2]);
        Assert.Equal("Level 3.A.2", messages[3]);
        Assert.Equal("Level 3.A.3", messages[4]);
        Assert.Equal("Level 2.B", messages[5]);
        Assert.Equal("Level 3.B.1", messages[6]);
        Assert.Equal("Level 3.B.2", messages[7]);
        Assert.Equal("Level 3.B.3", messages[8]);
        Assert.Equal("Level 2.C", messages[9]);
        Assert.Equal("Level 3.C.1", messages[10]);
        Assert.Equal("Level 3.C.2", messages[11]);
        Assert.Equal("Level 3.C.3", messages[12]);
    }

    [Fact]
    public void TestCaseSyncWithState()
    {
        // Arrange
        var messages = new List<string>();
        var writer = new Writer(messages);

        // Act
        SyncExtendWithState.Execute(writer);

        // Assert
        Assert.NotEmpty(messages);
        Assert.Equal("#123:Level 1", messages[0]);
        Assert.Equal("#123:Level 2.A", messages[1]);
        Assert.Equal("#123:Level 3.A.1", messages[2]);
        Assert.Equal("#123:Level 3.A.2", messages[3]);
        Assert.Equal("#123:Level 3.A.3", messages[4]);
        Assert.Equal("#123:Level 2.B", messages[5]);
        Assert.Equal("#123:Level 3.B.1", messages[6]);
        Assert.Equal("#123:Level 3.B.2", messages[7]);
        Assert.Equal("#123:Level 3.B.3", messages[8]);
        Assert.Equal("#123:Level 2.C", messages[9]);
        Assert.Equal("#123:Level 3.C.1", messages[10]);
        Assert.Equal("#123:Level 3.C.2", messages[11]);
        Assert.Equal("#123:Level 3.C.3", messages[12]);
    }
}

using Xunit;

namespace SMath.Expansions;

public class PascalsTriangleTest
{
    [Fact]
    public void Row()
    {
        Assert.Equal(new[] { 1 }, PascalsTriangle.Row(0));
        Assert.Equal(new[] { 1, 1 }, PascalsTriangle.Row(1));
        Assert.Equal(new[] { 1, 2, 1 }, PascalsTriangle.Row(2));
        Assert.Equal(new[] { 1, 3, 3, 1 }, PascalsTriangle.Row(3));
        Assert.Equal(new[] { 1, 4, 6, 4, 1 }, PascalsTriangle.Row(4));
    }
}

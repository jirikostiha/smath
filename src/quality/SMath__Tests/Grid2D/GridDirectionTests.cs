using SMath.Grid2D;
using Xunit;

namespace SMath;

public class GridDirectionTests
{
    [Theory]
    [InlineData(3, 0, 0)]
    [InlineData(5, 5, 1)]
    [InlineData(0, 2, 2)]
    [InlineData(-1, 1, 3)]
    [InlineData(-4, 0, 4)]
    [InlineData(-2, -2, 5)]
    [InlineData(0, -9, 6)]
    [InlineData(6, -6, 7)]
    public void OfStep_AlongADirection_IsThatDirection(int dx, int dy, int expected)
        => Assert.Equal(expected, GridDirection.OfStep(dx, dy));

    [Theory]
    [InlineData(5, 2, 0)]   // 21.8 degrees
    [InlineData(7, 3, 1)]   // 23.2 degrees
    [InlineData(12, 5, 1)]  // 22.6 degrees
    [InlineData(2, 5, 2)]   // 68.2 degrees
    [InlineData(3, 7, 1)]   // 66.8 degrees
    [InlineData(-7, -3, 5)]
    [InlineData(-5, -2, 4)]
    public void OfStep_SnapsToTheClosestDirection(int dx, int dy, int expected)
        => Assert.Equal(expected, GridDirection.OfStep(dx, dy));

    [Fact]
    public void OfStep_OfZeroStep_IsNone()
        => Assert.Equal(GridDirection.None, GridDirection.OfStep(0, 0));

    [Fact]
    public void OfStep_OfTheLongestSteps_DoesNotOverflow()
    {
        Assert.Equal(1, GridDirection.OfStep(int.MaxValue, int.MaxValue));
        Assert.Equal(0, GridDirection.OfStep(int.MaxValue, int.MaxValue / 3));
        Assert.Equal(5, GridDirection.OfStep(int.MinValue, int.MinValue));
        Assert.Equal(6, GridDirection.OfStep(0, int.MinValue));
    }
}

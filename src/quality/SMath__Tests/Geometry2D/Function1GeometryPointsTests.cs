using System.Linq;
using Xunit;

namespace SMath.Geometry2D;

public class Function1GeometryPointsTests
{
    [Fact]
    public void FromCount_ProducesCorrectStepAndCount()
    {
        // identity function on [0, 10) split into 5 steps => step = (10-0)/5 = 2
        // expected x values: 0, 2, 4, 6, 8
        var points = Function1Geometry.Points
            .FromCount<double, int>(x => x, 0d, 10d, 5)
            .ToArray();

        Assert.Equal(5, points.Length);
        Assert.Equal(new[] { 0d, 2d, 4d, 6d, 8d }, points.Select(p => p.X));
        // y == x for identity function
        Assert.All(points, p => Assert.Equal(p.X, p.Y));
    }

    [Fact]
    public void FromCount_NonZeroFrom_UsesRangeNotFromOverCount()
    {
        // on [2, 6) with 2 steps => step = (6-2)/2 = 2 => x = 2, 4
        var points = Function1Geometry.Points
            .FromCount<double, int>(x => x, 2d, 6d, 2)
            .ToArray();

        Assert.Equal(2, points.Length);
        Assert.Equal(new[] { 2d, 4d }, points.Select(p => p.X));
    }
}

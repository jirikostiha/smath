using Xunit;
using static System.Math;

namespace SMath.Geometry2D;

public class CircleSegmentSagittaTests
{
    [Theory]
    // sagitta = r * (1 - cos(angle/2))
    [InlineData(1d, 1d)]
    [InlineData(2d, 1.5d)]
    [InlineData(5d, 2d)]
    public void Sagitta_FromAngle(double radius, double angle)
    {
        var expected = radius * (1d - Cos(angle / 2d));
        Assert.Equal(expected, Circle.Segment.Sagitta.FromAngle(radius, angle), 6);
    }

    [Fact]
    public void Sagitta_HalfCircle_EqualsRadius()
    {
        // angle = PI => sagitta = r * (1 - cos(PI/2)) = r * (1 - 0) = r
        Assert.Equal(3d, Circle.Segment.Sagitta.FromAngle(3d, PI), 6);
    }

    [Fact]
    public void Sagitta_ZeroAngle_IsZero()
    {
        Assert.Equal(0d, Circle.Segment.Sagitta.FromAngle(5d, 0d), 6);
    }
}

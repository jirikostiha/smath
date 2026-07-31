using Xunit;

namespace SMath.Geometry2D;

public class EllipseSemiLatusRectumTests
{
    [Theory]
    // semi-latus rectum l = b^2 / a
    [InlineData(2d, 1d, 0.5d)]
    [InlineData(4d, 2d, 1d)]
    [InlineData(5d, 3d, 1.8d)]
    public void SemiLatusRectum_FromRadius(double major, double minor, double expected)
    {
        Assert.Equal(expected, Ellipse.SemiLatusRectum.Length.FromRadius(major, minor), 6);
    }
}

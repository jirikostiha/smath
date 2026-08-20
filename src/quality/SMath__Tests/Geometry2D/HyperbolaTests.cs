using Xunit;

namespace SMath.Geometry2D;

public class HyperbolaTests
{
    [Fact]
    public void Eccentricity_FromRadius()
    {
        Assert.Equal(double.Sqrt(2), Hyperbola.Eccentricity.FromRadius(3d, 3d), 6);
        Assert.Equal(1.25, Hyperbola.Eccentricity.FromRadius(4d, 3d), 6);
    }

    [Theory]
    [InlineData(4d, 3d)]
    [InlineData(2d, 1d)]
    [InlineData(1d, 5d)]
    public void Eccentricity_IsGreaterThanOne(double major, double minor)
    {
        Assert.True(Hyperbola.Eccentricity.FromRadius(major, minor) > 1d);
    }

    [Fact]
    public void LinearEccentricity_FromRadius()
    {
        Assert.Equal(5d, Hyperbola.LinearEccentricity.FromRadius(4d, 3d), 6);
    }

    [Theory]
    [InlineData(4d, 3d)]
    [InlineData(2d, 1d)]
    public void LinearEccentricity_IsEccentricityTimesMajorRadius(double major, double minor)
    {
        var expected = major * Hyperbola.Eccentricity.FromRadius(major, minor);

        Assert.Equal(expected, Hyperbola.LinearEccentricity.FromRadius(major, minor), 6);
    }

    [Fact]
    public void SemiLatusRectum_FromRadius()
    {
        Assert.Equal(2.25, Hyperbola.SemiLatusRectum.Length.FromRadius(4d, 3d), 6);
        Assert.Equal(2.25, Hyperbola.Parameter.FromRadius(4d, 3d), 6);
    }

    [Fact]
    public void Vertex_FromRadius()
    {
        var (vertex1, vertex2) = Hyperbola.Vertex.FromRadius(4d);

        Assert.Equal((4d, 0d), vertex1);
        Assert.Equal((-4d, 0d), vertex2);
    }

    [Fact]
    public void Focus_FromRadius()
    {
        var (focus1, focus2) = Hyperbola.Focus.FromRadius(4d, 3d);

        Assert.Equal((5d, 0d), focus1);
        Assert.Equal((-5d, 0d), focus2);
    }

    [Fact]
    public void Directrix_FromRadius()
    {
        var (line1, line2) = Hyperbola.Directrix.FromRadius(4d, 3d);

        // x = +-a^2/c = +-16/5
        Assert.Equal((1d, 0d, -3.2d), line1);
        Assert.Equal((1d, 0d, 3.2d), line2);
    }

    [Fact]
    public void Asymptote_FromRadius()
    {
        var (line1, line2) = Hyperbola.Asymptote.FromRadius(4d, 3d);

        // b*x - a*y = 0 and b*x + a*y = 0, that is y = +-(3/4)*x
        Assert.Equal((3d, -4d, 0d), line1);
        Assert.Equal((3d, 4d, 0d), line2);
    }

    [Fact]
    public void Point_YFromX_IsZeroInVertex()
    {
        Assert.Equal(0d, Hyperbola.Point.YFromX(4d, 3d, 4d), 6);
    }

    [Fact]
    public void Point_FromX()
    {
        var point = Hyperbola.Point.FromX(4d, 3d, 5d);

        Assert.Equal(5d, point.X);
        Assert.Equal(2.25d, point.Y, 6);
    }

    [Theory]
    [InlineData(4d, 3d, 5d)]
    [InlineData(4d, 3d, 8d)]
    [InlineData(2d, 1d, 3d)]
    public void Point_DistancesToFociDifferByTransverseAxis(double major, double minor, double x)
    {
        // definition of a hyperbola: the difference of the distances to the foci is 2*a
        var point = Hyperbola.Point.FromX(major, minor, x);
        var (focus1, focus2) = Hyperbola.Focus.FromRadius(major, minor);

        var difference = double.Abs(Point2.Distance(point, focus2) - Point2.Distance(point, focus1));

        Assert.Equal(2d * major, difference, 6);
    }

    [Theory]
    [InlineData(4d, 3d, 100d)]
    [InlineData(2d, 1d, 500d)]
    public void Point_ApproachesAsymptoteInInfinity(double major, double minor, double x)
    {
        var point = Hyperbola.Point.FromX(major, minor, x);
        var asymptoteY = minor / major * x;

        Assert.True(point.Y < asymptoteY);
        Assert.True(asymptoteY - point.Y < 0.1d);
    }
}

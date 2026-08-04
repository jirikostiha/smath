using Xunit;

namespace SMath.Geometry2D;

public class LineTests
{
    [Theory]
    [InlineData(-1, 1, 0, 1)]
    [InlineData(1, 1, -1, -1)]
    [InlineData(0, 1, -1, 0)]
    public void Slope_FromGeneralForm(double a, double b, double c, double slope)
    {
        Assert.Equal(slope, Line.Slope.FromGeneralForm((a, b, c)));
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(double.Pi / 4d, 1)]
    [InlineData(double.Pi / 2d, double.PositiveInfinity)]
    public void Slope_FromAngle(double angle, double slope)
    {
        Assert.Equal(slope, Line.Slope.FromAngle(angle), 6);
    }

    [Theory]
    [InlineData(0, 0, 1, 0, 0, 1, 0)]
    [InlineData(0, 0, 0, 1, -1, 0, 0)]
    [InlineData(0, 0, -1, 0, 0, -1, 0)]
    [InlineData(0, 0, 0, -1, 1, 0, 0)]
    [InlineData(0, 0, 1, 1, -1, 1, 0)]
    public void FromTwoPoints(double p1x, double p1y, double p2x, double p2y, double a, double b, double c)
    {
        var line = Line.FromTwoPoints((p1x, p1y), (p2x, p2y));

        Assert.Equal(a, line.A);
        Assert.Equal(b, line.B);
        Assert.Equal(c, line.C);
    }

    [Theory]
    // Regression: the y-intercept accessor was named 'GeneralForm', while every
    // sibling member (XIntercept, Slope, NormalLine, ...) uses the 'FromGeneralForm'
    // convention. The consistent name must be available.
    [InlineData(0, 1, 0, 0)]      // x-axis          -> y = 0
    [InlineData(-1, 1, 0, 0)]     // identity y = x  -> y = 0
    [InlineData(-1, 1, -2, 2)]    // y = x + 2       -> y = 2
    [InlineData(0, 1, -3, 3)]     // y = 3           -> y = 3
    [InlineData(-2, 1, 5, -5)]    // y = 2x - 5      -> y = -5
    public void YIntercept_FromGeneralForm(double a, double b, double c, double expected)
    {
        Assert.Equal(expected, Line.YIntercept.FromGeneralForm((a, b, c)), 6);
    }

    [Theory]
    [InlineData(0, 0, 0, 1, 0)] // x-axis
    [InlineData(1, 0, -1, 1, 0)] // identity
    public void FromSlopeAndYIntercept(double slope, double yintercept, double a, double b, double c)
    {

        var line = Line.FromSlopeAndYIntercept(slope, yintercept);

        Assert.Equal(a, line.A);
        Assert.Equal(b, line.B);
        Assert.Equal(c, line.C);
    }
}

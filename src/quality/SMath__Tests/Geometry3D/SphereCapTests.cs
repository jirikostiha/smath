namespace SMath.Geometry3D;

using System;
using Xunit;

public class SphereCapTests
{
    [Fact]
    public void CapHeightOfZeroRadius() => Assert.Equal(0d, SphereCap.CapHeight(0d, 0d), 5);

    [Fact]
    public void CapHeightOfZeroCapRadius() => Assert.Equal(0d, SphereCap.CapHeight(1d, 0d), 5);

    [Fact]
    public void CapHeightOfHemisphereFromRadius() => Assert.Equal(1d, SphereCap.CapHeight(1d, 1d), 5);

    [Fact]
    public void CapHeightOfHemisphereFromCapRadius() => Assert.Equal(1d, SphereCap.CapHeight(1d, 1d), 5);

    [Fact]
    public void CapRadiusOfZeroCapHeight() => Assert.Equal(0d, SphereCap.CapRadiusFromCapHeight(1d, 0d), 5);

    [Fact]
    public void CapRadiusOfZeroRadius() => Assert.Equal(0d, SphereCap.CapRadiusFromCapHeight(0d, 0d), 5);

    [Fact]
    public void CapRadiusOfHemisphereFromCapHeight() => Assert.Equal(1d, SphereCap.CapRadiusFromCapHeight(1d, 1d), 5);

    [Fact]
    public void CapRadiusOfHemisphereFromRadius() => Assert.Equal(1d, SphereCap.CapRadius.FromCapHeight(1d, 1d), 5);

    [Fact]
    public void RadiusOfZeroCapHeight() => Assert.True(double.IsNaN(SphereCap.Radius.FromCapHeightAndCapRadius(0d, 0d)));

    [Fact]
    public void RadiusOfZeroCapRadius() => Assert.True(double.IsNaN(SphereCap.Radius.FromCapHeightAndCapRadius(0d, 0d)));

    [Fact]
    public void RadiusOfHemisphereFromCapHeight() => Assert.Equal(1d, SphereCap.Radius.FromCapHeightAndCapRadius(1d, 1d), 5);

    [Fact]
    public void RadiusOfHemisphereFromCapRadius() => Assert.Equal(1d, SphereCap.RadiusFromCapHeightAndCapRadius(1d, 1d), 5);

    [Fact]
    public void SurfaceAreaOfZeroCapHeight() => Assert.Equal(0d, SphereCap.Surface.Area.FromCapHeight(1d, 0d), 5);

    [Fact]
    public void SurfaceAreaOfZeroPolarAngle() => Assert.Equal(0d, SphereCap.Surface.Area.FromPolarAngle(1d, 0d), 5);

    [Fact]
    public void SurfaceAreaOfZeroCapRadius() => Assert.Equal(0d, SphereCap.Surface.Area.FromCapRadius(0d, 0d), 4);

    [Fact]
    public void SurfaceAreaOfHemisphere() => Assert.Equal(6.283185, SphereCap.Surface.Area.FromCapHeight(1d, 1d), 4);

    [Fact]
    public void SurfaceAreaOfHemisphereByCapRadius() => Assert.Equal(6.283185, SphereCap.Surface.Area.FromCapRadius(1d, 1d), 4);

    [Fact]
    public void SurfaceAreaOfHemisphereByPolarAngle() => Assert.Equal(6.283185, SphereCap.Surface.Area.FromPolarAngle(1d, Math.PI / 2d), 4);

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(25)]
    public void SpiralPointsLieOnTheSphereInsideTheCap(int count)
    {
        const double radius = 2d;
        const double capAngle = 1d;

        var points = SphereCap.Surface.Points.Spiral(radius, capAngle, count).ToList();

        Assert.Equal(count, points.Count);
        Assert.All(points, point =>
        {
            Assert.Equal(radius, Point3.Distance(point), 10);
            Assert.True(point.Z >= (radius * Math.Cos(capAngle)) - 1e-10, $"{point} lies outside the cap.");
        });
    }

    [Fact]
    public void SpiralStartsOnThePoleAndEndsOnTheRim()
    {
        const double capAngle = 0.7d;

        var points = SphereCap.Surface.Points.Spiral(1d, capAngle, 9).ToList();

        Assert.Equal(1d, points[0].Z, 12);
        Assert.Equal(Math.Cos(capAngle), points[^1].Z, 12);
    }

    [Fact]
    public void SpiralOfAWholeSphereReachesBothSidesOfEveryAxis()
    {
        var points = SphereCap.Surface.Points.Spiral(1d, Math.PI, 12).ToList();

        Assert.Contains(points, point => point.X > 0d);
        Assert.Contains(points, point => point.X < 0d);
        Assert.Contains(points, point => point.Y > 0d);
        Assert.Contains(points, point => point.Y < 0d);
        Assert.Equal(-1d, points[^1].Z, 12);
    }

    [Fact]
    public void SpiralSpreadsThePointsEvenly()
    {
        // Equal area per point keeps the closest pair at a comparable distance to the farthest neighbours.
        var points = SphereCap.Surface.Points.Spiral(1d, Math.PI, 50).ToList();

        double closest = double.PositiveInfinity;
        for (int i = 0; i < points.Count; i++)
        {
            for (int j = i + 1; j < points.Count; j++)
            {
                closest = Math.Min(closest, Point3.Distance(points[i], points[j]));
            }
        }

        // The mean spacing of 50 points on a unit sphere is about sqrt(4 pi / 50) = 0.5.
        Assert.True(closest > 0.2d, $"The closest pair is only {closest} apart.");
    }
}

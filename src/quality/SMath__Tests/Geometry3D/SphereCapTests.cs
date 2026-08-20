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
}

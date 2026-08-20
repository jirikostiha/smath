namespace SMath.Geometry3D;

using System;
using Xunit;

public class BallCapTests
{
    [Fact]
    public void SurfaceAreaOfZeroCapHeight() => Assert.Equal(0d, BallCap.Surface.Area.FromCapHeight(1d, 0d), 5);

    [Fact]
    public void SurfaceAreaOfZeroPolarAngle() => Assert.Equal(0d, BallCap.Surface.Area.FromPolarAngle(1d, 0d), 5);

    [Fact]
    public void SurfaceAreaOfZeroCapRadius() => Assert.Equal(0d, BallCap.Surface.Area.FromCapRadius(0d, 0d), 4);

    [Fact]
    public void SurfaceAreaOfHemisphere() => Assert.Equal(9.424778, BallCap.Surface.Area.FromCapHeight(1d, 1d), 4);

    [Fact]
    public void SurfaceAreaOfHemisphereByCapRadius() => Assert.Equal(9.424778, BallCap.Surface.Area.FromCapRadius(1d, 1d), 4);

    [Fact]
    public void SurfaceAreaOfHemisphereByPolarAngle() => Assert.Equal(9.424778, BallCap.Surface.Area.FromPolarAngle(1d, Math.PI / 2d), 4);

    [Fact]
    public void VolumeOfZeroCapHeight() => Assert.Equal(0d, BallCap.Region.Volume.FromCapHeight(1d, 0d), 5);

    [Fact]
    public void VolumeOfZeroPolarAngle() => Assert.Equal(0d, BallCap.Region.Volume.FromPolarAngle(1d, 0d), 5);

    [Fact]
    public void VolumeOfZeroCapRadius() => Assert.Equal(0d, BallCap.Region.Volume.FromCapRadius(0d, 0d), 4);

    [Fact]
    public void VolumeOfHemisphere() => Assert.Equal(2.094395, BallCap.Region.Volume.FromCapHeight(1d, 1d), 4);

    [Fact]
    public void VolumeOfHemisphereByCapRadius() => Assert.Equal(2.094395, BallCap.Region.Volume.FromCapRadius(1d, 1d), 4);

    [Fact]
    public void VolumeOfHemisphereByPolarAngle() => Assert.Equal(2.094395, BallCap.Region.Volume.FromPolarAngle(1d, Math.PI / 2d), 4);
}

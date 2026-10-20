using SMath.Geometry3D;
using Xunit;

namespace SMath.Geometry3D;

public class SphereTests
{
    [Fact]
    public void SurfaceArea()
    {
        Assert.Equal(4d * double.Pi, Sphere.Surface.Area.FromRadius(1d));
    }

    [Fact]
    public void Volume()
    {
        Assert.Equal(4d / 3d * double.Pi, Sphere.Region.Volume.FromRadius(1d));
    }

    [Fact]
    public void Radius()
    {
        Assert.Equal(1d, Sphere.Radius.FromSurfaceArea(4 * double.Pi));
        Assert.Equal(1d, Sphere.Radius.FromVolume(4d / 3d * double.Pi));
    }

    [Fact]
    public void SurfacePointFromAngles()
    {
        var (x, y, z) = Sphere.Surface.Point.FromAngles(2d, Math.PI / 2, Math.PI / 2);

        Assert.Equal(0d, x, 12);
        Assert.Equal(2d, y, 12);
        Assert.Equal(0d, z, 12);
        Assert.Equal(x, Sphere.Surface.Point.XFromAngles(2d, Math.PI / 2, Math.PI / 2), 12);
        Assert.Equal(y, Sphere.Surface.Point.YFromAngles(2d, Math.PI / 2, Math.PI / 2), 12);
        Assert.Equal(2d, Sphere.Surface.Point.ZFromAngles(2d, 0d), 12);
    }

    [Fact]
    public void SurfacePointsSpanBothPoles()
    {
        var points = Sphere.Surface.Points.FromRadius(3d, 50).ToList();

        Assert.Equal(50, points.Count);
        Assert.All(points, p => Assert.Equal(3d, Point3.Distance(p), 12));
        Assert.Equal(3d, points[0].Z, 12);
        Assert.Equal(-3d, points[^1].Z, 12);
    }

    [Fact]
    public void SurfaceAndPoint()
    {
        Assert.Equal(1d, Sphere.Surface.And.Point.Distance.FromRadius(3d, (0d, 0d, 4d)), 12);
        Assert.Equal(3d, Sphere.Surface.And.Point.Distance.FromRadius(3d, (0d, 0d, 0d)), 12);
        Assert.Equal(1d, Sphere.Surface.And.Point.Distance.FromRadius((1d, 1d, 1d), 3d, (1d, 1d, 5d)), 12);
        Assert.True(Sphere.Surface.And.Point.Intersection.FromRadius(3d, (1d, 2d, 2d)));
        Assert.True(Sphere.Surface.And.Point.Intersection.FromRadius((1d, 1d, 1d), 3d, (2d, 3d, 3d)));
        Assert.False(Sphere.Surface.And.Point.Intersection.FromRadius(3d, (1d, 2d, 1d)));
    }

    [Fact]
    public void RegionAndPoint()
    {
        Assert.Equal(0d, Sphere.Region.And.Point.Distance.FromRadius(3d, (0d, 1d, 0d)));
        Assert.Equal(1d, Sphere.Region.And.Point.Distance.FromRadius((1d, 1d, 1d), 3d, (1d, 1d, 5d)), 12);
        Assert.True(Sphere.Region.And.Point.Intersection.FromRadius(3d, (1d, 2d, 2d)));
        Assert.True(Sphere.Region.And.Point.Intersection.FromRadius((1d, 1d, 1d), 3d, (1d, 1d, 4d)));
        Assert.False(Sphere.Region.And.Point.Intersection.FromRadius(3d, (1d, 2d, 2.1d)));
    }

    [Fact]
    public void SurfaceAndPlane()
    {
        var plane = (0d, 0d, 1d, -2d); // z = 2

        Assert.Equal(0d, Sphere.Surface.And.Plane.Distance.FromRadius((0d, 0d, 0d), 3d, plane));
        Assert.Equal(1d, Sphere.Surface.And.Plane.Distance.FromRadius((0d, 0d, -2d), 3d, plane), 12);
        Assert.Null(Sphere.Surface.And.Plane.Intersection.FromRadius((0d, 0d, -2d), 3d, plane));
        Assert.Equal(((0d, 0d, 2d), Math.Sqrt(5d)), Sphere.Surface.And.Plane.Intersection.FromRadius((0d, 0d, 0d), 3d, plane));
        Assert.Equal(((0d, 0d, 2d), 0d), Sphere.Surface.And.Plane.Intersection.FromRadius((0d, 0d, -1d), 3d, plane));
    }

    [Fact]
    public void SurfaceAndSphere()
    {
        Assert.Equal(1d, Sphere.Surface.And.Sphere.Distance.FromRadius(1d, (0d, 0d, 4d), 2d), 12);
        Assert.Equal(1d, Sphere.Surface.And.Sphere.Distance.FromRadius(5d, (0d, 1d, 0d), 3d), 12); // nested
        Assert.Equal(0d, Sphere.Surface.And.Sphere.Distance.FromRadius(2d, (0d, 3d, 0d), 2d));

        Assert.Equal(((1d, 1d, 5d), 3d), Sphere.Surface.And.Sphere.Intersection.FromRadius((1d, 1d, 1d), 5d, (1d, 1d, 9d), 5d));
        Assert.Equal(((0d, 0d, 1d), 0d), Sphere.Surface.And.Sphere.Intersection.FromRadius(1d, (0d, 0d, 2d), 1d));
        Assert.Null(Sphere.Surface.And.Sphere.Intersection.FromRadius(1d, (0d, 0d, 3d), 1d));
        Assert.Null(Sphere.Surface.And.Sphere.Intersection.FromRadius(5d, (0d, 0d, 1d), 1d));
    }

    [Fact]
    public void RegionAndSphere()
    {
        Assert.Equal(1d, Sphere.Region.And.Sphere.Distance.FromRadius(1d, (0d, 0d, 4d), 2d), 12);
        Assert.Equal(0d, Sphere.Region.And.Sphere.Distance.FromRadius((1d, 1d, 1d), 5d, (1d, 2d, 1d), 1d));
    }

    [Fact]
    public void Overlap()
    {
        Assert.Equal(0d, Sphere.Region.And.Sphere.Overlap.FromRadius(1d, 2d, 3d));
        Assert.Equal(Sphere.Region.Volume.FromRadius(1d), Sphere.Region.And.Sphere.Overlap.FromRadius(3d, 1d, 1.5d), 12);
        // two unit balls one radius apart share two caps of height 1/2, each pi h^2 (3r - h) / 3
        Assert.Equal(2d * Math.PI * 0.25d * 2.5d / 3d, Sphere.Region.And.Sphere.Overlap.FromRadius(1d, 1d, 1d), 12);
    }

    [Fact]
    public void TangentPlaneTouchesTheSurfacePoint()
    {
        var plane = Sphere.TangentPlane.FromAngles(2d, 0.6d, 1.1d);
        var point = Sphere.Surface.Point.FromAngles(2d, 0.6d, 1.1d);

        Assert.Equal(0d, Plane.And.Point.SignedDistance.FromGeneralForm(plane, point), 12);
        Assert.Equal(2d, Plane.And.Point.Distance.FromGeneralForm(plane, (0d, 0d, 0d)), 12);
    }
}

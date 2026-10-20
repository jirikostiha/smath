using Xunit;

namespace SMath.Geometry3D;

public class PlaneTests
{
    // 2x + 3y + 6z - 14 = 0, the normal has a length of 7
    private static readonly (double A, double B, double C, double D) _plane = (2d, 3d, 6d, -14d);

    [Fact]
    public void FromPointsFollowsTheRightHandRule()
        => Assert.Equal((0d, 0d, 1d, -1d), Plane.GeneralForm.FromPoints((0d, 0d, 1d), (1d, 0d, 1d), (0d, 1d, 1d)));

    [Fact]
    public void FromCollinearPointsIsNull() => Assert.Null(Plane.GeneralForm.FromPoints((0d, 0d, 0d), (1d, 1d, 1d), (2d, 2d, 2d)));

    [Fact]
    public void FromPointAndNormalContainsThePoint()
    {
        var plane = Plane.GeneralForm.FromPointAndNormal((1d, 2d, 3d), (4d, -1d, 2d));

        Assert.True(Plane.And.Point.Intersection.FromGeneralForm(plane, (1d, 2d, 3d)));
        Assert.Equal((4d, -1d, 2d), Plane.Normal.FromGeneralForm(plane));
    }

    [Fact]
    public void NormalizedHasAUnitNormal() => Assert.Equal((2d / 7d, 3d / 7d, 6d / 7d, -2d), Plane.GeneralForm.Normalized(_plane));

    [Theory]
    [InlineData(0d, 0d, 0d, -2d)]
    [InlineData(1d, 2d, 1d, 0d)]
    [InlineData(3d, 5d, 7d, 7d)]
    public void SignedDistanceAndDistance(double x, double y, double z, double expected)
    {
        Assert.Equal(expected, Plane.And.Point.SignedDistance.FromGeneralForm(_plane, (x, y, z)), 12);
        Assert.Equal(Math.Abs(expected), Plane.And.Point.Distance.FromGeneralForm(_plane, (x, y, z)), 12);
    }

    [Fact]
    public void ProjectionLiesInThePlaneAndReflectionOnTheOtherSide()
    {
        var point = (3d, -1d, 4d);

        var projection = Plane.And.Point.Projection.FromGeneralForm(_plane, point);
        var reflection = Plane.And.Point.Reflection.FromGeneralForm(_plane, point);

        Assert.Equal(0d, Plane.And.Point.SignedDistance.FromGeneralForm(_plane, projection), 12);
        Assert.Equal(-Plane.And.Point.SignedDistance.FromGeneralForm(_plane, point), Plane.And.Point.SignedDistance.FromGeneralForm(_plane, reflection), 12);
    }

    [Fact]
    public void LineIntersection()
    {
        Assert.Equal((1d, 2d, 1d), Plane.And.Line.Intersection.FromPoints((0d, 0d, 1d, -1d), (1d, 2d, 0d), (1d, 2d, 5d)));
        Assert.Null(Plane.And.Line.Intersection.FromPoints(Plane.XY<double>(), (0d, 0d, 1d), (1d, 0d, 1d)));
    }

    [Fact]
    public void PlaneIntersectionIsTheSharedLine()
    {
        var line = Plane.And.Plane.Intersection.FromGeneralForm((0d, 0d, 1d, -2d), (1d, 0d, 0d, -3d));

        Assert.NotNull(line);
        Assert.Equal((3d, 0d, 2d), line.Value.Point);
        Assert.Equal((0d, 1d, 0d), line.Value.Direction);
        Assert.Null(Plane.And.Plane.Intersection.FromGeneralForm(Plane.XY<double>(), (0d, 0d, 2d, 5d)));
    }

    [Fact]
    public void CoordinatePlanes()
    {
        Assert.True(Plane.And.Point.Intersection.FromGeneralForm(Plane.XY<double>(), (5d, -3d, 0d)));
        Assert.True(Plane.And.Point.Intersection.FromGeneralForm(Plane.XZ<double>(), (5d, 0d, -3d)));
        Assert.True(Plane.And.Point.Intersection.FromGeneralForm(Plane.YZ<double>(), (0d, 5d, -3d)));
    }
}

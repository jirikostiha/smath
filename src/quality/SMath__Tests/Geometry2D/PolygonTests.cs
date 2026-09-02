using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SMath.Geometry2D;

public class PolygonTests
{
    private static readonly (double X, double Y)[] UnitSquare
        = [(0d, 0d), (1d, 0d), (1d, 1d), (0d, 1d)];

    private static readonly (double X, double Y)[] RightTriangle
        = [(0d, 0d), (4d, 0d), (0d, 3d)];

    /// <summary> An arrowhead pointing to +y, the notch in the middle of its bottom edge is outside. </summary>
    private static readonly (double X, double Y)[] Arrow
        = [(0d, 0d), (2d, 4d), (4d, 0d), (2d, 1d)];

    private static IEnumerable<(double X, double Y)> AsSequence((double X, double Y)[] vertices)
        => vertices.AsEnumerable();

    private static ReadOnlySpan<(double X, double Y)> AsSpan((double X, double Y)[] vertices)
        => new(vertices);

    [Fact]
    public void Counts()
    {
        Assert.Equal(5, Polygon.EdgeCount(5));
        Assert.Equal(5, Polygon.VertexCount(5));
        Assert.Equal(0, Polygon.DiagonalCount(0));
        Assert.Equal(0, Polygon.DiagonalCount(1));
        Assert.Equal(0, Polygon.DiagonalCount(2));
        Assert.Equal(0, Polygon.DiagonalCount(3));
        Assert.Equal(2, Polygon.DiagonalCount(4));
        Assert.Equal(9, Polygon.DiagonalCount(6));
    }

    [Fact]
    public void InternalAngleSum()
    {
        Assert.Equal(double.Pi, Polygon.InternalAngleSum(3d), 6);
        Assert.Equal(2d * double.Pi, Polygon.InternalAngleSum(4d), 6);
    }

    [Fact]
    public void ExternalAngleSum()
    {
        Assert.Equal(2d * double.Pi, Polygon.ExternalAngleSum<double>(), 6);
    }

    [Fact]
    public void Perimeter_Length_FromVertices()
    {
        Assert.Equal(4d, Polygon.Perimeter.Length.FromVertices(AsSequence(UnitSquare)), 6);
        Assert.Equal(4d, Polygon.Perimeter.Length.FromVertices(AsSpan(UnitSquare)), 6);

        // 3-4-5 right triangle
        Assert.Equal(12d, Polygon.Perimeter.Length.FromVertices(AsSequence(RightTriangle)), 6);
        Assert.Equal(12d, Polygon.Perimeter.Length.FromVertices(AsSpan(RightTriangle)), 6);
    }

    [Fact]
    public void Perimeter_Length_OfDegeneratePolygon_IsZero()
    {
        (double X, double Y)[] none = [];
        (double X, double Y)[] single = [(2d, 3d)];

        Assert.Equal(0d, Polygon.Perimeter.Length.FromVertices(AsSequence(none)));
        Assert.Equal(0d, Polygon.Perimeter.Length.FromVertices(AsSpan(none)));
        Assert.Equal(0d, Polygon.Perimeter.Length.FromVertices(AsSequence(single)));
        Assert.Equal(0d, Polygon.Perimeter.Length.FromVertices(AsSpan(single)));
    }

    [Fact]
    public void Region_Area_FromVertices()
    {
        Assert.Equal(1d, Polygon.Region.Area.FromVertices(AsSequence(UnitSquare)), 6);
        Assert.Equal(1d, Polygon.Region.Area.FromVertices(AsSpan(UnitSquare)), 6);

        Assert.Equal(6d, Polygon.Region.Area.FromVertices(AsSequence(RightTriangle)), 6);
        Assert.Equal(6d, Polygon.Region.Area.FromVertices(AsSpan(RightTriangle)), 6);
    }

    [Fact]
    public void Region_Area_IsIndependentOfOrientation()
    {
        var clockwise = AsSequence(UnitSquare).Reverse().ToArray();

        Assert.Equal(1d, Polygon.Region.Area.FromVertices(AsSequence(clockwise)), 6);
        Assert.Equal(1d, Polygon.Region.Area.FromVertices(AsSpan(clockwise)), 6);
    }

    [Fact]
    public void Region_Area_Signed_FollowsOrientation()
    {
        var clockwise = AsSequence(UnitSquare).Reverse().ToArray();

        Assert.Equal(1d, Polygon.Region.Area.SignedFromVertices(AsSequence(UnitSquare)), 6);
        Assert.Equal(1d, Polygon.Region.Area.SignedFromVertices(AsSpan(UnitSquare)), 6);
        Assert.Equal(-1d, Polygon.Region.Area.SignedFromVertices(AsSequence(clockwise)), 6);
        Assert.Equal(-1d, Polygon.Region.Area.SignedFromVertices(AsSpan(clockwise)), 6);
    }

    [Fact]
    public void Region_Area_OfDegeneratePolygon_IsZero()
    {
        (double X, double Y)[] none = [];
        (double X, double Y)[] segment = [(1d, 1d), (2d, 2d)];

        Assert.Equal(0d, Polygon.Region.Area.FromVertices(AsSequence(none)));
        Assert.Equal(0d, Polygon.Region.Area.FromVertices(AsSpan(none)));
        Assert.Equal(0d, Polygon.Region.Area.FromVertices(AsSequence(segment)));
        Assert.Equal(0d, Polygon.Region.Area.FromVertices(AsSpan(segment)));
    }

    [Fact]
    public void Region_Centroid_FromVertices()
    {
        Assert.Equal((0.5d, 0.5d), Polygon.Region.Centroid.FromVertices(AsSequence(UnitSquare)));
        Assert.Equal((0.5d, 0.5d), Polygon.Region.Centroid.FromVertices(AsSpan(UnitSquare)));

        var centroid = Polygon.Region.Centroid.FromVertices(AsSequence(RightTriangle));
        Assert.Equal(4d / 3d, centroid.X, 6);
        Assert.Equal(1d, centroid.Y, 6);
    }

    [Fact]
    public void Region_Centroid_OfZeroAreaPolygon_IsVertexMean()
    {
        (double X, double Y)[] single = [(2d, 3d)];
        (double X, double Y)[] segment = [(1d, 1d), (2d, 2d)];

        Assert.Equal((2d, 3d), Polygon.Region.Centroid.FromVertices(AsSequence(single)));
        Assert.Equal((2d, 3d), Polygon.Region.Centroid.FromVertices(AsSpan(single)));
        Assert.Equal((1.5d, 1.5d), Polygon.Region.Centroid.FromVertices(AsSequence(segment)));
        Assert.Equal((1.5d, 1.5d), Polygon.Region.Centroid.FromVertices(AsSpan(segment)));
    }

    [Fact]
    public void Region_Centroid_OfEmptyPolygon_Throws()
    {
        (double X, double Y)[] none = [];

        Assert.Throws<ArgumentException>(() => Polygon.Region.Centroid.FromVertices(AsSequence(none)));
    }

    [Theory]
    [InlineData(0.5d, 0.5d, true)]
    [InlineData(0.01d, 0.99d, true)]
    [InlineData(2d, 0.5d, false)]
    [InlineData(-1d, -1d, false)]
    public void Contains_Square(double x, double y, bool expected)
    {
        Assert.Equal(expected, Polygon.Contains(AsSequence(UnitSquare), (x, y)));
        Assert.Equal(expected, Polygon.Contains(AsSpan(UnitSquare), (x, y)));
    }

    [Theory]
    [InlineData(1d, 0.5d, true)]
    [InlineData(3.9d, 0.5d, false)]
    [InlineData(-0.5d, 1d, false)]
    public void Contains_Triangle(double x, double y, bool expected)
    {
        Assert.Equal(expected, Polygon.Contains(AsSequence(RightTriangle), (x, y)));
        Assert.Equal(expected, Polygon.Contains(AsSpan(RightTriangle), (x, y)));
    }

    [Theory]
    [InlineData(2d, 2d, true)]
    [InlineData(2d, 0.5d, false)]
    [InlineData(1d, 1.5d, true)]
    public void Contains_ConcavePolygon(double x, double y, bool expected)
    {
        Assert.Equal(expected, Polygon.Contains(AsSequence(Arrow), (x, y)));
        Assert.Equal(expected, Polygon.Contains(AsSpan(Arrow), (x, y)));
    }

    [Fact]
    public void Contains_EmptyPolygon_IsFalse()
    {
        (double X, double Y)[] none = [];

        Assert.False(Polygon.Contains(AsSequence(none), (0d, 0d)));
        Assert.False(Polygon.Contains(AsSpan(none), (0d, 0d)));
    }

    [Fact]
    public void Regular_InternalAngle()
    {
        Assert.Equal(double.Pi / 3d, Polygon.Regular.InternalAngle(3d), 6);
        Assert.Equal(double.Pi / 2d, Polygon.Regular.InternalAngle(4d), 6);
        Assert.Equal(2d * double.Pi / 3d, Polygon.Regular.InternalAngle(6d), 6);
    }

    [Fact]
    public void Regular_ExternalAngle()
    {
        Assert.Equal(2d * double.Pi / 3d, Polygon.Regular.ExternalAngle(3d), 6);
        Assert.Equal(double.Pi / 3d, Polygon.Regular.ExternalAngle(6d), 6);
    }

    [Fact]
    public void Regular_CentralAngle()
    {
        Assert.Equal(double.Pi / 2d, Polygon.Regular.CentralAngle(4d), 6);
    }

    [Fact]
    public void Regular_InternalAndExternalAngle_AreSupplementary()
    {
        for (var edges = 3d; edges <= 12d; edges++)
        {
            Assert.Equal(double.Pi,
                Polygon.Regular.InternalAngle(edges) + Polygon.Regular.ExternalAngle(edges), 6);
        }
    }

    [Fact]
    public void Regular_SchlafliSymbol()
    {
        Assert.Equal("{3}", Polygon.Regular.SchlafliSymbol(3));
        Assert.Equal("{6}", Polygon.Regular.SchlafliSymbol(6));
    }

    [Fact]
    public void Regular_Radiuses_OfSquare()
    {
        // a square with the edge of 2 has the circumradius of sqrt(2) and the inradius of 1
        Assert.Equal(double.Sqrt(2d), Polygon.Regular.Circumradius.FromEdge(4d, 2d), 6);
        Assert.Equal(1d, Polygon.Regular.Inradius.FromEdge(4d, 2d), 6);
        Assert.Equal(double.Sqrt(2d), Polygon.Regular.Circumradius.FromInradius(4d, 1d), 6);
        Assert.Equal(1d, Polygon.Regular.Inradius.FromCircumradius(4d, double.Sqrt(2d)), 6);
    }

    [Fact]
    public void Regular_Edge()
    {
        Assert.Equal(2d, Polygon.Regular.Edge.FromCircumradius(4d, double.Sqrt(2d)), 6);
        Assert.Equal(2d, Polygon.Regular.Edge.FromInradius(4d, 1d), 6);
    }

    [Fact]
    public void Regular_Perimeter_Length()
    {
        Assert.Equal(8d, Polygon.Regular.Perimeter.Length.FromEdge(4d, 2d), 6);
        Assert.Equal(8d, Polygon.Regular.Perimeter.Length.FromCircumradius(4d, double.Sqrt(2d)), 6);
        Assert.Equal(8d, Polygon.Regular.Perimeter.Length.FromInradius(4d, 1d), 6);
    }

    [Fact]
    public void Regular_Region_Area_OfSquare()
    {
        Assert.Equal(4d, Polygon.Regular.Region.Area.FromEdge(4d, 2d), 6);
        Assert.Equal(4d, Polygon.Regular.Region.Area.FromCircumradius(4d, double.Sqrt(2d)), 6);
        Assert.Equal(4d, Polygon.Regular.Region.Area.FromInradius(4d, 1d), 6);
        Assert.Equal(4d, Polygon.Regular.Region.Area.FromInradiusAndPerimeter(1d, 8d), 6);
    }

    [Fact]
    public void Regular_Region_Area_OfHexagon()
    {
        Assert.Equal(6d * double.Sqrt(3d), Polygon.Regular.Region.Area.FromEdge(6d, 2d), 6);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(12)]
    public void Regular_Vertices_MatchTheShoelaceArea(int edges)
    {
        var vertices = Polygon.Regular.Vertices.FromCircumradius(1d, edges).ToArray();

        Assert.Equal(edges, vertices.Length);
        Assert.Equal(Polygon.Regular.Region.Area.FromCircumradius((double)edges, 1d),
            Polygon.Region.Area.FromVertices(AsSpan(vertices)), 6);
    }

    [Fact]
    public void Regular_ApproachesCircleWithGrowingEdgeCount()
    {
        Assert.Equal(double.Pi, Polygon.Regular.Region.Area.FromCircumradius(100_000d, 1d), 6);
        Assert.Equal(Circle.Perimeter.Length.FromRadius(1d),
            Polygon.Regular.Perimeter.Length.FromCircumradius(100_000d, 1d), 6);
    }
}

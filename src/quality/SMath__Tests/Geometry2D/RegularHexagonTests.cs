using System.Linq;
using Xunit;

namespace SMath.Geometry2D;

public class RegularHexagonTests
{
    [Fact]
    public void Counts()
    {
        Assert.Equal(6, RegularHexagon.VertexCount<int>());
        Assert.Equal(6, RegularHexagon.EdgeCount<int>());
        Assert.Equal(9, RegularHexagon.DiagonalCount<int>());
    }

    [Fact]
    public void Angles()
    {
        Assert.Equal(2d * double.Pi / 3d, RegularHexagon.InternalAngle<double>(), 6);
        Assert.Equal(double.Pi / 3d, RegularHexagon.ExternalAngle<double>(), 6);
        Assert.Equal(double.Pi / 3d, RegularHexagon.CentralAngle<double>(), 6);
        Assert.Equal(4d * double.Pi, RegularHexagon.InternalAngleSum<double>(), 6);
    }

    [Fact]
    public void Angles_MatchRegularPolygon()
    {
        Assert.Equal(Polygon.Regular.InternalAngle(6d), RegularHexagon.InternalAngle<double>(), 6);
        Assert.Equal(Polygon.Regular.ExternalAngle(6d), RegularHexagon.ExternalAngle<double>(), 6);
        Assert.Equal(Polygon.InternalAngleSum(6d), RegularHexagon.InternalAngleSum<double>(), 6);
    }

    [Fact]
    public void SchlafliSymbol()
    {
        Assert.Equal("{6}", RegularHexagon.SchlafliSymbol);
        Assert.Equal(RegularHexagon.SchlafliSymbol, Polygon.Regular.SchlafliSymbol(6));
    }

    [Fact]
    public void Edge()
    {
        // the circumradius of a regular hexagon equals the length of its edge
        Assert.Equal(2d, RegularHexagon.Edge.FromCircumradius(2d), 6);
        Assert.Equal(2d, RegularHexagon.Edge.FromInradius(double.Sqrt(3d)), 6);
    }

    [Fact]
    public void Circumradius()
    {
        Assert.Equal(2d, RegularHexagon.Circumradius.FromEdge(2d), 6);
        Assert.Equal(2d, RegularHexagon.Circumradius.FromInradius(double.Sqrt(3d)), 6);
    }

    [Fact]
    public void Inradius()
    {
        Assert.Equal(double.Sqrt(3d), RegularHexagon.Inradius.FromEdge(2d), 6);
        Assert.Equal(double.Sqrt(3d), RegularHexagon.Inradius.FromCircumradius(2d), 6);
    }

    [Fact]
    public void Diagonal()
    {
        Assert.Equal(2d * double.Sqrt(3d), RegularHexagon.Diagonal.Minor.FromEdge(2d), 6);
        Assert.Equal(4d, RegularHexagon.Diagonal.Major.FromEdge(2d), 6);
    }

    [Fact]
    public void Perimeter_Length()
    {
        Assert.Equal(12d, RegularHexagon.Perimeter.Length.FromEdge(2d), 6);
        Assert.Equal(12d, RegularHexagon.Perimeter.Length.FromCircumradius(2d), 6);
        Assert.Equal(12d, RegularHexagon.Perimeter.Length.FromInradius(double.Sqrt(3d)), 6);
    }

    [Fact]
    public void Region_Area()
    {
        // 3*sqrt(3)/2 * a^2
        Assert.Equal(6d * double.Sqrt(3d), RegularHexagon.Region.Area.FromEdge(2d), 6);
        Assert.Equal(6d * double.Sqrt(3d), RegularHexagon.Region.Area.FromCircumradius(2d), 6);
        Assert.Equal(6d * double.Sqrt(3d), RegularHexagon.Region.Area.FromInradius(double.Sqrt(3d)), 6);
    }

    [Fact]
    public void Region_Area_MatchesRegularPolygon()
    {
        Assert.Equal(Polygon.Regular.Region.Area.FromEdge(6d, 2d), RegularHexagon.Region.Area.FromEdge(2d), 6);
    }

    [Fact]
    public void Region_Area_IsSixEquilateralTriangles()
    {
        Assert.Equal(6d * Triangle.Equilateral.Region.Area.FromEdge(2d),
            RegularHexagon.Region.Area.FromEdge(2d), 6);
    }

    [Fact]
    public void Vertices_FromCircumradius()
    {
        var vertices = RegularHexagon.Vertices.FromCircumradius(2d).ToArray();

        Assert.Equal(6, vertices.Length);
        Assert.Equal((2d, 0d), vertices[0]);
        Assert.Equal(RegularHexagon.Region.Area.FromCircumradius(2d),
            Polygon.Region.Area.FromVertices(new ReadOnlySpan<(double X, double Y)>(vertices)), 6);
        Assert.Equal(RegularHexagon.Perimeter.Length.FromCircumradius(2d),
            Polygon.Perimeter.Length.FromVertices(new ReadOnlySpan<(double X, double Y)>(vertices)), 6);
    }
}

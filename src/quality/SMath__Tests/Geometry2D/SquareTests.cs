using System.Linq;
using Xunit;

namespace SMath.Geometry2D;

public class SquareTests
{
    [Fact]
    public void Counts()
    {
        Assert.Equal(4, Square.VertexCount<int>());
        Assert.Equal(4, Square.EdgeCount<int>());
        Assert.Equal(2, Square.DiagonalCount<int>());
    }

    [Fact]
    public void Angles()
    {
        Assert.Equal(double.Pi / 2d, Square.InternalAngle<double>(), 6);
        Assert.Equal(double.Pi / 2d, Square.CentralAngle<double>(), 6);
        Assert.Equal(2d * double.Pi, Square.InternalAngleSum<double>(), 6);
        Assert.Equal(Polygon.Regular.InternalAngle(4d), Square.InternalAngle<double>(), 6);
    }

    [Fact]
    public void SchlafliSymbol()
    {
        Assert.Equal("{4}", Square.SchlafliSymbol);
        Assert.Equal(Square.SchlafliSymbol, Polygon.Regular.SchlafliSymbol(4));
    }

    [Fact]
    public void Vertices_OfUnitSquare()
    {
        Assert.Equal((0d, 0d), Square.Vertex1<double>());
        Assert.Equal((1d, 0d), Square.Vertex2<double>());
        Assert.Equal((1d, 1d), Square.Vertex3<double>());
        Assert.Equal((0d, 1d), Square.Vertex4<double>());
        Assert.Equal((0.5d, 0.5d), Square.Center<double>());
    }

    [Fact]
    public void Vertices_FromCircumradius()
    {
        var vertices = Square.Vertices.FromCircumradius(1d).ToArray();

        Assert.Equal(4, vertices.Length);
        Assert.Equal((1d, 0d), vertices[0]);
        Assert.Equal(Square.Region.Area.FromCircumradius(1d),
            Polygon.Region.Area.FromVertices(new ReadOnlySpan<(double X, double Y)>(vertices)), 6);
    }

    [Fact]
    public void Edge()
    {
        Assert.Equal(3d, Square.Edge.FromDiagonal(3d * double.Sqrt(2d)), 6);
        Assert.Equal(3d, Square.Edge.FromPerimeter(12d), 6);
        Assert.Equal(3d, Square.Edge.FromArea(9d), 6);
        Assert.Equal(3d, Square.Edge.FromCircumradius(1.5d * double.Sqrt(2d)), 6);
        Assert.Equal(3d, Square.Edge.FromInradius(1.5d), 6);
    }

    [Fact]
    public void Diagonal()
    {
        Assert.Equal(3d * double.Sqrt(2d), Square.Diagonal.FromEdge(3d), 6);
        Assert.Equal(3d * double.Sqrt(2d), Square.Diagonal.FromCircumradius(1.5d * double.Sqrt(2d)), 6);
    }

    [Fact]
    public void Circumradius()
    {
        Assert.Equal(1.5d * double.Sqrt(2d), Square.Circumradius.FromEdge(3d), 6);
        Assert.Equal(1.5d * double.Sqrt(2d), Square.Circumradius.FromDiagonal(3d * double.Sqrt(2d)), 6);
        Assert.Equal(1.5d * double.Sqrt(2d), Square.Circumradius.FromInradius(1.5d), 6);
    }

    [Fact]
    public void Inradius()
    {
        Assert.Equal(1.5d, Square.Inradius.FromEdge(3d), 6);
        Assert.Equal(1.5d, Square.Inradius.FromCircumradius(1.5d * double.Sqrt(2d)), 6);
    }

    [Fact]
    public void Perimeter_Length()
    {
        Assert.Equal(12d, Square.Perimeter.Length.FromEdge(3d));
        Assert.Equal(12d, Square.Perimeter.Length.FromDiagonal(3d * double.Sqrt(2d)), 6);
        Assert.Equal(12d, Square.Perimeter.Length.FromCircumradius(1.5d * double.Sqrt(2d)), 6);
        Assert.Equal(12d, Square.Perimeter.Length.FromInradius(1.5d), 6);
    }

    [Fact]
    public void Region_Area()
    {
        Assert.Equal(9d, Square.Region.Area.FromEdge(3d));
        Assert.Equal(8d, Square.Region.Area.FromDiagonal(4d));
        Assert.Equal(18d, Square.Region.Area.FromCircumradius(3d));
        Assert.Equal(36d, Square.Region.Area.FromInradius(3d));
        Assert.Equal(9d, Square.Region.Area.FromPerimeter(12d), 6);
    }

    [Fact]
    public void Region_Area_MatchesRegularPolygonAndRectangle()
    {
        Assert.Equal(Polygon.Regular.Region.Area.FromEdge(4d, 3d), Square.Region.Area.FromEdge(3d), 6);
        Assert.Equal(Rectangle.Perimeter.FromEdges(3d, 3d), Square.Perimeter.Length.FromEdge(3d), 6);
    }
}

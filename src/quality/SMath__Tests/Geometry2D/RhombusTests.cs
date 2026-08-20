using Xunit;

namespace SMath.Geometry2D;

public class RhombusTests
{
    /// <summary> Vertex angle of the rhombus with the edge of 5 and the diagonals of 6 and 8. </summary>
    private static readonly double VertexAngle = 2d * double.Asin(0.6d);

    [Fact]
    public void Counts()
    {
        Assert.Equal(4, Rhombus.VertexCount<int>());
        Assert.Equal(4, Rhombus.EdgeCount<int>());
        Assert.Equal(2, Rhombus.DiagonalCount<int>());
    }

    [Fact]
    public void Angles()
    {
        Assert.Equal(2d * double.Pi, Rhombus.InternalAngleSum<double>(), 6);
        Assert.Equal(double.Pi - VertexAngle, Rhombus.AdjacentAngle(VertexAngle), 6);
        Assert.Equal(double.Pi / 2d, Rhombus.AdjacentAngle(double.Pi / 2d), 6);
    }

    [Fact]
    public void SchlafliSymbol()
    {
        Assert.Equal("{} + {}", Rhombus.SchlafliSymbol);
    }

    [Fact]
    public void Edge()
    {
        Assert.Equal(5d, Rhombus.Edge.FromDiagonals(6d, 8d), 6);
        Assert.Equal(5d, Rhombus.Edge.FromHeightAndAngle(4.8d, VertexAngle), 6);
    }

    [Fact]
    public void Diagonal_FromEdgeAndAngle()
    {
        var (diagonal1, diagonal2) = Rhombus.Diagonal.FromEdgeAndAngle(5d, VertexAngle);

        Assert.Equal(6d, diagonal1, 6);
        Assert.Equal(8d, diagonal2, 6);
    }

    [Fact]
    public void Diagonal_OfSquare_AreEqual()
    {
        var (diagonal1, diagonal2) = Rhombus.Diagonal.FromEdgeAndAngle(1d, double.Pi / 2d);

        Assert.Equal(double.Sqrt(2d), diagonal1, 6);
        Assert.Equal(double.Sqrt(2d), diagonal2, 6);
    }

    [Fact]
    public void Diagonal_FromEdgeAndDiagonal()
    {
        Assert.Equal(8d, Rhombus.Diagonal.FromEdgeAndDiagonal(5d, 6d), 6);
        Assert.Equal(6d, Rhombus.Diagonal.FromEdgeAndDiagonal(5d, 8d), 6);
    }

    [Fact]
    public void Height()
    {
        Assert.Equal(4.8d, Rhombus.Height.FromDiagonals(6d, 8d), 6);
        Assert.Equal(4.8d, Rhombus.Height.FromEdgeAndAngle(5d, VertexAngle), 6);
    }

    [Fact]
    public void Inradius()
    {
        Assert.Equal(2.4d, Rhombus.Inradius.FromDiagonals(6d, 8d), 6);
        Assert.Equal(2.4d, Rhombus.Inradius.FromEdgeAndAngle(5d, VertexAngle), 6);
        Assert.Equal(2.4d, Rhombus.Inradius.FromHeight(4.8d), 6);
    }

    [Fact]
    public void Perimeter_Length()
    {
        Assert.Equal(12d, Rhombus.Perimeter.Length.FromEdge(3d));
        Assert.Equal(20d, Rhombus.Perimeter.Length.FromDiagonals(6d, 8d), 6);
    }

    [Fact]
    public void Region_Area()
    {
        Assert.Equal(24d, Rhombus.Region.Area.FromDiagonals(6d, 8d), 6);
        Assert.Equal(24d, Rhombus.Region.Area.FromHeight(5d, 4.8d), 6);
        Assert.Equal(24d, Rhombus.Region.Area.FromEdgeAndAngle(5d, VertexAngle), 6);
        Assert.Equal(24d, Rhombus.Region.Area.FromHeightAndAngle(4.8d, VertexAngle), 6);
        Assert.Equal(24d, Rhombus.Region.Area.FromInradius(5d, 2.4d), 6);
    }

    [Fact]
    public void Region_Area_OfSquare_MatchesSquare()
    {
        Assert.Equal(Square.Region.Area.FromEdge(3d),
            Rhombus.Region.Area.FromEdgeAndAngle(3d, double.Pi / 2d), 6);
    }
}

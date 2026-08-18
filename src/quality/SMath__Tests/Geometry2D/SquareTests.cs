using SMath.Geometry2D;
using Xunit;

namespace SMath.Tests.Geometry2D;

public class SquareTests
{
    [Fact]
    public void Properties()
    {
        Assert.Equal(4, Square.VertexCount<int>());
        Assert.Equal(4, Square.EdgeCount<int>());
        Assert.Equal(double.Pi / 2.0, Square.InternalAngle<double>(), 5);
        Assert.Equal("{4}", Square.SchlafliSymbol);
    }

    [Fact]
    public void Perimeter_Length()
    {
        Assert.Equal(12.0, Square.Perimeter.Length.FromEdge(3.0));
    }

    [Fact]
    public void Region_Area()
    {
        Assert.Equal(9.0, Square.Region.Area.FromEdge(3.0));
        Assert.Equal(8.0, Square.Region.Area.FromDiagonal(4.0));
        Assert.Equal(18.0, Square.Region.Area.FromCircumradius(3.0));
        Assert.Equal(36.0, Square.Region.Area.FromInradius(3.0));
    }
}

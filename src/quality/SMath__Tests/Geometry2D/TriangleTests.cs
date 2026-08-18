using SMath.Geometry2D;
using Xunit;

namespace SMath.Tests.Geometry2D;

public class TriangleTests
{
    [Fact]
    public void Properties()
    {
        Assert.Equal(3, Triangle.VertexCount<int>());
        Assert.Equal(3, Triangle.EdgeCount<int>());
        Assert.Equal("{3}", Triangle.SchlafliSymbol);
    }

    [Fact]
    public void Perimeter_Length()
    {
        Assert.Equal(12.0, Triangle.Perimeter.Length.FromEdges(3.0, 4.0, 5.0));
        Assert.Equal(9.0, Triangle.Equilateral.Perimeter.Length.FromEdge(3.0));
    }

    [Fact]
    public void Region_Area()
    {
        Assert.Equal(6.0, Triangle.Region.Area.FromEdges(3.0, 4.0, 5.0), 5);
        Assert.Equal(6.0, Triangle.Region.Area.FromHeight(4.0, 3.0));
        Assert.Equal(6.0, Triangle.Right.Region.Area.FromCathetuses(3.0, 4.0));
        Assert.Equal(6.0, Triangle.Right.Region.Area.FromHypotenuse(5.0, 2.4));
        Assert.Equal((double.Sqrt(3) / 4.0) * 4.0, Triangle.Equilateral.Region.Area.FromEdge(2.0), 5);
    }
}

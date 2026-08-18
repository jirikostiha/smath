using SMath.Geometry2D;
using Xunit;

namespace SMath.Tests.Geometry2D;

public class RegularHexagonTests
{
    [Fact]
    public void Properties()
    {
        Assert.Equal(6, RegularHexagon.VertexCount<int>());
        Assert.Equal(6, RegularHexagon.EdgeCount<int>());
        Assert.Equal("{6}", RegularHexagon.SchlafliSymbol);
        Assert.Equal(2.0 * double.Pi / 3.0, RegularHexagon.InternalAngle<double>(), 5);
    }

    [Fact]
    public void Perimeter_Length()
    {
        Assert.Equal(12.0, RegularHexagon.Perimeter.Length.FromEdge(2.0));
    }

    [Fact]
    public void Region_Area()
    {
        Assert.Equal(3.0 * double.Sqrt(3.0) / 2.0 * 4.0, RegularHexagon.Region.Area.FromEdge(2.0), 5);
    }
}

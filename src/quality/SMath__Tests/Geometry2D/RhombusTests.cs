using SMath.Geometry2D;
using Xunit;

namespace SMath.Tests.Geometry2D;

public class RhombusTests
{
    [Fact]
    public void Properties()
    {
        Assert.Equal(4, Rhombus.VertexCount<int>());
        Assert.Equal(4, Rhombus.EdgeCount<int>());
        Assert.Equal("{} + {}", Rhombus.SchlafliSymbol);
    }

    [Fact]
    public void Perimeter_Length()
    {
        Assert.Equal(12.0, Rhombus.Perimeter.Length.FromEdge(3.0));
    }

    [Fact]
    public void Region_Area()
    {
        Assert.Equal(12.0, Rhombus.Region.Area.FromHeight(3.0, 4.0));
        Assert.Equal(12.0, Rhombus.Region.Area.FromDiagonals(4.0, 6.0));
    }
}

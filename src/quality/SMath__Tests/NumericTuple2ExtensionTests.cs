using Xunit;

namespace SMath;

public class NumericTuple2ExtensionTests
{
    [Fact]
    public void Add()
    {
        var a = (1d, 2d);
        var b = (3d, 4d);
        Assert.Equal((4d, 6d), a.Add(b));
    }

    [Fact]
    public void Multiply()
    {
        var a = (2d, 3d);
        Assert.Equal((6d, 9d), a.Multiply(3d));
    }

    [Fact]
    public void Direction()
    {
        var a = (1d, 2d);
        var b = (4d, 6d);
        Assert.Equal((3d, 4d), a.Direction(b));
    }

    [Fact]
    public void Magnitude()
    {
        var a = (3d, 4d);
        Assert.Equal(5d, a.Magnitude());
    }

    [Fact]
    public void Dot()
    {
        var a = (1d, 2d);
        var b = (3d, 4d);
        Assert.Equal(11d, a.Dot(b));
    }

    [Fact]
    public void CrossProduct()
    {
        var a = (1d, 2d);
        var b = (3d, 4d);
        // 1*4 - 2*3 = 4 - 6 = -2
        Assert.Equal(-2d, a.CrossProduct(b));
    }
}

using Xunit;

namespace SMath.Geometry2D;

public class RectangleTests
{
    [Fact]
    public void Counts()
    {
        Assert.Equal(4, Rectangle.VertexCount<int>());
        Assert.Equal(4, Rectangle.EdgeCount<int>());
        Assert.Equal(2, Rectangle.DiagonalCount<int>());
    }

    [Theory]
    [InlineData(3d, 4d, 5d)]
    [InlineData(5d, 12d, 13d)]
    [InlineData(1d, 1d, 1.4142135623730951d)]
    public void Diagonal_FromEdges(double a, double b, double expected)
    {
        Assert.Equal(expected, Rectangle.Diagonal.FromEdges(a, b), 9);
    }

    [Theory]
    [InlineData(3d, 4d, 12d)]
    [InlineData(5d, 2d, 10d)]
    [InlineData(0d, 5d, 0d)]
    public void Area_FromEdges(double a, double b, double expected)
    {
        Assert.Equal(expected, Rectangle.Region.Area.FromEdges(a, b));
    }

    [Theory]
    [InlineData(3d, 4d, 14d)]
    [InlineData(5d, 2d, 14d)]
    public void Perimeter_Length_FromEdges(double a, double b, double expected)
    {
        Assert.Equal(expected, Rectangle.Perimeter.Length.FromEdges(a, b));
        Assert.Equal(expected, Rectangle.Perimeter.FromEdges(a, b));
    }

    [Fact]
    public void Perimeter_Points_Indexes_And_Indices()
    {
        var points = System.Linq.Enumerable.ToArray(Rectangle.Perimeter.Points.Indexes(4, 2d, 2d));
        Assert.Equal(4, points.Length);
        Assert.Equal((0d, 0d), points[0]);
        Assert.Equal((2d, 0d), points[1]);
        Assert.Equal((2d, 2d), points[2]);
        Assert.Equal((0d, 2d), points[3]);

        var indicesPoints = System.Linq.Enumerable.ToArray(Rectangle.Perimeter.Points.Indices(4, 2d, 2d));
        Assert.Equal(points, indicesPoints);
    }

    [Fact]
    public void Perimeter_Points_ZeroPerimeter()
    {
        var points = System.Linq.Enumerable.ToArray(Rectangle.Perimeter.Points.Indexes(3, 0d, 0d));
        Assert.Equal(3, points.Length);
        Assert.All(points, p => Assert.Equal((0d, 0d), p));
    }

    [Fact]
    public void Perimeter_Points_NonPositiveCount()
    {
        Assert.Empty(Rectangle.Perimeter.Points.Indexes(0, 2d, 2d));
        Assert.Empty(Rectangle.Perimeter.Points.Indexes(-1, 2d, 2d));
        Assert.Empty(Rectangle.Perimeter.Points.Indices(0, 2d, 2d));
    }
}

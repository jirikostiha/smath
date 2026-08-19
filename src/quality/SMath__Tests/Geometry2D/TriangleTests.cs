using Xunit;

namespace SMath.Geometry2D;

public class TriangleTests
{
    [Fact]
    public void Counts()
    {
        Assert.Equal(3, Triangle.VertexCount<int>());
        Assert.Equal(3, Triangle.EdgeCount<int>());
        Assert.Equal(0, Triangle.DiagonalCount<int>());
    }

    [Fact]
    public void InternalAngleSum()
    {
        Assert.Equal(double.Pi, Triangle.InternalAngleSum<double>(), 6);
        Assert.Equal(Polygon.InternalAngleSum(3d), Triangle.InternalAngleSum<double>(), 6);
    }

    [Fact]
    public void SchlafliSymbol()
    {
        Assert.Equal("{3}", Triangle.SchlafliSymbol);
    }

    [Fact]
    public void Perimeter_Length()
    {
        Assert.Equal(12d, Triangle.Perimeter.Length.FromEdges(3d, 4d, 5d));
        Assert.Equal(6d, Triangle.Semiperimeter.FromEdges(3d, 4d, 5d));
    }

    [Fact]
    public void Region_Area()
    {
        // 3-4-5 right triangle
        Assert.Equal(6d, Triangle.Region.Area.FromEdges(3d, 4d, 5d), 6);
        Assert.Equal(6d, Triangle.Region.Area.FromHeight(4d, 3d), 6);
        Assert.Equal(6d, Triangle.Region.Area.FromEdgesAndAngle(3d, 4d, double.Pi / 2d), 6);
        Assert.Equal(6d, Triangle.Region.Area.FromEdgesAndCircumradius(3d, 4d, 5d, 2.5d), 6);
        Assert.Equal(6d, Triangle.Region.Area.FromEdgesAndInradius(3d, 4d, 5d, 1d), 6);
        Assert.Equal(6d, Triangle.Region.Area.FromVertices((0d, 0d), (4d, 0d), (0d, 3d)), 6);
    }

    [Fact]
    public void Region_Area_FromVertices_IsIndependentOfOrientation()
    {
        Assert.Equal(6d, Triangle.Region.Area.FromVertices((0d, 0d), (0d, 3d), (4d, 0d)), 6);
    }

    [Fact]
    public void Region_Area_MatchesPolygon()
    {
        (double X, double Y)[] vertices = [(0d, 0d), (4d, 0d), (0d, 3d)];

        Assert.Equal(Polygon.Region.Area.FromVertices(new ReadOnlySpan<(double X, double Y)>(vertices)),
            Triangle.Region.Area.FromVertices(vertices[0], vertices[1], vertices[2]), 6);
    }

    [Fact]
    public void InternalAngle_FromEdges()
    {
        // the angle opposite to the hypotenuse of a right triangle is right
        Assert.Equal(double.Pi / 2d, Triangle.InternalAngle.FromEdges(5d, 3d, 4d), 6);
        Assert.Equal(double.Pi / 3d, Triangle.InternalAngle.FromEdges(2d, 2d, 2d), 6);
    }

    [Fact]
    public void InternalAngle_FromEdges_SumUpToStraightAngle()
    {
        var sum = Triangle.InternalAngle.FromEdges(3d, 4d, 5d)
            + Triangle.InternalAngle.FromEdges(4d, 5d, 3d)
            + Triangle.InternalAngle.FromEdges(5d, 3d, 4d);

        Assert.Equal(double.Pi, sum, 6);
    }

    [Fact]
    public void Height()
    {
        // the height perpendicular to the edge of 3 in a 3-4-5 triangle
        Assert.Equal(4d, Triangle.Height.FromEdges(3d, 4d, 5d), 6);
        Assert.Equal(3d, Triangle.Height.FromArea(4d, 6d), 6);
    }

    [Fact]
    public void Median()
    {
        Assert.Equal(double.Sqrt(73d) / 2d, Triangle.Median.FromEdges(3d, 4d, 5d), 6);

        // the median to the hypotenuse of a right triangle is half of it
        Assert.Equal(2.5d, Triangle.Median.FromEdges(5d, 3d, 4d), 6);
    }

    [Fact]
    public void Circumradius()
    {
        Assert.Equal(2.5d, Triangle.Circumradius.FromEdges(3d, 4d, 5d), 6);
        Assert.Equal(2.5d, Triangle.Circumradius.FromEdgeAndOppositeAngle(5d, double.Pi / 2d), 6);
    }

    [Fact]
    public void Inradius()
    {
        Assert.Equal(1d, Triangle.Inradius.FromEdges(3d, 4d, 5d), 6);
    }

    [Fact]
    public void Centroid()
    {
        Assert.Equal((1d, 1d), Triangle.Centroid.FromVertices((0d, 0d), (3d, 0d), (0d, 3d)));
    }

    [Fact]
    public void Equilateral_InternalAngle()
    {
        Assert.Equal(double.Pi / 3d, Triangle.Equilateral.InternalAngle<double>(), 6);
    }

    [Fact]
    public void Equilateral_Edge()
    {
        Assert.Equal(2d, Triangle.Equilateral.Edge.FromHeight(double.Sqrt(3d)), 6);
        Assert.Equal(3d, Triangle.Equilateral.Edge.FromCircumradius(double.Sqrt(3d)), 6);
        Assert.Equal(3d, Triangle.Equilateral.Edge.FromInradius(double.Sqrt(3d) / 2d), 6);
    }

    [Fact]
    public void Equilateral_Height()
    {
        Assert.Equal(double.Sqrt(3d), Triangle.Equilateral.Height.FromEdge(2d), 6);
    }

    [Fact]
    public void Equilateral_Radiuses()
    {
        Assert.Equal(double.Sqrt(3d), Triangle.Equilateral.Circumradius.FromEdge(3d), 6);
        Assert.Equal(double.Sqrt(3d) / 2d, Triangle.Equilateral.Inradius.FromEdge(3d), 6);

        // the circumradius of an equilateral triangle is twice its inradius
        Assert.Equal(2d * Triangle.Equilateral.Inradius.FromEdge(3d),
            Triangle.Equilateral.Circumradius.FromEdge(3d), 6);
    }

    [Fact]
    public void Equilateral_Perimeter_Length()
    {
        Assert.Equal(9d, Triangle.Equilateral.Perimeter.Length.FromEdge(3d));
        Assert.Equal(6d, Triangle.Equilateral.Perimeter.Length.FromHeight(double.Sqrt(3d)), 6);
    }

    [Fact]
    public void Equilateral_Region_Area()
    {
        Assert.Equal(double.Sqrt(3d), Triangle.Equilateral.Region.Area.FromEdge(2d), 6);
        Assert.Equal(double.Sqrt(3d), Triangle.Equilateral.Region.Area.FromHeight(double.Sqrt(3d)), 6);
        Assert.Equal(Triangle.Region.Area.FromEdges(2d, 2d, 2d),
            Triangle.Equilateral.Region.Area.FromEdge(2d), 6);
    }

    [Fact]
    public void Isosceles_Height()
    {
        Assert.Equal(4d, Triangle.Isosceles.Height.FromLegAndBase(5d, 6d), 6);
    }

    [Fact]
    public void Isosceles_InternalAngle()
    {
        var apex = Triangle.Isosceles.InternalAngle.Apex.FromLegAndBase(5d, 6d);
        var @base = Triangle.Isosceles.InternalAngle.Base.FromLegAndBase(5d, 6d);

        Assert.Equal(double.Pi, apex + (2d * @base), 6);
        Assert.Equal(double.Pi / 3d, Triangle.Isosceles.InternalAngle.Apex.FromLegAndBase(2d, 2d), 6);
    }

    [Fact]
    public void Isosceles_Perimeter_Length()
    {
        Assert.Equal(16d, Triangle.Isosceles.Perimeter.Length.FromLegAndBase(5d, 6d), 6);
    }

    [Fact]
    public void Isosceles_Region_Area()
    {
        Assert.Equal(12d, Triangle.Isosceles.Region.Area.FromLegAndBase(5d, 6d), 6);
        Assert.Equal(Triangle.Region.Area.FromEdges(5d, 5d, 6d),
            Triangle.Isosceles.Region.Area.FromLegAndBase(5d, 6d), 6);
    }

    [Fact]
    public void Right_Hypotenuse()
    {
        Assert.Equal(5d, Triangle.Right.Hypotenuse.FromCathetuses(3d, 4d), 6);
        Assert.Equal(5d, Triangle.Right.Hypotenuse.FromCircumradius(2.5d), 6);
        Assert.Equal(3d, Triangle.Right.Cathetus.FromHypotenuse(5d, 4d), 6);
    }

    [Fact]
    public void Right_Height()
    {
        Assert.Equal(2.4d, Triangle.Right.Height.FromCathetuses(3d, 4d), 6);
    }

    [Fact]
    public void Right_Radiuses()
    {
        Assert.Equal(2.5d, Triangle.Right.Circumradius.FromHypotenuse(5d), 6);
        Assert.Equal(1d, Triangle.Right.Inradius.FromCathetuses(3d, 4d), 6);
        Assert.Equal(Triangle.Inradius.FromEdges(3d, 4d, 5d),
            Triangle.Right.Inradius.FromCathetuses(3d, 4d), 6);
    }

    [Fact]
    public void Right_Perimeter_Length()
    {
        Assert.Equal(12d, Triangle.Right.Perimeter.Length.FromCathetuses(3d, 4d), 6);
    }

    [Fact]
    public void Right_Region_Area()
    {
        Assert.Equal(6d, Triangle.Right.Region.Area.FromCathetuses(3d, 4d), 6);
        Assert.Equal(6d, Triangle.Right.Region.Area.FromHypotenuse(5d, 2.4d), 6);
    }
}

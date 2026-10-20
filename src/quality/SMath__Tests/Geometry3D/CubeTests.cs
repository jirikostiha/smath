using Xunit;

namespace SMath.Geometry3D;

public class CubeTests
{
    [Fact]
    public void Counts()
    {
        Assert.Equal(8, Cube.VertexCount<int>());
        Assert.Equal(12, Cube.EdgeCount<int>());
        Assert.Equal(6, Cube.FaceCount<int>());
        Assert.Equal(double.Pi / 2, Cube.DihedralAngle<double>());
        Assert.Equal((0.5d, 0.5d, 0.5d), Cube.Center<double>());
    }

    [Fact]
    public void MeasuresFromEdge()
    {
        Assert.Equal(2d * Math.Sqrt(2d), Cube.FaceDiagonal.FromEdge(2d), 12);
        Assert.Equal(2d * Math.Sqrt(3d), Cube.SpaceDiagonal.FromEdge(2d), 12);
        Assert.Equal(Math.Sqrt(3d), Cube.Circumradius.FromEdge(2d), 12);
        Assert.Equal(Math.Sqrt(2d), Cube.Midradius.FromEdge(2d), 12);
        Assert.Equal(1d, Cube.Inradius.FromEdge(2d), 12);
        Assert.Equal(24d, Cube.Surface.Area.FromEdge(2d), 12);
        Assert.Equal(8d, Cube.Region.Volume.FromEdge(2d), 12);
    }

    [Fact]
    public void EdgeInvertsTheMeasures()
    {
        Assert.Equal(2d, Cube.Edge.FromFaceDiagonal(Cube.FaceDiagonal.FromEdge(2d)), 12);
        Assert.Equal(2d, Cube.Edge.FromSpaceDiagonal(Cube.SpaceDiagonal.FromEdge(2d)), 12);
        Assert.Equal(2d, Cube.Edge.FromCircumradius(Cube.Circumradius.FromEdge(2d)), 12);
        Assert.Equal(2d, Cube.Edge.FromMidradius(Cube.Midradius.FromEdge(2d)), 12);
        Assert.Equal(2d, Cube.Edge.FromInradius(Cube.Inradius.FromEdge(2d)), 12);
        Assert.Equal(2d, Cube.Edge.FromSurfaceArea(24d), 12);
        Assert.Equal(2d, Cube.Edge.FromVolume(8d), 12);
    }

    [Fact]
    public void OtherRelations()
    {
        Assert.Equal(2d * Math.Sqrt(3d), Cube.SpaceDiagonal.FromCircumradius(Math.Sqrt(3d)), 12);
        Assert.Equal(Math.Sqrt(3d), Cube.Circumradius.FromSpaceDiagonal(2d * Math.Sqrt(3d)), 12);
        Assert.Equal(Math.Sqrt(3d), Cube.Circumradius.FromInradius(1d), 12);
        Assert.Equal(1d, Cube.Inradius.FromCircumradius(Math.Sqrt(3d)), 12);
        Assert.Equal(24d, Cube.Surface.Area.FromSpaceDiagonal(2d * Math.Sqrt(3d)), 12);
        Assert.Equal(8d, Cube.Region.Volume.FromSurfaceArea(24d), 12);
    }
}

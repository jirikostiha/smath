using Xunit;

namespace SMath.Geometry3D;

public class TetrahedronTests
{
    // a regular tetrahedron with the edge 2 * sqrt(2) inscribed in the cube [-1, 1]^3
    private static readonly (double X, double Y, double Z)[] _regular = [(1d, 1d, 1d), (1d, -1d, -1d), (-1d, 1d, -1d), (-1d, -1d, 1d)];
    private static readonly double _edge = 2d * Math.Sqrt(2d);

    [Fact]
    public void Counts()
    {
        Assert.Equal(4, Tetrahedron.VertexCount<int>());
        Assert.Equal(6, Tetrahedron.EdgeCount<int>());
        Assert.Equal(4, Tetrahedron.FaceCount<int>());
    }

    [Fact]
    public void VolumeFromVertices()
    {
        Assert.Equal(1d / 6d, Tetrahedron.Region.Volume.FromVertices((0d, 0d, 0d), (1d, 0d, 0d), (0d, 1d, 0d), (0d, 0d, 1d)), 12);
        Assert.Equal(-1d / 6d, Tetrahedron.Region.Volume.SignedFromVertices((0d, 0d, 0d), (1d, 0d, 0d), (0d, 1d, 0d), (0d, 0d, -1d)), 12);
        Assert.Equal(1d / 6d, Tetrahedron.Region.Volume.FromVertices((0d, 0d, 0d), (1d, 0d, 0d), (0d, 1d, 0d), (0d, 0d, -1d)), 12);
    }

    [Fact]
    public void CentroidIsTheAverageOfTheVertices()
        => Assert.Equal((0d, 0d, 0d), Tetrahedron.Centroid.FromVertices(_regular[0], _regular[1], _regular[2], _regular[3]));

    [Fact]
    public void RegularMeasuresMatchTheInscribedTetrahedron()
    {
        Assert.Equal(_edge, Point3.Distance(_regular[0], _regular[1]), 12);
        Assert.Equal(Math.Sqrt(3d), Tetrahedron.Regular.Circumradius.FromEdge(_edge), 12);
        Assert.Equal(1d, Tetrahedron.Regular.Midradius.FromEdge(_edge), 12);
        Assert.Equal(1d / Math.Sqrt(3d), Tetrahedron.Regular.Inradius.FromEdge(_edge), 12);
        Assert.Equal(4d / Math.Sqrt(3d), Tetrahedron.Regular.Height.FromEdge(_edge), 12);
        Assert.Equal(Tetrahedron.Region.Volume.FromVertices(_regular[0], _regular[1], _regular[2], _regular[3]), Tetrahedron.Regular.Region.Volume.FromEdge(_edge), 12);
        Assert.Equal(Math.Sqrt(3d) * _edge * _edge, Tetrahedron.Regular.Surface.Area.FromEdge(_edge), 12);
        Assert.Equal(Math.Acos(1d / 3d), Tetrahedron.Regular.DihedralAngle<double>(), 12);
    }

    [Fact]
    public void RegularEdgeInvertsTheMeasures()
    {
        Assert.Equal(_edge, Tetrahedron.Regular.Edge.FromHeight(Tetrahedron.Regular.Height.FromEdge(_edge)), 12);
        Assert.Equal(_edge, Tetrahedron.Regular.Edge.FromCircumradius(Tetrahedron.Regular.Circumradius.FromEdge(_edge)), 12);
        Assert.Equal(_edge, Tetrahedron.Regular.Edge.FromInradius(Tetrahedron.Regular.Inradius.FromEdge(_edge)), 12);
        Assert.Equal(_edge, Tetrahedron.Regular.Edge.FromSurfaceArea(Tetrahedron.Regular.Surface.Area.FromEdge(_edge)), 12);
        Assert.Equal(_edge, Tetrahedron.Regular.Edge.FromVolume(Tetrahedron.Regular.Region.Volume.FromEdge(_edge)), 12);
    }
}

using Xunit;

namespace SMath.Geometry3D;

public class CuboidTests
{
    [Fact]
    public void Counts()
    {
        Assert.Equal(8, Cuboid.VertexCount<int>());
        Assert.Equal(12, Cuboid.EdgeCount<int>());
        Assert.Equal(6, Cuboid.FaceCount<int>());
        Assert.Equal(double.Pi / 2, Cuboid.DihedralAngle<double>());
    }

    [Theory]
    [InlineData(1d, 2d, 3d, true)]    // inside
    [InlineData(0d, 0d, 0d, true)]    // origin corner, faces are inclusive
    [InlineData(4d, 6d, 8d, true)]    // opposite corner
    [InlineData(4d, 3d, 0d, true)]    // on a face
    [InlineData(-1d, 3d, 4d, false)]  // below in x
    [InlineData(2d, 7d, 4d, false)]   // above in y
    [InlineData(2d, 3d, 9d, false)]   // above in z
    public void Contains_OriginAndSize(double x, double y, double z, bool expected)
        => Assert.Equal(expected, Cuboid.Contains((0d, 0d, 0d), (4d, 6d, 8d), (x, y, z)));

    [Theory]
    [InlineData(0d, 0d, 0d, true)]
    [InlineData(-2d, -2d, -2d, true)]
    [InlineData(3d, 3d, 3d, true)]
    [InlineData(3.5d, 0d, 0d, false)]
    public void ContainsByCorners_AcceptsCornersInAnyOrder(double x, double y, double z, bool expected)
    {
        Assert.Equal(expected, Cuboid.ContainsByCorners((-2d, -2d, -2d), (3d, 3d, 3d), (x, y, z)));
        Assert.Equal(expected, Cuboid.ContainsByCorners((3d, 3d, 3d), (-2d, -2d, -2d), (x, y, z)));
    }

    [Fact]
    public void UnitVerticesAndCenters()
    {
        Assert.Equal((0d, 0d, 0d), Cuboid.Vertex1<double>());
        Assert.Equal((1d, 1d, 1d), Cuboid.Vertex7<double>());
        Assert.Equal((0.5d, 0.5d, 0.5d), Cuboid.Center<double>());

        // octant centers of the unit cuboid, indexed per axis
        Assert.Equal((0.25d, 0.25d, 0.25d), Cuboid.Octant111Center<double>());
        Assert.Equal((0.75d, 0.25d, 0.25d), Cuboid.Octant211Center<double>());
        Assert.Equal((0.25d, 0.75d, 0.25d), Cuboid.Octant121Center<double>());
        Assert.Equal((0.25d, 0.25d, 0.75d), Cuboid.Octant112Center<double>());
        Assert.Equal((0.75d, 0.75d, 0.75d), Cuboid.Octant222Center<double>());
    }

    [Fact]
    public void SurfaceAreaVolumeAndDiagonal()
    {
        Assert.Equal(52d, Cuboid.Surface.Area.FromEdges(2d, 3d, 4d));
        Assert.Equal(24d, Cuboid.Region.Volume.FromEdges(2d, 3d, 4d));
        Assert.Equal(13d, Cuboid.Diagonal.FromEdges(3d, 4d, 12d), 12);
    }
}

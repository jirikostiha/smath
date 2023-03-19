using SMath.Geometry3D;
using Xunit;
using static System.Math;

namespace SMath.Geometry2D
{
    public class SphereTests
    {
        [Fact]
        public void SurfaceArea()
        {
            Assert.Equal(4d * PI, Sphere.Surface.Area.FromRadius(1d));
        }

        [Fact]
        public void Volume()
        {
            Assert.Equal(4d / 3d * PI, Sphere.Region.Volume.FromRadius(1d));
        }

        [Fact]
        public void Radius()
        {
            Assert.Equal(1d, Sphere.Radius.FromSurfaceArea(4 * PI));
            Assert.Equal(1d, Sphere.Radius.FromVolume(4d / 3d * PI));
        }
    }
}

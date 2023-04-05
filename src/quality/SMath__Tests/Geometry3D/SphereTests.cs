using SMath.Geometry3D;
using Xunit;

namespace SMath.Geometry2D
{
    public class SphereTests
    {
        [Fact]
        public void SurfaceArea()
        {
            Assert.Equal(4d * double.Pi, Sphere.Surface.Area.FromRadius(1d));
        }

        [Fact]
        public void Volume()
        {
            Assert.Equal(4d / 3d * double.Pi, Sphere.Region.Volume.FromRadius(1d));
        }

        [Fact]
        public void Radius()
        {
            Assert.Equal(1d, Sphere.Radius.FromSurfaceArea(4 * double.Pi));
            Assert.Equal(1d, Sphere.Radius.FromVolume(4d / 3d * double.Pi));
        }
    }
}

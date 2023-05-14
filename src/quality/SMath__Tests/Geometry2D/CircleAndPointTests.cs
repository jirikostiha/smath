using System.Linq;
using Xunit;

namespace SMath.Geometry2D
{
    public class CircleAndPointTests
    {
        [Fact]
        public void Distance_OfOuterPoint()
        {
            Assert.Equal(1d, Circle.Perimeter.And.Point.Distance.FromRadius(1d, (2d, 0d)), 6);
        }

        [Fact]
        public void Distance_OfInnerPoint()
        {
            Assert.Equal(0.5d, Circle.Perimeter.And.Point.Distance.FromRadius(1d, (0.5d, 0d)), 6);
        }

        [Fact]
        public void Distance_OfIncludedPoint()
        {
            Assert.Equal(0d, Circle.Perimeter.And.Point.Distance.FromRadius(1d, (1d, 0d)), 6);
        }

        [Fact]
        public void Intersection_WithOuterPoint()
        {
            Assert.False(Circle.Perimeter.And.Point.Intersection.FromRadius(1d, (2d, 0d)));
        }

        [Fact]
        public void Intersection_WithInnerPoint()
        {
            Assert.False(Circle.Perimeter.And.Point.Intersection.FromRadius(1d, (0.5d, 0d)));
        }

        [Fact]
        public void Intersection_WithIncludedPoint()
        {
            Assert.False(Circle.Perimeter.And.Point.Intersection.FromRadius(1d, (1d, 0d)));
        }
    }
}

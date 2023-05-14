using Xunit;

namespace SMath.Geometry2D
{
    public class CircleAndCircleTests
    {
        [Fact]
        public void Distance_OfOuterCircle()
        {
            Assert.Equal(1d, Circle.Perimeter.And.Circle.Distance.FromRadius(1d, (3d, 0d), 1d));
        }

        [Fact]
        public void Distance_OfOverlappingCircle()
        {
            Assert.Equal(0d, Circle.Perimeter.And.Circle.Distance.FromRadius(1d, (0.5d, 0d), 1d));
        }

        [Fact]
        public void Distance_OfInnerCircle()
        {
            Assert.Equal(0.5d, Circle.Perimeter.And.Circle.Distance.FromRadius(1d, (0, 0), 0.5d));
        }

        [Fact]
        public void Distance_OfTouchingCircle()
        {
            Assert.Equal(0d, Circle.Perimeter.And.Circle.Distance.FromRadius(1d, (2d, 0d), 1d));
        }

        [Fact]
        public void Distance_OfEqualCircle()
        {
            Assert.Equal(0d, Circle.Perimeter.And.Circle.Distance.FromRadius(1d, (0d, 0d), 1d));
        }

        [Fact]
        public void Intersection_WithOuterCircle_PointsDoNotExist()
        {
            var points = Circle.Perimeter.And.Circle.Intersection.FromRadius(1d, (1d, 1d), 1d); //todo

            Assert.Null(points);
        }

        [Fact]
        public void Intersection_WithOfOverlappingCircle_TwoPointsExists()
        {
            var points = Circle.Perimeter.And.Circle.Intersection.FromRadius(1d, (0.5d, 0d), 1d); //todo

            //Assert.Equal(0d, points);
        }

        [Fact]
        public void Intersection_WithInnerCircle_NoPointExists()
        {
            Assert.Equal(0.5d, Circle.Perimeter.And.Circle.Distance.FromRadius(1d, (0, 0), 0.5d));
        }

        [Fact]
        public void Intersection_WithTouchingCircle_TwoEqualPointExists()
        {
            var points = Circle.Perimeter.And.Circle.Intersection.FromRadius(1d, (2d, 0d), 1d);

            //Assert.Equal(0d, points);
        }

        [Fact]
        public void Intersection_WithEqualCircle_NoPointExists()
        {
            var points = Circle.Perimeter.And.Circle.Intersection.FromRadius(1d, (0d, 0d), 1d);

            //Assert.Equal(0d, point);
        }
    }
}
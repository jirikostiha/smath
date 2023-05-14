using Xunit;

namespace SMath.Geometry2D
{
    public class CircleAndLineTests
    {
        [Fact]
        public void Distance_OfOuterLine()
        {
            Assert.Equal(1d, Circle.Perimeter.And.Line.Distance.FromRadius(1d, (1, 1, 0)));
        }

        [Fact]
        public void Distance_OfOverlappingLine()
        {
            Assert.Equal(0d, Circle.Perimeter.And.Line.Distance.FromRadius(1d, (1, 1, 0)));
        }

        [Fact]
        public void Intersection_WithOuterLine_PointsDoNotExist()
        {
            var points = Circle.Perimeter.And.Line.Intersection.FromRadius(1d, (1,1,0));

            Assert.Null(points);
        }

        [Fact]
        public void Intersection_WithTouchingLine_TwoPointsExistsAndAreEqual()
        {
            var points = Circle.Perimeter.And.Line.Intersection.FromRadius(1d, (1, 1, 0));

            Assert.NotNull(points);
            Assert.Equal(points.Value.Point1, points.Value.Point2);
            //todo Assert.Equal(1d, points.Value.);
        }

        [Fact]
        public void Intersection_WithOverlappingLine_TwoDifferentPointsExists()
        {
            var points = Circle.Perimeter.And.Line.Intersection.FromRadius(1d, (1, 1, 0));

            Assert.NotNull(points);
            Assert.NotEqual(points.Value.Point1, points.Value.Point2);
            //todo Assert.Equal(1d, points.Value.);
        }
    }
}

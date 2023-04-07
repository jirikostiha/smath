using System.Linq;
using Xunit;

namespace SMath.Geometry2D
{
    public class CircleTests
    {
        [Fact]
        public void Circumference()
        {
            Assert.Equal(2 * double.Pi, Circle.Perimeter.Length.FromRadius(1d));
        }

        [Fact]
        public void Radius()
        {
            Assert.Equal(1, Circle.Radius.FromCircumference(2 * double.Pi));
            Assert.Equal(1, Circle.Radius.FromArea(double.Pi));
        }

        [Fact]
        public void PerimeterPoints_FromRadius()
        {
            Assert.Empty(Circle.Perimeter.Points.FromRadius(1d, 0));

            Assert.Single(Circle.Perimeter.Points.FromRadius(1d, 1));
            Assert.Equal((1, 0), Circle.Perimeter.Points.FromRadius(1d, 1).First());

            Assert.Equal((1, 0), Circle.Perimeter.Points.FromRadius(1d, 2).First());
            Assert.Equal(-1, Circle.Perimeter.Points.FromRadius(1d, 2).Last().X, 6);
            Assert.Equal(0, Circle.Perimeter.Points.FromRadius(1d, 2).Last().Y, 6);

            Assert.Equal(0, Circle.Perimeter.Points.FromRadius(1d, 4).Skip(1).First().X, 6);
            Assert.Equal(1, Circle.Perimeter.Points.FromRadius(1d, 4).Skip(1).First().Y, 6);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0)]
        [InlineData(1, 0, 1, 0, -1)]
        [InlineData(1, double.Pi / 2d, 0, 1, -1)]
        [InlineData(1, double.Pi / 4d, 0.707107, 0.707107, -1)]
        public void TangentLine_FromAngle(double radius, double angle, double a, double b, double c)
        {
            var line = Circle.TangentLine.FromAngle(radius, angle);
            Assert.Equal(a, line.A, 6);
            Assert.Equal(b, line.B, 6);
            Assert.Equal(c, line.C, 6);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(-1, 0, 0)]
        [InlineData(1, 0, 0)]
        [InlineData(1, 0.5, 0.5)]
        public void TangentPoints_FromInnerPoint(double radius, double pX, double pY)
        {
            Assert.Null(Circle.TangentPoint.FromPoint(radius, (pX, pY)));
        }

        [Theory]
        [InlineData(1, 1, 0)] // x-axis
        [InlineData(1, 0, 1)] // y-axis
        public void TangentPoints_FromCirclePoint(double radius, double pX, double pY)
        {
            var points = Circle.TangentPoint.FromPoint(radius, (pX, pY));

            Assert.NotNull(points);
            Assert.Equal(pX, points.Value.Point1.X, 6);
            Assert.Equal(pY, points.Value.Point1.Y, 6);
            Assert.Equal(pX, points.Value.Point2.X, 6);
            Assert.Equal(pY, points.Value.Point2.Y, 6);
        }

        [Theory]
        [InlineData(1, 1, 1, 0, 1, 1, 0)] // tangent is parallel to axis in (0,1), (1,0)
        [InlineData(2, 4, 0, 1, 1.732050807, 1, -1.732050807)] // outer point on x-axis
        public void TangentPoints_FromOuterPoint(double radius, double pX, double pY, double x1, double y1, double x2, double y2)
        {
            var points = Circle.TangentPoint.FromPoint(radius, (pX, pY));

            Assert.NotNull(points);
            Assert.Equal(x1, points.Value.Point1.X, 6);
            Assert.Equal(y1, points.Value.Point1.Y, 6);
            Assert.Equal(x2, points.Value.Point2.X, 6);
            Assert.Equal(y2, points.Value.Point2.Y, 6);
        }
    }
}

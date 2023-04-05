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
    }
}

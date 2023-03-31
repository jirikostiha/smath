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
        public void TangentLine_FromAngle(double radius, double angle, double a, double b, double c)
        {
            Assert.Equal(a, Circle.TangentLine.FromAngle(radius, angle).A, 6);
            Assert.Equal(b, Circle.TangentLine.FromAngle(radius, angle).B, 6);
            Assert.Equal(c, Circle.TangentLine.FromAngle(radius, angle).C, 6);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(double.Pi / 4d, 1)]
        [InlineData(double.Pi / 2d, double.PositiveInfinity)]
        public void TangentLineSlope_FromAngle(double angle, double slope)
        {
            Assert.Equal(slope, Circle.TangentLine.Slope.FromAngle(angle), 6);
        }
    }
}

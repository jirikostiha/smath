using System.Linq;
using Xunit;

namespace SMath.Geometry2D
{
    public class CirclePerimeterPointsTests
    {
        [Fact]
        public void FromRadius()
        {
            Assert.Empty(Circle.Perimeter.Points.FromRadius(1d, 0));

            Assert.Single(Circle.Perimeter.Points.FromRadius(1d, 1));
            Assert.Equal((1d, 0d), Circle.Perimeter.Points.FromRadius(1d, 1).First());

            Assert.Equal((1d, 0d), Circle.Perimeter.Points.FromRadius(1d, 2).First());
            Assert.Equal(-1d, Circle.Perimeter.Points.FromRadius(1d, 2).Last().X, 0.0000001d);
            Assert.Equal(0d, Circle.Perimeter.Points.FromRadius(1d, 2).Last().Y, 0.0000001d);

            Assert.Equal(0d, Circle.Perimeter.Points.FromRadius(1d, 4).Skip(1).First().X, 0.00000001d);
            Assert.Equal(1d, Circle.Perimeter.Points.FromRadius(1d, 4).Skip(1).First().Y, 0.00000001d);
        }
    }
}

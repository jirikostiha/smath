using Xunit;
using static System.Math;

namespace SMath.Geometry2D
{
    public class CircleSegmentTests
    {
        [Fact]
        public void Perimeter()
        {
            Assert.Equal(1d + 2d * Sin(0.5d), Circle.Segment.Perimeter.Length.FromAngle(1d, 1d));
        }

        [Fact]
        public void Area()
        {
            var a = (1d * 1d) / 2d * (1d - Sin(1d));
            //Assert.Equal(a, 0.5d - 0.5d * Sin(1d));
            Assert.Equal(0.5d - Sin(1d) / 2d, Circle.Segment.Region.Area.FromAngle(1d, 1d));
        }
    }
}

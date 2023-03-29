using Xunit;

namespace SMath.Geometry2D
{
    public class LineSegmentTests
    {
        [Fact]
        public void Slope()
        {
            Assert.Equal(0, Line.Segment.Slope.Get((0d, 0d), (1d, 0d)));
            Assert.Equal(1, Line.Segment.Slope.Get((0d, 0d), (1d, 1d)));
            //Assert.Equal(not-defined nebo infinity?, Line.Slope.FromTwoPoints((0d, 0d), (1d, 0d)));
        }

        [Fact]
        public void Points()
        {
            Assert.Equal(0, Line.Segment.Points.Get((0d, 0d), (1d, 1d), 4));
            Assert.Equal(1, Line.Segment.Points.Get((0d, 0d), (1d, 1d), 1));
            //Assert.Equal(not-defined nebo infinity?, Line.Slope.FromTwoPoints((0d, 0d), (1d, 0d)));
        }
    }
}

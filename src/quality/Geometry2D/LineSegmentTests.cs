using System.Collections.Generic;
using Xunit;

namespace SMath.Geometry2D
{
    public class LineSegmentTests
    {
        [Theory]
        [InlineData(0, 0, 1, 0, 0)]
        [InlineData(0, 0, 0, 1, double.PositiveInfinity)]
        [InlineData(0, 0, -1, 0, 0)]
        [InlineData(0, 0, 0, -1, double.NegativeInfinity)]
        [InlineData(0, 0, 1, 1, 1)]
        public void Slope(double p1x, double p1y, double p2x, double p2y, double slope)
        {
            Assert.Equal(slope, Line.Segment.Slope.Get((p1x, p1y), (p2x, p2y)));
        }

        [Fact]
        public void Points()
        {
            Assert.Empty(Line.Segment.Points.Get((0d, 0d), (1d, 1d), 0));
            
            Assert.Collection(Line.Segment.Points.Get((0d, 0d), (1d, 1d), 1),
                item => { 
                    Assert.Equal((0.5), item.X, 6);
                    Assert.Equal((0.5), item.Y, 6);
                });

            Assert.Collection(Line.Segment.Points.Get((0d, 0d), (1d, 1d), 2),
                item => {
                    Assert.Equal((1/3d), item.X, 6);
                    Assert.Equal((1/3d), item.Y, 6);
                },
                item => {
                    Assert.Equal((2/3d), item.X, 6);
                    Assert.Equal((2/3d), item.Y, 6);
                });
        }
    }
}

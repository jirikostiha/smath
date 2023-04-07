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
            Assert.Equal(slope, Line.Segment.Slope.FromTwoPoints((p1x, p1y), (p2x, p2y)));
        }

        [Fact]
        public void Parallel_ToXAxis_FromThreePoints()
        {
            var point = Line.Segment.Parallel.FromThreePoints((0d, 0), (1, 0), (0, 1));

            Assert.Equal(1, point.X);
            Assert.Equal(1, point.Y);
        }

        [Fact]
        public void Parallel_ToYAxis_FromThreePoints()
        {
            var point = Line.Segment.Parallel.FromThreePoints((0d, 0), (0, 1), (1, 0));

            Assert.Equal(1, point.X);
            Assert.Equal(1, point.Y); 
        }

        [Fact]
        public void Parallels_ToXAxis()
        {
            var parallels = Line.Segment.Parallels.Get((0d, 0), (1, 0), (0, 1), new[] { (1d, 1d), (2, 2) });

            Assert.Collection(parallels,
             item =>
             {
                 Assert.Equal(0, item.Point1.X, 6);
                 Assert.Equal(1, item.Point1.Y, 6);
                 Assert.Equal(1, item.Point2.X, 6);
                 Assert.Equal(1, item.Point2.Y, 6);
             },
             item =>
             {
                 Assert.Equal(0, item.Point1.X, 6);
                 Assert.Equal(2, item.Point1.Y, 6);
                 Assert.Equal(2, item.Point2.X, 6);
                 Assert.Equal(2, item.Point2.Y, 6);
             });
        }

        [Fact]
        public void Parallels_ToYAxis()
        {
            var parallels = Line.Segment.Parallels.Get((0d, 0), (0, 1), (1, 0), new[] { (1d, 1d), (2, 2) });

            Assert.Collection(parallels,
             item =>
             {
                 Assert.Equal(1, item.Point1.X, 6);
                 Assert.Equal(0, item.Point1.Y, 6);
                 Assert.Equal(1, item.Point2.X, 6);
                 Assert.Equal(1, item.Point2.Y, 6);
             },
             item =>
             {
                 Assert.Equal(2, item.Point1.X, 6);
                 Assert.Equal(0, item.Point1.Y, 6);
                 Assert.Equal(2, item.Point2.X, 6);
                 Assert.Equal(2, item.Point2.Y, 6);
             });
        }

        [Fact]
        public void Points()
        {
            Assert.Empty(Line.Segment.Points.Get((0d, 0d), (1d, 1d), 0));

            Assert.Collection(Line.Segment.Points.Get((0d, 0d), (1d, 1d), 1),
                item =>
                {
                    Assert.Equal((0.5), item.X, 6);
                    Assert.Equal((0.5), item.Y, 6);
                });

            Assert.Collection(Line.Segment.Points.Get((0d, 0d), (1d, 1d), 2),
                item =>
                {
                    Assert.Equal((1 / 3d), item.X, 6);
                    Assert.Equal((1 / 3d), item.Y, 6);
                },
                item =>
                {
                    Assert.Equal((2 / 3d), item.X, 6);
                    Assert.Equal((2 / 3d), item.Y, 6);
                });
        }
    }
}

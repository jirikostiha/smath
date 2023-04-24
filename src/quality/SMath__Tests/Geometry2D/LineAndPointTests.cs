using Xunit;

namespace SMath.Geometry2D
{
    public class LineAndPointTests
    {
        [Theory]
        [InlineData(0d, 1, 0, 0, 0, 0)] // x-axis, origin
        [InlineData(0d, 1, 0, 0, 1, 1)] // x-axis
        [InlineData(1d, 0, 0, 0, 0, 0)] // y-axis, origin
        [InlineData(1d, 0, 0, 1, 0, 1)] // y-axis
        [InlineData(1d, 1, 0, 1, 1, 1.4142135)]
        public void Distance_FromGeneralForm(double a, double b, double c, double x, double y, double distance)
        {
            Assert.Equal(distance, Line.And.Point.Distance.FromGeneralForm((a, b, c), (x, y)), 6);
        }


        [Theory]
        //[InlineData(0, 0, 0, 0, 0, 0, 0)] // zero to zero  ? is not line
        [InlineData(-1, 0, 1, 0, -1, 0, 0)] // identical to first seg point
        [InlineData(-1, 0, 1, 0, 1, 0, 0)]  // identical to second seg point
        [InlineData(-1, 0, 1, 0, 0, 0, 0)]  // on segment
        [InlineData(-1, 0, 1, 0, -1, 1, 1)] // above first point
        [InlineData(-1, 0, 1, 0, 0, 1, 1)]  // above the center point (origin)
        [InlineData(-1, 0, 1, 0, 1, 1, 1)]  // above second point
        [InlineData(-1, 0, 1, 0, -2, 0, 0)] // inline before first point
        [InlineData(-1, 0, 1, 0, 2, 0, 0)]  // inline after second point
        public void Distance_FromPoints(double ax, double ay, double bx, double by,
            double px, double py, double pointDistance)
        {
            Assert.Equal(pointDistance, Line.And.Point.Distance.FromPoints((ax, ay), (bx, by), (px, py)), 6);
        }

        [Theory]
        [InlineData(-1, 0, 1, 0, -1, 0, -1, 0)] // identical to first point on x-axis
        [InlineData(-1, 0, 1, 0, 1, 0, 1, 0)]   // identical to second point on x-axis
        [InlineData(-1, 0, 1, 0, 0, 0, 0, 0)]   // on line (x-axis)
        [InlineData(-1, 0, 1, 0, -1, 1, -1, 0)] // above first point on x-axis
        [InlineData(-1, 0, 1, 0, 0, 1, 0, 0)]   // above the center point (origin) on x-axis
        [InlineData(-1, 0, 1, 0, 1, 1, 1, 0)]   // above second point on x-axis
        [InlineData(-1, 0, 1, 0, -2, 0, -2, 0)] // inline before first point
        [InlineData(-1, 0, 1, 0, 2, 0, 2, 0)]   // inline after second point
        [InlineData(0, 0, 0, 2, 1, 1, 0, 1)]    // right to the first point on y-axis
        [InlineData(0, 2, 2, 0, 0, 0, 1, 1)]    
        public void Projection_FromPoints(double ax, double ay, double bx, double by,
          double px, double py, double pointX, double pointY)
        {
            var point = Line.And.Point.Projection.FromPoints((ax, ay), (bx, by), (px, py));
            
            Assert.Equal(pointX, point.X, 6);
            Assert.Equal(pointY, point.Y, 6);
        }
    }
}

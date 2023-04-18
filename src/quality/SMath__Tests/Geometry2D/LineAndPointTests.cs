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
        public void Distance_FromGeneralForm_DoesNotExist(double a, double b, double c, double x, double y, double distance)
        {
            Assert.Equal(distance, Line.And.Point.Distance.FromGeneralForm((a, b, c), (x, y)), 6);
        }
    }
}

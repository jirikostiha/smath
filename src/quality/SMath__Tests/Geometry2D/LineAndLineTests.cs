using Xunit;

namespace SMath.Geometry2D
{
    public class LineAndLineTests
    {
        [Theory]
        [InlineData(1, 0, 0, 0, 1, 0, 0, 0)] // x-axis cross y-axis in origin
        [InlineData(1, 1, 0, -1, 1, 0, 0, 0)] // the cross of 90 degrees in origin
        [InlineData(-1, 1, 0, 1, 1, -2, 1, 1)] // the cross of 90 degrees in (1,1)
        public void Intersection_FromGeneralForm_InPoint(double a1, double b1, double c1, double a2, double b2, double c2,
            double x, double y)
        {
            var point = Line.And.Line.Intersection.FromGeneralForm((a1, b1, c1), (a2, b2, c2));

            Assert.NotNull(point);
            Assert.Equal(x, point.Value.X);
            Assert.Equal(y, point.Value.Y);
        }

        [Theory]
        [InlineData(1, 0, 0, 1, 0, 0)] // identical to y-axis
        [InlineData(1, 1, 0, 1, 1, 0)] // identical to x-axis
        [InlineData(1, 1, -2, 1, 1, -2)] // identical
        [InlineData(1, 1, -2, 1, 1, -3)] // parallel
        public void Intersection_FromGeneralForm_DoesNotExist(double a1, double b1, double c1, double a2, double b2, double c2)
        {
            Assert.Null(Line.And.Line.Intersection.FromGeneralForm((a1, b1, c1), (a2, b2, c2)));
        }
    }
}

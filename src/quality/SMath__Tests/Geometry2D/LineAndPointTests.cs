using System;
using System.Collections.Generic;
using Xunit;

namespace SMath.Geometry2D;

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
    [MemberData(nameof(TestData.ProjectionData), MemberType = typeof(TestData))]
    public void Projection_FromPoints(((double X, double Y) P1, (double X, double Y) P2) segment,
        (double X, double Y) point, (double X, double Y) projectedPoint, string message)
    {
        var evaluatetPoint = Line.And.Point.Projection.FromPoints(segment.P1, segment.P2, point);

        Assert.Equal(projectedPoint.X, evaluatetPoint.X, 6);
        Assert.Equal(projectedPoint.Y, evaluatetPoint.Y, 6);
    }

    private static class TestData
    {
        public static IEnumerable<object[]> Projection()
        {
            yield return new object[] { (-1d, 0d), (1d, 0d), (-1d, 0d), (-1d, 0d), "Identical to first point on x-axis" };
            yield return new object[] { (-1d, 0d), (1d, 0d), (1d, 0d), (1d, 0d), "identical to second point on x-axis" };
            yield return new object[] { (-1d, 0d), (1d, 0d), (0d, 0d), (0d, 0d), "on line (x-axis)" };
            yield return new object[] { (-1d, 0d), (1d, 0d), (-1d, 1d), (-1d, 0d), "above first point on x-axis" };
            yield return new object[] { (-1d, 0d), (1d, 0d), (0d, 1d), (0d, 0d), "above the center point (origin) on x-axis" };
            yield return new object[] { (-1d, 0d), (1d, 0d), (1d, 1d), (1d, 0d), "above second point on x-axis" };
            yield return new object[] { (-1d, 0d), (1d, 0d), (-2d, 0d), (-2d, 0d), "inline before first point" };
            yield return new object[] { (-1d, 0d), (1d, 0d), (2d, 0d), (2d, 0d), "inline after second point" };
            yield return new object[] { (0d, 0d), (0d, 2d), (1d, 1d), (0d, 1d), "right to the first point on y-axis" };
            yield return new object[] { (0d, 2d), (2d, 0d), (0d, 0d), (1d, 1d), "" };
        }

        public static TheoryData<
            ((double X, double Y) P1, (double X, double Y) P2),
            (double X, double Y), (double X, double Y),
            string> ProjectionData => new()
        {
            { ((-1d, 0d), (1d, 0d)), (-1d, 0d), (-1d, 0d), "Identical to first point on x-axis" },
            //(-1d, 0d), (1d, 0d), (-1d, 0d), (-1d, 0d), "Identical to first point on x-axis" };
            //(-1d, 0d), (1d, 0d), (1d, 0d), (1d, 0d), "identical to second point on x-axis" };
        };
    }
}

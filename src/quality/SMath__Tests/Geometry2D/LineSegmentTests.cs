using System.Numerics;
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

        [Theory]
        [MemberData(nameof(LineSegmentData.DistanceToPoint), MemberType = typeof(LineSegmentData))]
        public void Distance_ToPoint((double X, double Y) sp1, (double X, double Y) sp2,
            (double X, double Y) point, double expDistance)
        {
            Assert.Equal(expDistance,
                Line.Segment.And.Point.Distance.FromPoints(sp1, sp2, point),
                6);
        }

        [Theory]
        [MemberData(nameof(LineSegmentData.IntersectingWithPoint), MemberType = typeof(LineSegmentData))]
        public void Intersection_WithPoint_Exists((double X, double Y) sp1, (double X, double Y) sp2, (double X, double Y) point)
        {
            Assert.True(Line.Segment.And.Point.Intersection.FromPoints(sp1, sp2, point));
        }

        [Theory]
        [MemberData(nameof(LineSegmentData.NotIntersectingWithPoint), MemberType = typeof(LineSegmentData))]
        public void Intersection_WithPoint_DoesNotExist((double X, double Y) sp1, (double X, double Y) sp2, (double X, double Y) point)
        {
            Assert.False(Line.Segment.And.Point.Intersection.FromPoints(sp1, sp2, point));
        }

        [Theory]
        [MemberData(nameof(LineSegmentData.IntersectingWithSegment), MemberType = typeof(LineSegmentData))]
        public void Intersection_WithSegment_Exists((double X, double Y) s1p1, (double X, double Y) s1p2,
            (double X, double Y) s2p1, (double X, double Y) s2p2,
            (double X, double Y) point)
        {
            var evalutedPoint = Line.Segment.And.Segment.Intersection.FromPoints(s1p1, s1p2, s2p1, s2p2);

            Assert.NotNull(evalutedPoint);
            Assert.Equal(point.X, evalutedPoint.Value.X, 6);
            Assert.Equal(point.Y, evalutedPoint.Value.Y, 6);
        }
    }

    public static class LineSegmentData
    {
        public static TheoryData<(double X, double Y), (double X, double Y), (double X, double Y), double>
            DistanceToPoint => new()
        {
          //{ (0, 0), (0, 0), (0, 0), 0 }, // zero to zero
            { (-1, 0), (1, 0), (-1, 0), 0 }, // identical to first seg point
            { (-1, 0), (1, 0), (1, 0), 0 },  // identical to second seg point
            { (-1, 0), (1, 0), (0, 0), 0 },  // on segment
            { (-1, 0), (1, 0), (-1, 1), 1 }, // above first point
            { (-1, 0), (1, 0), (0, 1), 1 },  // above the center point (origin)
            { (-1, 0), (1, 0), (1, 1), 1 },  // above second point
            { (-1, 0), (1, 0), (-2, 0), 1 }, // inline before first point
            { (-1, 0), (1, 0), (2, 0), 1 },  // inline after second point
        };

        public static TheoryData<(double X, double Y), (double X, double Y), (double X, double Y)>
            IntersectingWithPoint => new()
        {
            { (0,0), (1,0), (0,0) },
            { (0,0), (1,0), (1,0) },
            { (0, 0), (1, 0), (0.5, 0) },
            { (0, 0), (0, 1), (0, 1) },
        };

        public static TheoryData<(double X, double Y), (double X, double Y), (double X, double Y)>
            NotIntersectingWithPoint => new()
        {
            { (0,0), (1,0), (-1,0) },
            { (0,0), (1,0), (2,0) },
            { (0,0), (1,0), (0.5,1) },
            { (0,0), (1,0), (0.1,-1) },
        };

        public static TheoryData<(double X, double Y), (double X, double Y),
            (double X, double Y), (double X, double Y), (double X, double Y)>
            IntersectingWithSegment => new()
        {
            { (-1, 0), (1, 0), (0, -1), (0, 1), (0, 0) },   // x-axis intersect y-axis in origin
            { (1, 1), (-1, -1), (-1, 1), (1, -1), (0, 0) }, // 90 degrees cross intersect in origin
            { (0, 0), (5, 5), (0, 5), (5, 0), (2.5, 2.5) }, // X
            { (-1, 1), (1, 1), (0, 1), (0, 0), (0, 1) }     // T
        };
    }
}

using System.Numerics;

namespace SMath.Geometry1D;

/// <summary>
/// Line segment in 1D.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Line_segment">Wikipedia</a>
/// </remarks>
public static class LineSegment
{
    /// <summary>
    /// Divides a 1D unit line segment into equal subsegments and gets start distances.
    /// </summary>
    public static IEnumerable<N> Indexes<N>(int count)
        where N : INumberBase<N>
        => Indexes(count, N.One);

    /// <summary>
    /// Divides a 1D line segment of specified length into equal subsegments and gets start distances.
    /// </summary>
    public static IEnumerable<N> Indexes<N>(int count, N length)
        where N : INumberBase<N>
    {
        if (count <= 0)
            yield break;

        N step = length / N.CreateTruncating(count);
        for (int i = 0; i < count; i++)
            yield return N.CreateTruncating(i) * step;
    }

    /// <summary>
    /// Divides a 1D line segment of specified length into equal subsegments and gets start distances.
    /// </summary>
    public static IEnumerable<N> Indices<N>(int count, N length)
        where N : INumberBase<N>
        => Indexes(count, length);

    /// <summary>
    /// Length of a 1D line segment.
    /// </summary>
    public static class Length
    {
        /// <summary>
        /// Length of a 1D line segment determined by two points.
        /// </summary>
        public static N FromTwoPoints<N>(N point1, N point2)
            where N : INumberBase<N>, IComparisonOperators<N, N, bool>
            => point2 >= point1 ? point2 - point1 : point1 - point2;
    }

    /// <summary>
    /// Points on a 1D line segment.
    /// </summary>
    public static class Points
    {
        /// <summary>
        /// Get <paramref name="count"/> interior points on a 1D line segment determined by two points.
        /// </summary>
        public static IEnumerable<N> Get<N>(N point1, N point2, int count)
            where N : INumberBase<N>
        {
            var step = (point2 - point1) / N.CreateChecked(count + 1);
            for (int i = 1; i <= count; i++)
            {
                yield return point1 + N.CreateChecked(i) * step;
            }
        }
    }

    /// <summary>
    /// 1D line segment and point investigations.
    /// </summary>
    public static class And
    {
        /// <summary>
        /// 1D line segment and point investigation.
        /// </summary>
        public static class Point
        {
            /// <summary>
            /// Shortest distance from a 1D point to a 1D line segment. Zero when the point lies on the segment.
            /// </summary>
            public static class Distance
            {
                public static N FromPoints<N>(N segmentPoint1, N segmentPoint2, N point)
                    where N : INumberBase<N>, IComparisonOperators<N, N, bool>
                {
                    var min = segmentPoint1 < segmentPoint2 ? segmentPoint1 : segmentPoint2;
                    var max = segmentPoint1 > segmentPoint2 ? segmentPoint1 : segmentPoint2;

                    if (point < min)
                        return min - point;
                    if (point > max)
                        return point - max;
                    return N.Zero;
                }
            }

            /// <summary>
            /// Determines whether a point lies on a 1D line segment.
            /// </summary>
            public static class Intersection
            {
                public static bool FromPoints<N>(N segmentPoint1, N segmentPoint2, N point)
                    where N : INumberBase<N>, IComparisonOperators<N, N, bool>
                {
                    var min = segmentPoint1 < segmentPoint2 ? segmentPoint1 : segmentPoint2;
                    var max = segmentPoint1 > segmentPoint2 ? segmentPoint1 : segmentPoint2;

                    return point >= min && point <= max;
                }
            }
        }
    }
}

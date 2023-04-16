namespace Wayout.Mathematics.Geometry.D1
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Line segment in 1D.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Line_segment">wikipedia</a>
    /// </remarks>
    public static class LineSegment
    {
        /// <summary>
        /// Get indexed line segment to n subsegments and get start distances.
        /// </summary>
        public static IEnumerable<double> Indexes(int count, double length = 1)
        {
            for (int i = 0; i < count; i++)
                yield return i * length / count;
        }
    }
}

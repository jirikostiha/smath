namespace Wayout.Mathematics.Geometry.D2
{
    using System.Collections.Generic;

    /// <summary>
    /// Rectangle plane.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Rectangle">wikipedia</a>
    /// </remarks>
    public static class RectanglePlane
    {
         //?
        //public static IEnumerable<double> IndexX(double x, int xCount, int yCount)
        //{
        //    for (int i = 0; i < yCount; i++)
        //        foreach (var xIndex in D1.LineSegment.Indexes(xCount, x))
        //            yield return xIndex;
        //}

        //?
        //public static IEnumerable<double> IndexY(double y, int xCount, int yCount)
        //{
        //    for (int i = 0; i < xCount; i++)
        //        foreach (var yIndex in D1.LineSegment.Indexes(yCount, y))
        //            yield return yIndex;
        //}

        /// <summary>
        /// Je jen uvnitr.
        /// </summary>
        public static bool IncludesPoint(double topX1, double topX2, double px1, double px2) 
            => px1 > 0 && px1 < topX1 && px2 > 0 && px2 < topX2;
    }
}

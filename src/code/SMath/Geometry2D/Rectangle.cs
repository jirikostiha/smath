using System.Numerics;

namespace SMath.GeometryD2
{
    using System.Collections.Generic;

    /// <summary>
    /// Rectangle shape.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Rectangle">wikipedia</a>
    /// </remarks>
    public static class Rectangle
    {
        public static N VertexCount<N>()
            where N : INumberBase<N>
            => N.CreateChecked(4);

        public static N EdgeCount<N>()
            where N : INumberBase<N>
            => N.CreateChecked(4);

        // 90 degrees
        public static N InternalAngle<N>()
            where N : IFloatingPoint<N>
            => N.Pi / N.CreateChecked(2); 

        public const string SchlafliSymbol = "{} x {}";

        public static class Vertices
        {
            public static (N X, N Y) FromCenterPoint<N>(N xcenter, N ycenter, N halfWidth, N halfHeight, N rotationAngle)
            {


                return default;
            }

            /// <summary>
            /// Get rest of vertices from two initial vertices and height.
            /// </summary>
            public static (N X, N Y) FromTwoVertices<N>(N xa, N ya, N xb, N yb, N height)
            {


                return default;
            }
        }

        public static class Edge
        {
            public static N FromArea<N>(N area, N a)
                where N : IDivisionOperators<N, N, N>
                => area / a;
        }

        public static class Diagonal
        {
            public static N FromEdges<N>(N a, N b)
                where N : IRootFunctions<N>
                => PT.Hypotenuse(a, b);
        }

        public static class Perimeter
        {
            public static class Length
            {
                public static N FromEdges<N>(N a, N b)
                    where N : INumberBase<N>
                    => N.CreateChecked(2) * (a + b);
            }

            public static class Points
            {
                //public static IEnumerable<(N X1, N X2)> Indexes<N>(int count, N a, N b)
                //    where N : INumberBase<N>
                //{
                //    //todo urcite pujde pocitat lepe
                //    var perimeter = Perimeter.Length.FromEdges(a, b);
                //    foreach (var index in Geometry2D.Line.Segment.Points.Get(count, perimeter))
                //    {
                //        if (index <= a)
                //            yield return (index, N.Zero);
                //        else if (index <= a + b)
                //            yield return (a, index - a);
                //        else if (index <= 2 * a + b)
                //            yield return (a - (index - a - b), b);
                //        else
                //            yield return (0, b - perimetr - index);
                //    }
                //}
            }

            //public static class Intersection //?
            //{
            //}
        }

        //?
        //public static IEnumerable<N> IndexX(N x, int xCount, int yCount)
        //{
        //    for (int i = 0; i < yCount; i++)
        //        foreach (var xIndex in D1.LineSegment.Indexes(xCount, x))
        //            yield return xIndex;
        //}

        //?
        //public static IEnumerable<N> IndexY(N y, int xCount, int yCount)
        //{
        //    for (int i = 0; i < xCount; i++)
        //        foreach (var yIndex in D1.LineSegment.Indexes(yCount, y))
        //            yield return yIndex;
        //}

        /// <summary>
        /// Je jen uvnitr.
        /// </summary>
        public static bool IncludesPoint<N>(N topX1, N topX2, N px1, N px2)
            where N : INumberBase<N>, IComparisonOperators<N, N, bool>
            => px1 > N.Zero && px1 < topX1 && px2 > N.Zero && px2 < topX2;

        /// <summary>
        /// Je primo na obvodu.
        /// </summary>
        public static bool IncludesPoint2<N>(N topx1, N topx2, N px1, N px2)
            where N : INumberBase<N>, IComparisonOperators<N, N, bool>
            =>
            ((px1 == N.Zero || px1 == topx1) && px2 >= N.Zero && px2 <= topx2) 
            ||
            ((px2 == N.Zero || px2 == topx2) && px1 >= N.Zero && px1 <= topx1);


        /// <summary>
        /// Line intersection rectangle.
        /// </summary>
        /// <returns>
        ///     (0): no intersection
        ///     (1): single point - vertice
        ///     (2): two points
        ///     (int.max): identical with any edge
        /// </returns>
        //public static int IntersectsLine(N topx1, N topx2, N a1, N a2, N c,
        //   out (N X1, N X2) ip1, out (N X1, N X2) ip2)
        //{
        //    var ix1 = LineSegment.IntersectsLine(0, 0, topx1, 0, a1, a2, c, out var ipx1);
        //    if (ix1 == 1)
        //    {
        //    }
        //    else if (ix1 == int.MaxValue)
        //    {
        //        ip1 = (N.PositiveInfinity, N.PositiveInfinity);
        //    }

        //    //else if(Line.IntersectsLineSegment(a1, a2, c, 0, 0, 0, topx2, out var ipx2))
        //    //{

        //    //}
        //    //else if(Line.IntersectsLineSegment(a1, a2, c, 0, topx2, topx1, topx2, out var iptopx1))
        //    //{
        //    //}
        //    //else if (Line.IntersectsLineSegment(a1, a2, c, topx1, 0, topx1, topx2, out var iptopx2))
        //    //{ 
        //    //}

        //    ip1 = default;
        //    ip2 = default;
        //    return 0;
        //}

        //public static bool IntersectsRay(N topx1, N topx2, N a1, N a2, N c,
        //    out (N X1, N X2) ip1, out (N X1, N X2) ip2)
        //{

        //    ip1 = default;
        //    ip2 = default;
        //    return false;
        //}
    }
}

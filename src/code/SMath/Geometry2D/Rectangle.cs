namespace Wayout.Mathematics.Geometry.D2 //-> SMath.Geometry2D
{
    using System.Collections.Generic;
    using System.Numerics;

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

        public static N InternalAngle<N>()
            where N : IFloatingPoint<N>
            => N.Pi / N.CreateChecked(2); // 90 degrees

        public const string SchlafliSymbol = "{} x {}";

        //public static double d => Rectangle.Perimeter.Intersection.FromEdges(default, default);

        /// <summary>
        /// Je primo na obvodu.
        /// </summary>
        public static bool IncludesPoint(double topx1, double topx2, double px1, double px2) =>
            ((px1 == 0 || px1 == topx1) && px2 >= 0 && px2 <= topx2) ||
            ((px2 == 0 || px2 == topx2) && px1 >= 0 && px1 <= topx1);


        /// <summary>
        /// Line intersection rectangle.
        /// </summary>
        /// <returns>
        ///     (0): no intersection
        ///     (1): single point - vertice
        ///     (2): two points
        ///     (int.max): identical with any edge
        /// </returns>
        //public static int IntersectsLine(double topx1, double topx2, double a1, double a2, double c,
        //   out (double X1, double X2) ip1, out (double X1, double X2) ip2)
        //{
        //    var ix1 = LineSegment.IntersectsLine(0, 0, topx1, 0, a1, a2, c, out var ipx1);
        //    if (ix1 == 1)
        //    {
        //    }
        //    else if (ix1 == int.MaxValue)
        //    {
        //        ip1 = (double.PositiveInfinity, double.PositiveInfinity);
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

        //public static bool IntersectsRay(double topx1, double topx2, double a1, double a2, double c,
        //    out (double X1, double X2) ip1, out (double X1, double X2) ip2)
        //{

        //    ip1 = default;
        //    ip2 = default;
        //    return false;
        //}

        public static class Vertices
        {
            public static (double X, double Y) FromCenterPoint(double xcenter, double ycenter, double halfWidth, double halfHeight, double rotationAngle)
            {


                return default;
            }

            /// <summary>
            /// Get rest of vertices from two initial vertices and height.
            /// </summary>
            public static (double X, double Y) FromTwoVertices(double xa, double ya, double xb, double yb, double height)
            {


                return default;
            }
        }

        public static class Edge
        {
            public static double FromArea(double area, double a) => area / a;
        }

        public static class Diagonal
        {
            public static double FromEdges(double a, double b) => PT.Hypotenuse(a, b);
        }

        public static class Perimeter
        {
            public static double FromEdges(double a, double b) => 2 * (a + b);

            public static class Points
            {
                public static IEnumerable<(double X1, double X2)> Indexes(int count, double a = 1, double b = 1)
                {
                    //todo urcite pujde pocitat lepe
                    var perimetr = Perimeter.FromEdges(a, b);
                    foreach (var index in D1.LineSegment.Indexes(count, perimetr))
                    {
                        if (index <= a)
                            yield return (index, 0);
                        else if (index <= a + b)
                            yield return (a, index - a);
                        else if (index <= 2 * a + b)
                            yield return (a - (index - a - b), b);
                        else
                            yield return (0, b - perimetr - index);
                    }
                }
            }

            //public static class Intersection //?
            //{
            //}
        }
    }
}

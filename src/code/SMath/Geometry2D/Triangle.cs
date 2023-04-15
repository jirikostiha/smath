using System.Numerics;

namespace SMath.GeometryD2
{
    /// <summary>
    /// Triangle
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Triangle">wikipedia</a>
    /// </remarks>
    public static class Triangle
    {
        public static N VertexCount<N>()
            where N : INumberBase<N>
            => N.CreateChecked(3);

        public static N EdgeCount<N>()
            where N : INumberBase<N>
            => N.CreateChecked(3);

        public const string SchlafliSymbol = "{3}";

        /// <summary>
        /// Perimeter of a triangle.
        /// </summary>
        public static class Perimeter
        {
            /// <summary>
            /// Length of perimeter of a triangle.
            /// </summary>
            /// <remarks>
            /// <a href="https://en.wikipedia.org/wiki/..">wikipedia</a>
            /// </remarks>
            public static class Length
            {
                public static N FromEdges<N>(N a, N b, N c)
                    where N : IAdditionOperators<N, N, N>
                    => a + b + c;
            }
        }

        /// <summary>
        /// Region of a triangle.
        /// </summary>
        public static class Region
        {
            /// <summary>
            /// Area of a plane region enclosed by triangle.
            /// </summary>
            /// <remarks>
            /// <a href="https://en.wikipedia.org/wiki/Area_of_a_triangle">wikipedia</a>
            /// </remarks>
            public static class Area
            {
                /// <summary>
                /// Area of a rectangle by Heron's formula.
                /// </summary>
                public static N FromEdges<N>(N a, N b, N c)
                    where N : IRootFunctions<N>
                {
                    var semiperimeter = Perimeter.Length.FromEdges(a, b, c) / N.CreateChecked(2);
                    return N.Sqrt(semiperimeter * (semiperimeter - a) * (semiperimeter - b) * (semiperimeter - c));
                }

                /// <summary>
                /// Area of a rectangle by base and height.
                /// </summary>
                /// <param name="base"> The length of the base of the triangle. </param>
                /// <param name="height"> The length of a perpendicular from the vertex opposite the base onto the line containing the base. </param>
                public static N FromHeight<N>(N @base, N height)
                    where N : INumberBase<N>
                    => @base * height / N.CreateChecked(2);
            }
        }

        /// <summary>
        /// Equilateral triangle.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Equilateral_triangle">wikipedia</a>
        /// </remarks>
        public static class Equilateral
        {
            // 60 degrees
            public static N InternalAngle<N>()
                where N : IFloatingPoint<N>
                => N.Pi / N.CreateChecked(3);

            /// <summary>
            /// Perimeter of an equilateral triangle.
            /// </summary>
            public static class Perimeter
            {
                public static class Length
                {
                    public static N FromEdges<N>(N edgeLength)
                        where N : INumberBase<N>
                        => EdgeCount<N>() * edgeLength;
                }
            }

            /// <summary>
            /// Region of an equilateral triangle.
            /// </summary>
            public static class Region
            {
                /// <summary>
                /// Area of a plane region enclosed by equilateral triangle.
                /// </summary>
                public static class Area
                {
                    public static N FromEdge<N>(N a)
                        where N : IRootFunctions<N>
                        => (N.Sqrt(N.CreateChecked(3)) / N.CreateChecked(4)) * a * a;
                }
            }
        }

        /// <summary>
        /// Right triangle.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Right_triangle">wikipedia</a>
        /// </remarks>
        public static class Right
        {
            /// </summary>
            /// <summary>
            /// Region of a right triangle.
            /// </summary>
            public static class Region
            {
                /// <summary>
                /// Area of a plane region enclosed by right triangle.
                /// </summary>
                public static class Area
                {
                    /// <summary>
                    /// Area of a rectangle by base and height.
                    /// </summary>
                    /// <param name="cathetus1"> The length of the first side adjacent to the right angle. </param>
                    /// <param name="cathetus2"> The length of the second side adjacent to the right angle. </param>
                    /// <returns> Surface area. </returns>
                    public static N FromCathetuses<N>(N cathetus1, N cathetus2)
                        where N : INumberBase<N>
                        => cathetus1 * cathetus2 / N.CreateChecked(2);

                    /// <summary>
                    /// Area of a rectangle by hypotenuse and height.
                    /// </summary>
                    /// <param name="hypotenuse"> The length of the hypotenuse. </param>
                    /// <param name="height"> The length of a perpendicular from the vertex opposite the base onto the line containing the base. </param>
                    /// <returns> Surface area. </returns>
                    public static N FromHypotenuse<N>(N hypotenuse, N height)
                        where N : INumberBase<N>
                        => hypotenuse * height / N.CreateChecked(2);
                }
            }
        }

        /// <summary>
        /// Golden triangle.
        /// </summary>
        /// <remarks>
        /// /// <a href="https://en.wikipedia.org/wiki/Golden_triangle_(mathematics)">wikipedia</a>
        /// </remarks>
        public static class Golden
        {
            //pentagon vychazi z GoldenTriangles
        }
    }
}

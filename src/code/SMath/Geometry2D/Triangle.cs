using System.Numerics;

namespace SMath.Geometry2D;

/// <summary>
/// Triangle, the polygon with three edges. Edges are denoted <c>a</c>, <c>b</c> and <c>c</c>,
/// every quantity derived from a single edge belongs to the edge <c>a</c>.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Triangle">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Triangle.html">Wolfram Mathworld</a>
/// </remarks>
public static class Triangle
{
    /// <summary> Count of vertices. </summary>
    public static N VertexCount<N>()
        where N : INumberBase<N>
        => N.CreateTruncating(3);

    /// <summary> Count of edges. </summary>
    public static N EdgeCount<N>()
        where N : INumberBase<N>
        => N.CreateTruncating(3);

    /// <summary> Count of diagonals. A triangle has none. </summary>
    public static N DiagonalCount<N>()
        where N : INumberBase<N>
        => N.Zero;

    /// <summary> Sum of internal angles in rads. </summary>
    public static N InternalAngleSum<N>()
        where N : IFloatingPointConstants<N>
        => N.Pi;

    /// <summary> Schläfli symbol of the regular, that is equilateral, triangle. </summary>
    public const string SchlafliSymbol = "{3}";

    /// <summary>
    /// Perimeter or outline of a triangle.
    /// </summary>
    public static class Perimeter
    {
        /// <summary>
        /// Length of the perimeter of a triangle.
        /// </summary>
        public static class Length
        {
            public static N FromEdges<N>(N a, N b, N c)
                where N : IAdditionOperators<N, N, N>
                => a + b + c;
        }
    }

    /// <summary>
    /// Semiperimeter of a triangle, half the length of its perimeter.
    /// </summary>
    public static class Semiperimeter
    {
        public static N FromEdges<N>(N a, N b, N c)
            where N : INumberBase<N>
            => Perimeter.Length.FromEdges(a, b, c) / N.CreateChecked(2);
    }

    /// <summary>
    /// Internal angle of a triangle.
    /// </summary>
    public static class InternalAngle
    {
        /// <summary>
        /// Internal angle opposite to the edge <paramref name="a"/> in rads, by the law of cosines.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Law_of_cosines">Wikipedia</a>
        /// </remarks>
        public static N FromEdges<N>(N a, N b, N c)
            where N : ITrigonometricFunctions<N>
            => N.Acos(((b * b) + (c * c) - (a * a)) / (N.CreateChecked(2) * b * c));
    }

    /// <summary>
    /// Height or altitude of a triangle, the distance between an edge and the opposite vertex.
    /// </summary>
    public static class Height
    {
        /// <summary> Height perpendicular to the edge <paramref name="a"/>. </summary>
        public static N FromEdges<N>(N a, N b, N c)
            where N : IRootFunctions<N>
            => N.CreateChecked(2) * Region.Area.FromEdges(a, b, c) / a;

        /// <summary> Height perpendicular to the base from the area of a triangle. </summary>
        public static N FromArea<N>(N @base, N area)
            where N : INumberBase<N>
            => N.CreateChecked(2) * area / @base;
    }

    /// <summary>
    /// Median of a triangle, the segment joining a vertex with the midpoint of the opposite edge.
    /// </summary>
    public static class Median
    {
        /// <summary> Length of the median to the edge <paramref name="a"/>. </summary>
        public static N FromEdges<N>(N a, N b, N c)
            where N : IRootFunctions<N>
            => N.Sqrt((N.CreateChecked(2) * b * b) + (N.CreateChecked(2) * c * c) - (a * a))
                / N.CreateChecked(2);
    }

    /// <summary>
    /// Circumradius of a triangle, the radius of the circle passing through all its vertices.
    /// </summary>
    public static class Circumradius
    {
        public static N FromEdges<N>(N a, N b, N c)
            where N : IRootFunctions<N>
            => a * b * c / (N.CreateChecked(4) * Region.Area.FromEdges(a, b, c));

        /// <summary> Circumradius from an edge and the angle opposite to it, by the law of sines. </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Law_of_sines">Wikipedia</a>
        /// </remarks>
        public static N FromEdgeAndOppositeAngle<N>(N a, N angle)
            where N : ITrigonometricFunctions<N>
            => a / (N.CreateChecked(2) * N.Sin(angle));
    }

    /// <summary>
    /// Inradius of a triangle, the radius of the circle inscribed in it.
    /// </summary>
    public static class Inradius
    {
        public static N FromEdges<N>(N a, N b, N c)
            where N : IRootFunctions<N>
            => Region.Area.FromEdges(a, b, c) / Semiperimeter.FromEdges(a, b, c);
    }

    /// <summary>
    /// Centroid of a triangle, the intersection of its medians.
    /// </summary>
    public static class Centroid
    {
        public static (N X, N Y) FromVertices<N>((N X, N Y) vertex1, (N X, N Y) vertex2, (N X, N Y) vertex3)
            where N : INumberBase<N>
        {
            var three = N.CreateChecked(3);
            return ((vertex1.X + vertex2.X + vertex3.X) / three,
                (vertex1.Y + vertex2.Y + vertex3.Y) / three);
        }
    }

    /// <summary>
    /// Enclosed plane region of a triangle.
    /// </summary>
    public static class Region
    {
        /// <summary>
        /// Enclosed plane region area of a triangle.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Area_of_a_triangle">Wikipedia</a>
        /// </remarks>
        public static class Area
        {
            /// <summary>
            /// Area of a triangle by Heron's formula.
            /// </summary>
            public static N FromEdges<N>(N a, N b, N c)
                where N : IRootFunctions<N>
            {
                var semiperimeter = Semiperimeter.FromEdges(a, b, c);
                return N.Sqrt(semiperimeter * (semiperimeter - a) * (semiperimeter - b) * (semiperimeter - c));
            }

            /// <summary>
            /// Area of a triangle by base and height.
            /// </summary>
            /// <param name="base"> The length of the base of the triangle. </param>
            /// <param name="height"> The length of a perpendicular from the vertex opposite the base onto the line containing the base. </param>
            public static N FromHeight<N>(N @base, N height)
                where N : INumberBase<N>
                => @base * height / N.CreateChecked(2);

            /// <summary>
            /// Area of a triangle by two edges and the angle they enclose.
            /// </summary>
            public static N FromEdgesAndAngle<N>(N a, N b, N angle)
                where N : ITrigonometricFunctions<N>
                => a * b * N.Sin(angle) / N.CreateChecked(2);

            /// <summary>
            /// Area of a triangle by its edges and circumradius.
            /// </summary>
            public static N FromEdgesAndCircumradius<N>(N a, N b, N c, N circumradius)
                where N : INumberBase<N>
                => a * b * c / (N.CreateChecked(4) * circumradius);

            /// <summary>
            /// Area of a triangle by its edges and inradius.
            /// </summary>
            public static N FromEdgesAndInradius<N>(N a, N b, N c, N inradius)
                where N : INumberBase<N>
                => Semiperimeter.FromEdges(a, b, c) * inradius;

            /// <summary>
            /// Area of a triangle by the coordinates of its vertices.
            /// </summary>
            public static N FromVertices<N>((N X, N Y) vertex1, (N X, N Y) vertex2, (N X, N Y) vertex3)
                where N : INumberBase<N>
                => N.Abs(((vertex2.X - vertex1.X) * (vertex3.Y - vertex1.Y))
                    - ((vertex3.X - vertex1.X) * (vertex2.Y - vertex1.Y))) / N.CreateChecked(2);
        }
    }

    /// <summary>
    /// Equilateral triangle, the triangle with three edges of equal length.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Equilateral_triangle">Wikipedia</a>
    /// </remarks>
    public static class Equilateral
    {
        /// <summary> 60 degrees in rads. </summary>
        public static N InternalAngle<N>()
            where N : IFloatingPointConstants<N>
            => N.Pi / N.CreateChecked(3);

        /// <summary> Edge of an equilateral triangle. </summary>
        public static class Edge
        {
            public static N FromHeight<N>(N height)
                where N : IRootFunctions<N>
                => N.CreateChecked(2) * height / N.Sqrt(N.CreateChecked(3));

            public static N FromCircumradius<N>(N circumradius)
                where N : IRootFunctions<N>
                => circumradius * N.Sqrt(N.CreateChecked(3));

            public static N FromInradius<N>(N inradius)
                where N : IRootFunctions<N>
                => N.CreateChecked(2) * inradius * N.Sqrt(N.CreateChecked(3));
        }

        /// <summary> Height or altitude of an equilateral triangle. </summary>
        public static class Height
        {
            public static N FromEdge<N>(N edgeLength)
                where N : IRootFunctions<N>
                => edgeLength * N.Sqrt(N.CreateChecked(3)) / N.CreateChecked(2);
        }

        /// <summary> Circumradius of an equilateral triangle. </summary>
        public static class Circumradius
        {
            public static N FromEdge<N>(N edgeLength)
                where N : IRootFunctions<N>
                => edgeLength / N.Sqrt(N.CreateChecked(3));
        }

        /// <summary> Inradius of an equilateral triangle. </summary>
        public static class Inradius
        {
            public static N FromEdge<N>(N edgeLength)
                where N : IRootFunctions<N>
                => edgeLength / (N.CreateChecked(2) * N.Sqrt(N.CreateChecked(3)));
        }

        /// <summary>
        /// Perimeter or outline of an equilateral triangle.
        /// </summary>
        public static class Perimeter
        {
            /// <summary>
            /// Length of the perimeter of an equilateral triangle.
            /// </summary>
            public static class Length
            {
                public static N FromEdge<N>(N edgeLength)
                    where N : INumberBase<N>
                    => EdgeCount<N>() * edgeLength;

                public static N FromHeight<N>(N height)
                    where N : IRootFunctions<N>
                    => EdgeCount<N>() * Edge.FromHeight(height);
            }
        }

        /// <summary>
        /// Enclosed plane region of an equilateral triangle.
        /// </summary>
        public static class Region
        {
            /// <summary>
            /// Enclosed plane region area of an equilateral triangle.
            /// </summary>
            public static class Area
            {
                public static N FromEdge<N>(N edgeLength)
                    where N : IRootFunctions<N>
                    => N.Sqrt(N.CreateChecked(3)) / N.CreateChecked(4) * edgeLength * edgeLength;

                public static N FromHeight<N>(N height)
                    where N : IRootFunctions<N>
                    => height * height / N.Sqrt(N.CreateChecked(3));
            }
        }
    }

    /// <summary>
    /// Isosceles triangle, the triangle with two edges of equal length. The two equal edges are
    /// the legs, the remaining one is the base.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Isosceles_triangle">Wikipedia</a>
    /// </remarks>
    public static class Isosceles
    {
        /// <summary> Height or altitude of an isosceles triangle perpendicular to its base. </summary>
        public static class Height
        {
            public static N FromLegAndBase<N>(N legLength, N baseLength)
                where N : IRootFunctions<N>
                => PT.Leg(legLength, baseLength / N.CreateChecked(2));
        }

        /// <summary> Internal angle of an isosceles triangle. </summary>
        public static class InternalAngle
        {
            /// <summary> Internal angle at the apex, the vertex opposite to the base. </summary>
            public static class Apex
            {
                public static N FromLegAndBase<N>(N legLength, N baseLength)
                    where N : ITrigonometricFunctions<N>
                    => N.CreateChecked(2) * N.Asin(baseLength / (N.CreateChecked(2) * legLength));
            }

            /// <summary> Internal angle at the base. Both base angles are equal. </summary>
            public static class Base
            {
                public static N FromLegAndBase<N>(N legLength, N baseLength)
                    where N : ITrigonometricFunctions<N>
                    => N.Acos(baseLength / (N.CreateChecked(2) * legLength));
            }
        }

        /// <summary>
        /// Perimeter or outline of an isosceles triangle.
        /// </summary>
        public static class Perimeter
        {
            /// <summary>
            /// Length of the perimeter of an isosceles triangle.
            /// </summary>
            public static class Length
            {
                public static N FromLegAndBase<N>(N legLength, N baseLength)
                    where N : INumberBase<N>
                    => (N.CreateChecked(2) * legLength) + baseLength;
            }
        }

        /// <summary>
        /// Enclosed plane region of an isosceles triangle.
        /// </summary>
        public static class Region
        {
            /// <summary>
            /// Enclosed plane region area of an isosceles triangle.
            /// </summary>
            public static class Area
            {
                public static N FromLegAndBase<N>(N legLength, N baseLength)
                    where N : IRootFunctions<N>
                    => baseLength * Height.FromLegAndBase(legLength, baseLength) / N.CreateChecked(2);
            }
        }
    }

    /// <summary>
    /// Right triangle, the triangle with one right angle. The two edges adjacent to it are the
    /// cathetuses, the remaining one is the hypotenuse.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Right_triangle">Wikipedia</a>
    /// </remarks>
    public static class Right
    {
        /// <summary> Hypotenuse of a right triangle, the edge opposite to the right angle. </summary>
        public static class Hypotenuse
        {
            public static N FromCathetuses<N>(N cathetus1, N cathetus2)
                where N : IRootFunctions<N>
                => PT.Hypotenuse(cathetus1, cathetus2);

            public static N FromCircumradius<N>(N circumradius)
                where N : INumberBase<N>
                => N.CreateChecked(2) * circumradius;
        }

        /// <summary> Cathetus of a right triangle, an edge adjacent to the right angle. </summary>
        public static class Cathetus
        {
            public static N FromHypotenuse<N>(N hypotenuse, N otherCathetus)
                where N : IRootFunctions<N>
                => PT.Leg(hypotenuse, otherCathetus);
        }

        /// <summary> Height or altitude of a right triangle perpendicular to its hypotenuse. </summary>
        public static class Height
        {
            public static N FromCathetuses<N>(N cathetus1, N cathetus2)
                where N : IRootFunctions<N>
                => cathetus1 * cathetus2 / PT.Hypotenuse(cathetus1, cathetus2);
        }

        /// <summary>
        /// Circumradius of a right triangle. Its circumcenter is the midpoint of the hypotenuse.
        /// </summary>
        public static class Circumradius
        {
            public static N FromHypotenuse<N>(N hypotenuse)
                where N : INumberBase<N>
                => hypotenuse / N.CreateChecked(2);
        }

        /// <summary> Inradius of a right triangle. </summary>
        public static class Inradius
        {
            public static N FromCathetuses<N>(N cathetus1, N cathetus2)
                where N : IRootFunctions<N>
                => (cathetus1 + cathetus2 - PT.Hypotenuse(cathetus1, cathetus2)) / N.CreateChecked(2);
        }

        /// <summary>
        /// Perimeter or outline of a right triangle.
        /// </summary>
        public static class Perimeter
        {
            /// <summary>
            /// Length of the perimeter of a right triangle.
            /// </summary>
            public static class Length
            {
                public static N FromCathetuses<N>(N cathetus1, N cathetus2)
                    where N : IRootFunctions<N>
                    => cathetus1 + cathetus2 + PT.Hypotenuse(cathetus1, cathetus2);
            }
        }

        /// <summary>
        /// Enclosed plane region of a right triangle.
        /// </summary>
        public static class Region
        {
            /// <summary>
            /// Enclosed plane region area of a right triangle.
            /// </summary>
            public static class Area
            {
                /// <summary>
                /// Area of a triangle by cathetuses.
                /// </summary>
                /// <param name="cathetus1"> The length of the first side adjacent to the right angle. </param>
                /// <param name="cathetus2"> The length of the second side adjacent to the right angle. </param>
                /// <returns> Surface area. </returns>
                public static N FromCathetuses<N>(N cathetus1, N cathetus2)
                    where N : INumberBase<N>
                    => cathetus1 * cathetus2 / N.CreateChecked(2);

                /// <summary>
                /// Area of a triangle by hypotenuse and height.
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
}

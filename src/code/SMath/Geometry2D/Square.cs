using System.Numerics;

namespace SMath.GeometryD2
{
    /// <summary>
    /// Square
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Square">wikipedia</a>
    /// </remarks>
    public static class Square
    {
        public static N VertexCount<N>()
            where N : INumberBase<N>
            => Rectangle.VertexCount<N>();

        public static N EdgeCount<N>()
            where N : INumberBase<N>
            => Rectangle.EdgeCount<N>();

        // 90 degrees
        public static N InternalAngle<N>()
            where N : IFloatingPoint<N>
            => Rectangle.InternalAngle<N>();

        public const string SchlafliSymbol = "{4}";

        public static class Perimeter
        {
            public static class Length
            {
                public static N Perimeter<N>(N edgeLength)
                    where N : INumberBase<N>
                    => EdgeCount<N>() * edgeLength;
            }

            public static class Points
            {
                /// <summary>
                /// Split perimeter to n segments and return coords of splitting points
                /// in ++ quadrant
                /// </summary>
                //public static IEnumerable<(double X1, double X2)> Indexes(int count, double edgeLength = 1)
                //    => Rectangle.Perimeter.Points.(count, edgeLength, edgeLength);
            }
        }

        public static class Region
        {
            public static class Area
            {
                public static double FromEdge<N>(double edgeLength)
                    where N : IMultiplyOperators<N, N, N>
                    => edgeLength * edgeLength;

                public static N FromDiagonal<N>(N diagonal)
                    where N : INumberBase<N>
                    => diagonal * diagonal / N.CreateChecked(2);

                public static N FromCircumradius<N>(N circumradius)
                    where N : INumberBase<N>
                    => N.CreateChecked(2) * circumradius * circumradius;

                public static N AreaByInradius<N>(N inradius)
                    where N : INumberBase<N>
                    => N.CreateChecked(4) * inradius * inradius;
            }
        }
    }
}

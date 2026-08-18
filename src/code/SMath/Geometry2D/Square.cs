using System.Numerics;

namespace SMath.Geometry2D
{
    /// <summary>
    /// Square shape.
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

        /// <summary>
        /// 90 degrees (Pi / 2 radians).
        /// </summary>
        public static N InternalAngle<N>()
            where N : ITrigonometricFunctions<N>
            => Rectangle.InternalAngle<N>();

        public const string SchlafliSymbol = "{4}";

        public static class Perimeter
        {
            public static class Length
            {
                public static N FromEdge<N>(N edgeLength)
                    where N : INumberBase<N>
                    => EdgeCount<N>() * edgeLength;
            }
        }

        public static class Region
        {
            public static class Area
            {
                public static N FromEdge<N>(N edgeLength)
                    where N : IMultiplyOperators<N, N, N>
                    => edgeLength * edgeLength;

                public static N FromDiagonal<N>(N diagonal)
                    where N : INumberBase<N>
                    => diagonal * diagonal / N.CreateChecked(2);

                public static N FromCircumradius<N>(N circumradius)
                    where N : INumberBase<N>
                    => N.CreateChecked(2) * circumradius * circumradius;

                public static N FromInradius<N>(N inradius)
                    where N : INumberBase<N>
                    => N.CreateChecked(4) * inradius * inradius;
            }
        }
    }
}

using System.Numerics;

namespace SMath.Geometry2D
{
    /// <summary>
    /// Regular hexagon shape.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Hexagon#Regular_hexagon">wikipedia</a>
    /// </remarks>
    public static class RegularHexagon
    {
        public static N VertexCount<N>()
            where N : INumberBase<N>
            => N.CreateChecked(6);

        public static N EdgeCount<N>()
            where N : INumberBase<N>
            => N.CreateChecked(6);

        /// <summary>
        /// 120 degrees (2*Pi / 3 radians).
        /// </summary>
        public static N InternalAngle<N>()
            where N : IFloatingPoint<N>
            => N.CreateChecked(2) * N.Pi / N.CreateChecked(3);

        public const string SchlafliSymbol = "{6}";

        public static class Perimeter
        {
            public static class Length
            {
                public static N FromEdge<N>(N edgeLength)
                    where N : INumberBase<N>
                    => EdgeCount<N>() * edgeLength;
            }
        }

        /// <summary>
        /// Region of regular hexagon.
        /// </summary>
        public static class Region
        {
            /// <summary>
            /// Area of regular hexagon.
            /// </summary>
            public static class Area
            {
                public static N FromEdge<N>(N a)
                    where N : IRootFunctions<N>
                    => (N.CreateChecked(3) * N.Sqrt(N.CreateChecked(3)) / N.CreateChecked(2)) * a * a;
            }
        }
    }
}

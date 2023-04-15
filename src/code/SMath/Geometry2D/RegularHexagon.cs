using System.Numerics;

namespace SMath.GeometryD2
{
    /// <summary>
    /// Regular hexagon.
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

        public static N InternalAngle<N>()
            where N : IFloatingPoint<N>
            =>  N.CreateChecked(2 / 3) * N.Pi; // 120 degrees

        public const string SchlafliSymbol = "{6}, t{3}";

        public static class Perimeter
        {
            public static N FromEdges<N>(N edgeLength)
                where N : INumberBase<N>
                => EdgeCount<N>() * edgeLength;
        }
    }
}

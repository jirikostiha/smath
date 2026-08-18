using System.Numerics;

namespace SMath.Geometry2D
{
    /// <summary>
    /// Rhombus shape.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Rhombus">wikipedia</a>
    /// </remarks>
    public static class Rhombus
    {
        public static N VertexCount<N>()
            where N : INumberBase<N>
            => N.CreateChecked(4);

        public static N EdgeCount<N>()
            where N : INumberBase<N>
            => N.CreateChecked(4);

        public const string SchlafliSymbol = "{} + {}";

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
                public static N FromHeight<N>(N edgeLength, N height)
                    where N : INumberBase<N>
                    => edgeLength * height;

                public static N FromDiagonals<N>(N p, N q)
                    where N : INumberBase<N>
                    => p * q / N.CreateChecked(2);

                public static N FromEdgeAndAngle<N>(N edgeLength, N vertexAngle)
                    where N : ITrigonometricFunctions<N>
                    => edgeLength * edgeLength * N.Sin(vertexAngle);

                public static N FromHeightAndAngle<N>(N height, N vertexAngle)
                    where N : ITrigonometricFunctions<N>
                    => height * height / N.Sin(vertexAngle);
            }
        }
    }
}

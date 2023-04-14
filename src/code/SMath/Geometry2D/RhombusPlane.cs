namespace Wayout.Mathematics.Geometry.D2
{
    using System;
    using Wayout.Mathematics.Functions;

    /// <summary>
    /// Rhombus plane
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Rhombus">wikipedia</a>
    /// </remarks>
    public static class RhombusPlane
    {
        public static double AreaByHeigh(double edgeLength, double height) => edgeLength * height;

        public static double AreaByVertexAngle(double edgeLength, double vertexAngle) 
            => Power2.f(edgeLength) * Math.Sin(vertexAngle);

        public static double AreaByHeighAndVertexAngle(double height, double vertexAngle) 
            => Power2.f(height) / Math.Sin(vertexAngle);
    }
}

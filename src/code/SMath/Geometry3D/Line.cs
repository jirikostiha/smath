namespace Wayout.Mathematics.Geometry.D3
{
    /// <summary>
    /// Line in 3 dimensions.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Line_(geometry)">wikipedia</a>
    /// <a href="https://en.wikipedia.org/wiki/Linear_equation">wikipedia</a>
    /// </remarks>
    public static class Line
    {
        /// <summary>
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line">wikipedia</a>
        /// <a href="http://paulbourke.net/geometry/pointlineplane/source.c">paulbourke</a>
        /// </remarks>
        public static double DistanceOfPoint(
                double a1, double a2, double a3, double c,
                double px1, double px2, double px3)
                =>
                0; // Abs(a1 * px1 + a2 * px2 + c) / PythagorasTheorem.Calculate(a1, a2);
    }
}

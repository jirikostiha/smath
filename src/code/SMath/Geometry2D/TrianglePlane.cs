namespace Wayout.Mathematics.Geometry.D2
{
    /// <summary>
    /// Triangle plane
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Triangle">wikipedia</a>
    /// </remarks>
    public static class TrianglePlane
    {
        /// <summary> Surface area. Heron's formula. </summary>
        public static double Area(double v1v2Length, double v1v3Length, double v2v3Length)
        {
            //var s = PerimeterLength(v1v2Length, v1v3Length, v2v3Length) / 2;
            //return Math.Sqrt(s * (s - v1v2Length) * (s - v1v3Length) * (s - v2v3Length));
            return 0;
        }

        /// <summary>
        /// Area of a rectangle by base and height.
        /// </summary>
        /// <param name="base"> The length of the base of the triangle. </param>
        /// <param name="height"> The length of a perpendicular from the vertex opposite the base onto the line containing the base. </param>
        /// <returns> Surface area. </returns>
        public static double Area(double @base, double height)
        {
            return @base * height / 2;
        }
    }

    /// <summary>
    /// Right triangle plane
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Right_triangle">wikipedia</a>
    /// </remarks>
    public static class RightTrianglePlane
    {
        /// <summary>
        /// Area of a rectangle by base and height.
        /// </summary>
        /// <param name="cathetus1"> The length of the first side adjacent to the right angle. </param>
        /// <param name="cathetus2"> The length of the second side adjacent to the right angle. </param>
        /// <returns> Surface area. </returns>
        public static double AreaByCathetuses(double cathetus1, double cathetus2)
        {
            return cathetus1 * cathetus2 / 2;
        }

        /// <summary>
        /// Area of a rectangle by hypotenuse and height.
        /// </summary>
        /// <param name="hypotenuse"> The length of the hypotenuse. </param>
        /// <param name="height"> The length of a perpendicular from the vertex opposite the base onto the line containing the base. </param>
        /// <returns> Surface area. </returns>
        public static double AreaByHypotenuse(double hypotenuse, double height)
        {
            return hypotenuse * height / 2;
        }
    }
}

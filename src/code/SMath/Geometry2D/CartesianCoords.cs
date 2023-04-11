namespace Wayout.Mathematics.Geometry
{
    using System;
    using Wayout.Mathematics.Functions;
    using Wayout.Mathematics.Functions.P1.Trigonometricf;
    using static System.Math;

    /// <summary>
    /// Cartesian coordinates.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Cartesian_coordinate_system">wikipedia</a>
    /// </remarks>
    public static class CartesianCoords
    {
        public static (double X1, double X2) Rotate(double x1, double x2, double cx1, double cx2, double angle)
            => (cx1 + (x1 - cx1) * Cos(angle) - (x2 - cx2) * Sin(angle),
                cx2 + (x1 - cx1) * Sin(angle) + (x2 - cx2) * Cos(angle));

        //public static (double X1, double X2) Rotate((double X1, double X2) coords, (double X1, double X2) centre, double angle)
        //    => (centre.X1 + (coords.X1 - centre.X1) * Cos(angle) - (coords.X2 - centre.X2) * Sin(angle),
        //        centre.X2 + (coords.X1 - centre.X1) * Sin(angle) + (coords.X2 - centre.X2) * Cos(angle));

        //transform?

        //to spherical..
        //funkce z GeometricVector2


        /// <summary> Euclidean distance from origin </summary>
        //public static double Distance(double x1, double x2) => PythagorasTheorem.Hypotenuse(x1, x2);

        ///// <summary> Euclidean distance between two points </summary>
        //public static double Distance(double p1x1, double p1x2, double p2x1, double p2x2) => PythagorasTheorem.Hypotenuse(p2x1 - p1x1, p2x2 - p1x2);

        ///// <summary> Manhattan distance from origin </summary>
        //public static double ManhattanDistance(double x1, double x2) => Abs(x1) + Abs(x2);

        ///// <summary> Manhattan distance between two points </summary>
        //public static double ManhattanDistance(double p1x1, double p1x2, double p2x1, double p2x2) => Abs(p1x1 - p2x1) + Abs(p1x2 - p2x2);

    }
}

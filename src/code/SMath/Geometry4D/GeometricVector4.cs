namespace Wayout.Mathematics.Geometry
{
    using System;
    using Wayout.Mathematics.Functions;
    using static System.Math;

    /// <summary>
    /// Euclidean or geometric vector with 4 components.
    /// </summary>
    /// <remarks>
    /// </remarks>
    public static class GeometricVector4
    {
        //Magnitude
        public static double Length(double x1, double x2, double x3, double x4) => PythagorasTheorem.Hypotenuse(x1,x2,x3,x4);

        /// <summary> Polar angle. Angle from x1 axis to x2 axis. </summary>
        public static double Φ1(double x1, double x2) => GeometricVector3.Φ1(x1, x2);

        /// <summary> Angle from x1-x2 plane to x3 axis. </summary>
        public static double Φ2(double x1, double x2, double x3) => GeometricVector3.Φ2(x1,x2,x3);

        /// <summary> Angle from x1-x2-x3 space to x4 axis. </summary>
        public static double Φ3(double x1, double x2, double x3, double x4) => throw new NotImplementedException("todo");

        //public static double X1() => throw new NotImplementedException("todo");

        //public static double X2() => throw new NotImplementedException("todo");

        //public static double X3() => throw new NotImplementedException("todo");

        //public static double X4() => throw new NotImplementedException("todo");

        public static double[] Normalized(double x1, double x2, double x3, double x4)
        {
            var length = Length(x1, x2, x3, x4);
            return new[] { x1 / length, x2 / length, x3 / length };
        }
    }
}

namespace Wayout.Mathematics.Geometry
{
    using System;
    using Wayout.Mathematics.Functions;
    using static System.Math;

    /// <summary>
    /// Euclidean or geometric vector with 3 components.
    /// </summary>
    /// <remarks>
    /// https://en.wikipedia.org/wiki/Spherical_coordinate_system
    /// https://mathworld.wolfram.com/SphericalCoordinates.html
    /// https://mathworld.wolfram.com/CylindricalCoordinates.html
    /// https://keisan.casio.com/exec/system/1359533867
    /// https://dynref.engr.illinois.edu/rvs.html
    /// </remarks>
    public static class GeometricVector3
    {
        /// <summary>
        /// Length or magnitude or scalar value of the vector.
        /// </summary>
        public static double Length(double x1, double x2, double x3) => PythagorasTheorem.Hypotenuse(x1,x2,x3);

        /// <summary>
        /// Length or magnitude or scalar value of the vector.
        /// </summary>
        public static double Length(double r12, double x3) => PythagorasTheorem.Hypotenuse(r12, x3);

        /// <summary> Polar angle. Angle from x1 axis to x2 axis. </summary>
        public static double Φ1(double x1, double x2) => GeometricVector2.Φ1(x1, x2);

        /// <summary> Official azimutal angle. </summary>
        public static double Φ2a(double length, double x3) => Acos(x3 / length);
        
        /// <summary> Official azimutal angle </summary>
        public static double Φ2a(double x1, double x2, double x3) => Φ2a(Length(x1, x2, x3), x3);

        /// <summary> Angle from x1-x2 plane to x3 axis. </summary>
        public static double Φ2(double x1, double x2, double x3) => throw new NotImplementedException("todo");

        public static double X1(double length, double φ1, double φ2a) => length * Cos(φ1) * Sin(φ2a);

        public static double X1(double r12, double φ1) => r12 * Cos(φ1);

        public static double X2(double length, double φ1, double φ2a) => length * Sin(φ1) * Sin(φ2a);

        public static double X2(double r12, double φ1) => r12 * Sin(φ1);

        public static double X3(double length, double φ2a) => length * Cos(φ2a);

        /// <summary>
        /// Length or magnitude or scalar value of the vector in x1-x2 plane. It is measured from x1 axis to x2 axis.
        /// </summary>
        public static double R12(double x1, double x2) => GeometricVector2.Magnitude(x1, x2);

        //public static double R12(double x1, double φ1) => GeometricVector2.;

        //public static double R12(double x2, double φ1) => GeometricVector2.;

        /// <summary>
        /// Get normalized components of given vector.
        /// </summary>
        public static double[] Normalized(double x1, double x2, double x3)
        {
            var length = Length(x1, x2, x3);
            return new[] { x1 / length, x2 / length, x3 / length };
        }

        public static double Distance(double v1x1, double v1x2, double v1x3, double v2x1, double v2x2, double v2x3)
            => Root2.f(Power2.f(v1x1 - v2x1) + Power2.f(v1x2 - v2x2) + Power2.f(v1x3 - v2x3));

        /// <summary> Distance of two points given in spherical coordinates. </summary>
        /// <remarks> https://en.wikipedia.org/wiki/Spherical_coordinate_system#Distance_in_spherical_coordinates </remarks>
        public static double DistanceOfSpherical(double v1Length, double v1φ1, double v1φ2a, double v2Length, double v2φ1, double v2φ2a)
            => Root2.f(Power2.f(v1Length) + Power2.f(v2Length) - 2 * v1Length * v2Length * (Sin(v1φ1) * Sin(v2φ1) * Cos(v1φ2a - v2φ2a) + Cos(v1φ1) * Cos(v2φ1)));

        public static double[] Direction(double v1x1, double v1x2, double v1x3, double v2x1, double v2x2, double v2x3)
            => new[] { v2x1 - v1x1, v2x2 - v1x2, v2x3 - v1x3 };

        /// <summary> Calculate dot product or scalar product. </summary>
        public static double DotProduct(double v1x1, double v1x2, double v1x3, double v2x1, double v2x2, double v2x3) 
            => v1x1 * v2x1 + v1x2 * v2x2 + v1x3 * v2x3;

        /// <summary> Calculate cross product or vector product or directed area product for three dimensional euclidean space. </summary>
        public static double[] CrossProduct(double v1x1, double v1x2, double v1x3, double v2x1, double v2x2, double v2x3)
            => new[] { v1x2 * v2x3 - v1x3 * v2x2, v1x3 * v2x1 - v1x1 * v2x3, v1x1 * v2x2 - v1x2 * v2x1 };

        public static double SumLength(double v1Length, double v2Length, double v1φ1, double v2φ1, double v1φ2a, double v2φ2a)
            => throw new NotImplementedException("todo");
        
        public static double SumΦ1(double v1Length, double v2Length, double v1φ1, double v2φ1, double v1φ2a, double v2φ2a)
            => throw new NotImplementedException("todo");

        public static double SumΦ2a(double v1Length, double v2Length, double v1φ1, double v2φ1, double v1φ2a, double v2φ2a)
            => throw new NotImplementedException("todo");        
    }
}

namespace Wayout.Mathematics.Geometry
{
    using System;
    using System.Collections.Generic;
    using static System.Math;

    /// <summary>
    /// Euclidean or geometric vector.
    /// </summary>
    public static class GeometricVectorN
    {
        public static double Length(double x1, double x2) => PythagorasTheorem.Hypotenuse(x1,x2);

        public static double[] Normalized(double x1, double x2)
        {
            var length =  PythagorasTheorem.Hypotenuse(x1,x2);
            return new[] {x1 / length, x2 / length};
        }

        public static double DotProduct(IList<double> v1Coords, IList<double> v2Coords)
        {
            double product = 0;
            for (int i = 0; i < v1Coords.Count; i++)
                product += v1Coords[i] * v2Coords[i];

            return product;
        }
    }
}

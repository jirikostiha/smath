using System;
using System.Numerics;

namespace SMath.Geometry2D;

/// <summary>
/// Ellipse.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Ellipse">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Ellipse.html">Wolfram Mathworld</a>
/// </remarks>
public static class Ellipse
{
    /// <summary>
    /// Eccentricity of an ellipse.
    /// </summary>
    public static class Eccentricity
    {
        public static N FromRadius<N>(N majorRadius, N minorRadius)
            where N : IRootFunctions<N>
            => N.Sqrt(N.One - (minorRadius * minorRadius) / (majorRadius * majorRadius));
    }

    /// <summary>
    /// Linear eccentricity of an ellipse.
    /// </summary>
    public static class LinearEccentricity
    {
        public static N FromRadius<N>(N majorRadius, N minorRadius)
            where N : IRootFunctions<N>
            => N.Sqrt(majorRadius * majorRadius - minorRadius * minorRadius);
    }

    /// <summary>
    /// Parameter of an ellipse.
    /// </summary>
    public static class Parameter
    {
        public static N FromRadius<N>(N radius1, N radius2)
            where N : INumberBase<N>
            => radius2 * radius2 / radius1;
    }

    /// <summary>
    /// Semi-Latus Rectum of an ellipse.
    /// </summary>
    public static class SemiLatusRectum
    {
        /// <summary>
        /// Length of semi-latus rectum of an ellipse.
        /// </summary>
        public static class Length
        {
            public static N FromRadius<N>(N majorRadius, N minorRadius)
                where N : IDivisionOperators<N, N, N>, IMultiplyOperators<N, N, N>
                => minorRadius * minorRadius / majorRadius;
        }
    }

    /// <summary>
    /// Perimeter or curve or outline of an ellipse.
    /// </summary>
    public static class Perimeter
    {
        /// <summary>
        /// Circumference of an ellipse.
        /// </summary>
        /// <remarks>
        /// The circumference of an ellipse has no closed form, this is Ramanujan's second
        /// approximation pi * (a+b) * (1 + 3*h / (10 + sqrt(4 - 3*h))), where h is
        /// ((a-b)/(a+b))^2. It is exact for a circle and stays below a tenth of a percent
        /// of relative error even for very flat ellipses.
        /// <a href="https://en.wikipedia.org/wiki/Ellipse#Circumference">Wikipedia</a>
        /// </remarks>
        public static class Length
        {
            public static N FromRadius<N>(N radius1, N radius2)
                where N : IRootFunctions<N>
            {
                var sum = radius1 + radius2;
                if (N.IsZero(sum))
                    return N.Zero;

                var three = N.CreateChecked(3);
                var difference = radius1 - radius2;
                var h = difference * difference / (sum * sum);

                return N.Pi * sum * (N.One
                    + three * h / (N.CreateChecked(10) + N.Sqrt(N.CreateChecked(4) - three * h)));
            }
        }
    }

    /// <summary>
    /// Enclosed plane region of an ellipse.
    /// </summary>
    public static class Region
    {
        /// <summary>
        /// Enclosed plane region area of an ellipse.
        /// </summary>
        public static class Area
        {
            public static N FromRadius<N>(N majorRadius, N minorRadius)
                where N : IFloatingPoint<N>
                => N.Pi * majorRadius * minorRadius;
        }
    }
}

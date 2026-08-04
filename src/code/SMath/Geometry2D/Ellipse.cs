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
        /// approximation pi * (3*(a+b) - sqrt((3a+b)*(a+3b))). It is exact for a circle and
        /// stays below a tenth of a percent of relative error even for very flat ellipses.
        /// <a href="https://en.wikipedia.org/wiki/Ellipse#Circumference">Wikipedia</a>
        /// </remarks>
        public static class Length
        {
            public static N FromRadius<N>(N radius1, N radius2)
                where N : IRootFunctions<N>
            {
                var three = N.CreateChecked(3);
                return N.Pi * (three * (radius1 + radius2)
                    - N.Sqrt((three * radius1 + radius2) * (radius1 + three * radius2)));
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

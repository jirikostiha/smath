using System;
using System.Numerics;

namespace SMath.Geometry2D
{
    /// <summary>
    /// Ellipse.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Ellipse">wikipedia</a>
    /// <a href="https://mathworld.wolfram.com/Ellipse.html">mathworld</a>
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
                => N.Sqrt(N.One - (majorRadius * majorRadius / minorRadius * minorRadius));
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
                    where N : IDivisionOperators<N, N, N>
                    => minorRadius / majorRadius;
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
            /// <a href="https://en.wikipedia.org/wiki/Ellipse#Circumference">wikipedia</a>
            /// </remarks>
            public static class Length
            {
                public static N FromRadius<N>(N radius1, N radius2)
                    where N : IRootFunctions<N>
                    => N.Pi * (N.CreateChecked(1.5) * (radius1 + radius2) - N.Sqrt(radius1 * radius2));
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
}

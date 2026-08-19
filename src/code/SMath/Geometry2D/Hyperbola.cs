using System.Numerics;

namespace SMath.Geometry2D;

/// <summary>
/// Hyperbola in the canonical position, centered in the origin with its transverse axis
/// aligned with the x axis. It is fully determined by the major (transverse) semi-axis
/// and the minor (conjugate) semi-axis.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Hyperbola">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Hyperbola.html">Wolfram Mathworld</a>
/// </remarks>
public static class Hyperbola
{
    public static string SymmetryGroup => "D2";

    public static string PlainTextEquation => "x^2/a^2 - y^2/b^2 = 1";

    /// <summary>
    /// Eccentricity of a hyperbola. It is always greater than one.
    /// </summary>
    public static class Eccentricity
    {
        public static N FromRadius<N>(N majorRadius, N minorRadius)
            where N : IRootFunctions<N>
            => N.Sqrt(N.One + (minorRadius * minorRadius) / (majorRadius * majorRadius));
    }

    /// <summary>
    /// Linear eccentricity of a hyperbola, the distance between the center and a focus.
    /// </summary>
    public static class LinearEccentricity
    {
        public static N FromRadius<N>(N majorRadius, N minorRadius)
            where N : IRootFunctions<N>
            => PT.Hypotenuse(majorRadius, minorRadius);
    }

    /// <summary>
    /// Parameter of a hyperbola.
    /// </summary>
    public static class Parameter
    {
        public static N FromRadius<N>(N radius1, N radius2)
            where N : INumberBase<N>
            => radius2 * radius2 / radius1;
    }

    /// <summary>
    /// Semi-latus rectum of a hyperbola.
    /// </summary>
    public static class SemiLatusRectum
    {
        /// <summary>
        /// Length of the semi-latus rectum of a hyperbola, the distance between a focus and the
        /// hyperbola measured along the line perpendicular to the transverse axis.
        /// </summary>
        public static class Length
        {
            public static N FromRadius<N>(N majorRadius, N minorRadius)
                where N : IDivisionOperators<N, N, N>, IMultiplyOperators<N, N, N>
                => minorRadius * minorRadius / majorRadius;
        }
    }

    /// <summary>
    /// Vertices of a hyperbola, the two points where it meets its transverse axis.
    /// </summary>
    public static class Vertex
    {
        public static ((N X, N Y) Point1, (N X, N Y) Point2) FromRadius<N>(N majorRadius)
            where N : INumberBase<N>
            => ((majorRadius, N.Zero), (-majorRadius, N.Zero));
    }

    /// <summary>
    /// Foci of a hyperbola. The two points on the transverse axis whose distances from every
    /// point of the hyperbola differ by twice the major radius.
    /// </summary>
    public static class Focus
    {
        public static ((N X, N Y) Point1, (N X, N Y) Point2) FromRadius<N>(N majorRadius, N minorRadius)
            where N : IRootFunctions<N>
        {
            var linearEccentricity = LinearEccentricity.FromRadius(majorRadius, minorRadius);
            return ((linearEccentricity, N.Zero), (-linearEccentricity, N.Zero));
        }
    }

    /// <summary>
    /// Directrices of a hyperbola in general form. The two lines perpendicular to the transverse
    /// axis at the distance <c>a^2/c</c> from the center.
    /// </summary>
    public static class Directrix
    {
        public static ((N A, N B, N C) Line1, (N A, N B, N C) Line2) FromRadius<N>(N majorRadius, N minorRadius)
            where N : IRootFunctions<N>
        {
            var distance = majorRadius * majorRadius / LinearEccentricity.FromRadius(majorRadius, minorRadius);
            return ((N.One, N.Zero, -distance), (N.One, N.Zero, distance));
        }
    }

    /// <summary>
    /// Asymptotes of a hyperbola in general form. The two lines <c>y = +-(b/a)*x</c> the branches
    /// of the hyperbola approach without ever touching them.
    /// </summary>
    public static class Asymptote
    {
        public static ((N A, N B, N C) Line1, (N A, N B, N C) Line2) FromRadius<N>(N majorRadius, N minorRadius)
            where N : INumberBase<N>
            => ((minorRadius, -majorRadius, N.Zero), (minorRadius, majorRadius, N.Zero));
    }

    /// <summary>
    /// Point on a hyperbola.
    /// </summary>
    public static class Point
    {
        /// <summary>
        /// y coordinate of a point on the upper half of a hyperbola from its x coordinate.
        /// The lower half is its negation.
        /// </summary>
        public static N YFromX<N>(N majorRadius, N minorRadius, N x)
            where N : IRootFunctions<N>
            => minorRadius * N.Sqrt((x * x) / (majorRadius * majorRadius) - N.One);

        /// <summary>
        /// Point on the upper half of a hyperbola from its x coordinate.
        /// </summary>
        public static (N X, N Y) FromX<N>(N majorRadius, N minorRadius, N x)
            where N : IRootFunctions<N>
            => (x, YFromX(majorRadius, minorRadius, x));
    }
}

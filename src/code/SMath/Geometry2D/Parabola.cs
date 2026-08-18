using System.Numerics;

namespace SMath.Geometry2D;

/// <summary>
/// Parabola in the canonical position, with its vertex in the origin and its axis of symmetry
/// aligned with the y axis, opening towards the +y direction. It is fully determined by the
/// focal length, the distance between the vertex and the focus.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Parabola">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Parabola.html">Wolfram Mathworld</a>
/// </remarks>
public static class Parabola
{
    public static string SymmetryGroup => "D1";

    public static string PlainTextEquation => "y = x^2 / (4*f)";

    /// <summary>
    /// Focus of a parabola. The point on the axis of symmetry equidistant with the directrix
    /// from every point of the parabola.
    /// </summary>
    public static class Focus
    {
        /// <summary>
        /// Focus of a parabola determined by its focal length.
        /// </summary>
        public static (N X, N Y) FromFocalLength<N>(N focalLength)
            where N : INumberBase<N>
            => (N.Zero, focalLength);
    }

    /// <summary>
    /// Directrix of a parabola in general form. The line perpendicular to the axis of symmetry
    /// equidistant with the focus from every point of the parabola.
    /// </summary>
    public static class Directrix
    {
        /// <summary>
        /// Directrix of a parabola in general form determined by its focal length. It is the
        /// horizontal line y = -f.
        /// </summary>
        public static (N A, N B, N C) FromFocalLength<N>(N focalLength)
            where N : INumberBase<N>
            => (N.Zero, N.One, focalLength);
    }

    /// <summary>
    /// Semi-latus rectum of a parabola.
    /// </summary>
    public static class SemiLatusRectum
    {
        /// <summary>
        /// Length of the semi-latus rectum of a parabola, the distance between the focus and the
        /// parabola measured along the line parallel to the directrix. It equals twice the focal length.
        /// </summary>
        public static class Length
        {
            public static N FromFocalLength<N>(N focalLength)
                where N : INumberBase<N>
                => N.CreateChecked(2) * focalLength;
        }
    }

    /// <summary>
    /// Point on a parabola.
    /// </summary>
    public static class Point
    {
        /// <summary>
        /// y coordinate of a point on a parabola from its x coordinate.
        /// </summary>
        public static N YFromX<N>(N focalLength, N x)
            where N : INumberBase<N>
            => x * x / (N.CreateChecked(4) * focalLength);

        /// <summary>
        /// Point on a parabola from its x coordinate.
        /// </summary>
        public static (N X, N Y) FromX<N>(N focalLength, N x)
            where N : INumberBase<N>
            => (x, YFromX(focalLength, x));
    }

    /// <summary>
    /// Tangent line of a parabola in general form.
    /// </summary>
    public static class TangentLine
    {
        /// <summary>
        /// Tangent line of a parabola in general form at the point with the given x coordinate.
        /// </summary>
        public static (N A, N B, N C) FromX<N>(N focalLength, N x)
            where N : INumberBase<N>
        {
            var slope = Slope.FromX(focalLength, x);
            return (-slope, N.One, slope * x - Point.YFromX(focalLength, x));
        }

        /// <summary>
        /// Slope of the tangent line of a parabola, the first derivative of the parabola.
        /// </summary>
        public static class Slope
        {
            public static N FromX<N>(N focalLength, N x)
                where N : INumberBase<N>
                => x / (N.CreateChecked(2) * focalLength);
        }
    }

    /// <summary>
    /// Normal line of a parabola in general form.
    /// </summary>
    public static class NormalLine
    {
        /// <summary>
        /// Normal line of a parabola in general form at the point with the given x coordinate.
        /// At the vertex the normal line is the vertical axis of symmetry x = 0.
        /// </summary>
        public static (N A, N B, N C) FromX<N>(N focalLength, N x)
            where N : INumberBase<N>
        {
            var slope = Slope.FromX(focalLength, x);
            return N.IsFinite(slope)
                ? (-slope, N.One, slope * x - Point.YFromX(focalLength, x))
                : (N.One, N.Zero, -x);
        }

        /// <summary>
        /// Slope of the normal line of a parabola, the negative reciprocal of the tangent slope.
        /// </summary>
        public static class Slope
        {
            public static N FromX<N>(N focalLength, N x)
                where N : INumberBase<N>
                => -N.One / TangentLine.Slope.FromX(focalLength, x);
        }
    }
}

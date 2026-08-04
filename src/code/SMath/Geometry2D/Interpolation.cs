using System.Numerics;
using System.Runtime.CompilerServices;

namespace SMath.Geometry2D;

/// <summary>
/// Interpolation and easing of scalars and points in two dimensions.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Interpolation">Wikipedia</a>
/// </remarks>
public static class Interpolation
{
    /// <summary>
    /// Linear interpolation.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Linear_interpolation">Wikipedia</a>
    /// </remarks>
    public static class Linear
    {
        /// <summary>
        /// Value at a parameter between two values. Parameter 0 gives the first, 1 the second value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static N Eval<N>(N from, N to, N t)
            where N : INumberBase<N>
            => from + ((to - from) * t);

        /// <summary>
        /// Point at a parameter between two points. Parameter 0 gives the first, 1 the second point.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (N X, N Y) Point<N>((N X, N Y) from, (N X, N Y) to, N t)
            where N : INumberBase<N>
            => (from.X + ((to.X - from.X) * t),
                from.Y + ((to.Y - from.Y) * t));

        /// <summary>
        /// Parameter of a value between two values. It is the inverse of <see cref="Eval{N}"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static N Parameter<N>(N from, N to, N value)
            where N : INumberBase<N>
            => (value - from) / (to - from);

        /// <summary>
        /// Remap a value from one range to another one.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static N Remap<N>(N fromStart, N fromEnd, N toStart, N toEnd, N value)
            where N : INumberBase<N>
            => toStart + ((toEnd - toStart) * ((value - fromStart) / (fromEnd - fromStart)));
    }

    /// <summary>
    /// Bilinear interpolation over the four corners of a unit square.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Bilinear_interpolation">Wikipedia</a>
    /// </remarks>
    public static class Bilinear
    {
        /// <summary>
        /// Value at a point of a unit square determined by the values in its corners.
        /// </summary>
        /// <param name="v11"> Value at the corner (0,0). </param>
        /// <param name="v21"> Value at the corner (1,0). </param>
        /// <param name="v12"> Value at the corner (0,1). </param>
        /// <param name="v22"> Value at the corner (1,1). </param>
        /// <param name="tx"> Parameter in x direction. </param>
        /// <param name="ty"> Parameter in y direction. </param>
        public static N Eval<N>(N v11, N v21, N v12, N v22, N tx, N ty)
            where N : INumberBase<N>
        {
            var bottom = v11 + ((v21 - v11) * tx);
            var top = v12 + ((v22 - v12) * tx);

            return bottom + ((top - bottom) * ty);
        }
    }

    /// <summary>
    /// Spherical linear interpolation of an angle.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Slerp">Wikipedia</a>
    /// </remarks>
    public static class Angular
    {
        /// <summary>
        /// Angle at a parameter between two angles taking the shorter way around.
        /// </summary>
        public static N Eval<N>(N from, N to, N t)
            where N : IFloatingPointIeee754<N>
            => from + (Difference(from, to) * t);

        /// <summary>
        /// The shortest signed difference of two angles. It is in range (-pi, pi].
        /// </summary>
        public static N Difference<N>(N from, N to)
            where N : IFloatingPointIeee754<N>
        {
            var twoPi = N.Tau;
            var difference = (to - from) % twoPi;

            if (difference > N.Pi)
                difference -= twoPi;
            else if (difference <= -N.Pi)
                difference += twoPi;

            return difference;
        }

        /// <summary>
        /// Wrap an angle into range [0, 2*pi).
        /// </summary>
        public static N Wrapped<N>(N angle)
            where N : IFloatingPointIeee754<N>
        {
            var rest = angle % N.Tau;
            return rest < N.Zero ? rest + N.Tau : rest;
        }

        /// <summary>
        /// Rotate an angle towards another one by at most a given step.
        /// </summary>
        public static N Towards<N>(N from, N to, N maxStep)
            where N : IFloatingPointIeee754<N>
        {
            var difference = Difference(from, to);

            return N.Abs(difference) <= maxStep
                ? to
                : from + (N.CopySign(maxStep, difference));
        }
    }

    /// <summary>
    /// Easing of the interpolation parameter.
    /// </summary>
    /// <remarks>
    /// Every function maps range [0,1] onto range [0,1] keeping both ends.
    /// <a href="https://en.wikipedia.org/wiki/Smoothstep">Wikipedia</a>
    /// </remarks>
    public static class Easing
    {
        /// <summary> Clamp a parameter into range [0,1]. </summary>
        public static N Clamped<N>(N t)
            where N : INumber<N>
            => N.Clamp(t, N.Zero, N.One);

        /// <summary> Cubic Hermite easing 3t^2 - 2t^3 with a zero slope at both ends. </summary>
        public static N SmoothStep<N>(N t)
            where N : INumber<N>
        {
            t = N.Clamp(t, N.Zero, N.One);
            return t * t * (N.CreateTruncating(3) - (N.CreateTruncating(2) * t));
        }

        /// <summary> Quintic easing 6t^5 - 15t^4 + 10t^3 with a zero slope and curvature at both ends. </summary>
        public static N SmootherStep<N>(N t)
            where N : INumber<N>
        {
            t = N.Clamp(t, N.Zero, N.One);
            return t * t * t * ((t * ((t * N.CreateTruncating(6)) - N.CreateTruncating(15))) + N.CreateTruncating(10));
        }

        /// <summary> Quadratic easing t^2, slow at the start. </summary>
        public static N QuadraticIn<N>(N t)
            where N : INumber<N>
            => t * t;

        /// <summary> Quadratic easing 1-(1-t)^2, slow at the end. </summary>
        public static N QuadraticOut<N>(N t)
            where N : INumber<N>
        {
            var inverse = N.One - t;
            return N.One - (inverse * inverse);
        }

        /// <summary> Sinusoidal easing, slow at both ends. </summary>
        public static N SineInOut<N>(N t)
            where N : ITrigonometricFunctions<N>
            => (N.One - N.Cos(N.Pi * t)) / N.CreateTruncating(2);

        /// <summary>
        /// Frame rate independent exponential approach of a target.
        /// </summary>
        /// <remarks>
        /// It is the correct replacement of the naive <c>value += (target-value) * rate</c> smoothing
        /// which depends on the length of a frame.
        /// </remarks>
        /// <param name="rate"> Fraction of the remaining difference closed in one time unit, it is in range (0,1). </param>
        /// <param name="deltaTime"> Elapsed time. </param>
        public static N ExponentialDecay<N>(N rate, N deltaTime)
            where N : IPowerFunctions<N>
            => N.One - N.Pow(N.One - rate, deltaTime);
    }

    /// <summary>
    /// Cubic Bezier and Catmull-Rom curves in two dimensions.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/B%C3%A9zier_curve">Wikipedia</a>
    /// </remarks>
    public static class Curve
    {
        /// <summary>
        /// Point of a quadratic Bezier curve at a parameter.
        /// </summary>
        public static (N X, N Y) QuadraticBezier<N>((N X, N Y) point1, (N X, N Y) controlPoint, (N X, N Y) point2, N t)
            where N : INumberBase<N>
        {
            var inverse = N.One - t;
            var w1 = inverse * inverse;
            var w2 = N.CreateTruncating(2) * inverse * t;
            var w3 = t * t;

            return ((w1 * point1.X) + (w2 * controlPoint.X) + (w3 * point2.X),
                    (w1 * point1.Y) + (w2 * controlPoint.Y) + (w3 * point2.Y));
        }

        /// <summary>
        /// Point of a cubic Bezier curve at a parameter.
        /// </summary>
        public static (N X, N Y) CubicBezier<N>((N X, N Y) point1, (N X, N Y) controlPoint1,
            (N X, N Y) controlPoint2, (N X, N Y) point2, N t)
            where N : INumberBase<N>
        {
            var inverse = N.One - t;
            var three = N.CreateTruncating(3);
            var w1 = inverse * inverse * inverse;
            var w2 = three * inverse * inverse * t;
            var w3 = three * inverse * t * t;
            var w4 = t * t * t;

            return ((w1 * point1.X) + (w2 * controlPoint1.X) + (w3 * controlPoint2.X) + (w4 * point2.X),
                    (w1 * point1.Y) + (w2 * controlPoint1.Y) + (w3 * controlPoint2.Y) + (w4 * point2.Y));
        }

        /// <summary>
        /// Point of a Catmull-Rom spline segment at a parameter.
        /// The curve passes through the two inner points.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Centripetal_Catmull%E2%80%93Rom_spline">Wikipedia</a>
        /// </remarks>
        public static (N X, N Y) CatmullRom<N>((N X, N Y) point0, (N X, N Y) point1,
            (N X, N Y) point2, (N X, N Y) point3, N t)
            where N : INumberBase<N>
        {
            var two = N.CreateTruncating(2);
            var three = N.CreateTruncating(3);
            var four = N.CreateTruncating(4);
            var five = N.CreateTruncating(5);
            var half = N.One / two;

            var t2 = t * t;
            var t3 = t2 * t;

            var w0 = -t3 + (two * t2) - t;
            var w1 = (three * t3) - (five * t2) + two;
            var w2 = (-three * t3) + (four * t2) + t;
            var w3 = t3 - t2;

            return (half * ((w0 * point0.X) + (w1 * point1.X) + (w2 * point2.X) + (w3 * point3.X)),
                    half * ((w0 * point0.Y) + (w1 * point1.Y) + (w2 * point2.Y) + (w3 * point3.Y)));
        }
    }
}

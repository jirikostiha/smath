using System.Numerics;

namespace SMath.Geometry2D
{
    /// <summary>
    /// Line in two dimensional space.
    /// </summary>
    /// <remarks>
    /// Line is defined in general form by default.
    /// <a href="https://en.wikipedia.org/wiki/Line_(geometry)">wikipedia</a>
    /// <a href="https://www.123calculus.com/en/line-calculator-page-7-60-100.html">calculator 1</a>
    /// </remarks>
    public static class Line
    {
        /// <summary>
        /// X axis line in general form.
        /// </summary>
        public static (N A, N B, N C) XAxis<N>()
            where N : INumberBase<N>
            => (N.Zero, N.One, N.Zero);

        /// <summary>
        /// Y axis line in general form.
        /// </summary>
        public static (N A, N B, N C) YAxis<N>()
            where N : INumberBase<N>
            => (-N.One, N.Zero, N.Zero);

        public static string PlainTextEquation
            => "a*x + b*y + c = 0";

        /// <summary>
        /// Line in general form from two points in cartesian coordinates.
        /// </summary>
        public static (N A, N B, N C) FromTwoPoints<N>((N X, N Y) point1, (N X, N Y) point2)
            where N : IRootFunctions<N>
        {
            // direction vector = P2 - P1
            var direction = GeometricVector2.Direction(point1, point2);
            // normal vector
            var normal = GeometricVector2.Normal1(direction);
            // calculate c constant
            var c = -normal.X1 * point1.X - normal.X2 * point1.Y;

            return (normal.X1, normal.X2, c);
        }

        public static class Slope
        {
            public static N Get<N>((N A, N B, N C) line)
                where N : IUnaryNegationOperators<N, N>, IDivisionOperators<N, N, N>
                => -line.A / line.B;
        }

        /// <summary>
        /// Normal line to a given line.
        /// </summary>
        public static class NormalLine
        {
            public static class Slope
            {
                public static N Get<N>(N a, N b)
                    where N : IDivisionOperators<N, N, N>
                    => b / a;

                public static N Get<N>((N A, N B, N C) line)
                    where N : IDivisionOperators<N, N, N>
                    => line.B / line.A;
            }

            public static (N A, N B, N C) Get<N>((N A, N B, N C) line)
                where N : IUnaryNegationOperators<N, N>
                => (line.B, -line.A, line.C);
        }

        /// <summary>
        /// Ray.
        /// Determined by point and angle.
        /// </summary>
        public static class Ray
        {
            public static class Points
            {
                public static IEnumerable<(N X, N Y)> Get<N>(N angle, N step, int count)
                    where N : ITrigonometricFunctions<N>
                {
                    for (int i = 1; i <= count; i++)
                        yield return (
                            Circle.Perimeter.Point.XFromAngle(N.CreateChecked(i) * step, angle),
                            Circle.Perimeter.Point.YFromAngle(N.CreateChecked(i) * step, angle));
                }
            }
        }

        /// <summary>
        /// Line Segment
        /// Determined by two points.
        /// </summary>
        public static class Segment
        {
            public static class Length
            {
                public static N FromTwoPints<N>((N X, N Y) p1, (N X, N Y) p2)
                    where N : IRootFunctions<N>
                    => PT.Hypotenuse(p2.X - p1.X, p2.Y - p1.Y);
            }

            public static class Slope
            {
                /// <summary>
                /// Get slope value from two points in 2D.
                /// </summary>
                /// <remarks>
                /// <a href="https://en.wikipedia.org/wiki/Slope">wikipedia</a>
                /// </remarks>
                public static N Get<N>((N X, N Y) point1, (N X, N Y) point2)
                    where N : ISubtractionOperators<N, N, N>, IDivisionOperators<N, N, N>
                    => (point2.Y - point1.Y) / (point2.X - point1.X);
            }

            public static class Points
            {
                public static IEnumerable<(N X, N Y)> Get<N>((N X, N Y) point1, (N X, N Y) point2, int count)
                    where N : INumberBase<N>
                {
                    var xstep = (point2.X - point1.X) / N.CreateChecked(count + 1);
                    var ystep = (point2.Y - point1.Y) / N.CreateChecked(count + 1);
                    for (int i = 1; i <= count; i++)
                        yield return (
                            point1.X + N.CreateChecked(i) * xstep,
                            point1.Y + N.CreateChecked(i) * ystep);
                }
            }
        }
    }
}

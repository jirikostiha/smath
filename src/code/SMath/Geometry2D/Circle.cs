﻿using System.Numerics;

namespace SMath.Geometry2D;

/// <summary>
/// Circle.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Circle">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Circle.html">Wolfram Mathworld</a>
/// </remarks>
public static class Circle
{
    public static string SymmetryGroup => "O(2)";

    public static string PlainTextEquation => "(x - a)^2 + (y - b)^2 = r^2";

    /// <summary>
    /// Radius of a circle.
    /// </summary>
    public static class Radius
    {
        public static N FromCircumference<N>(N circumference)
            where N : IFloatingPointConstants<N>
            => circumference / (N.CreateChecked(2) * N.Pi);

        public static N FromArea<N>(N area)
            where N : IRootFunctions<N>
            => N.Sqrt(area / N.Pi);
    }

    /// <summary>
    /// Perimeter or curve or outline of a circle.
    /// </summary>
    public static class Perimeter
    {
        /// <summary>
        /// Circumference of a circle.
        /// </summary>
        public static class Length
        {
            public static N FromRadius<N>(N radius)
                where N : IFloatingPointConstants<N>
                => N.CreateTruncating(2) * N.Pi * radius;
        }

        /// <summary>
        /// Point on the perimeter of a circle.
        /// </summary>
        public static class Point
        {
            public static N XFromAngle<N>(N radius, N angle)
                where N : ITrigonometricFunctions<N>
                => radius * N.Cos(angle);

            public static N YFromAngle<N>(N radius, N angle)
                where N : ITrigonometricFunctions<N>
                => radius * N.Sin(angle);

            public static (N X, N Y) FromAngle<N>(N radius, N angle)
                where N : ITrigonometricFunctions<N>
                => (XFromAngle(radius, angle), YFromAngle(radius, angle));
        }

        /// <summary>
        /// Points on the perimeter of a circle.
        /// </summary>
        public static class Points
        {
            /// <summary>
            /// Enumerating points on circle from the +x axis towards the +y axis and beyond.
            /// </summary>
            public static IEnumerable<(N X, N Y)> FromRadius<N>(N radius, int count)
                where N : IFloatingPoint<N>, ITrigonometricFunctions<N>
            {
                N deltaAngle = N.CreateChecked(2) * N.Pi / N.CreateChecked(count);
                for (int i = 0; i < count; i++)
                {
                    N angle = N.CreateTruncating(i) * deltaAngle;
                    yield return Point.FromAngle(radius, angle);
                }
            }
        }

        /// <summary>
        /// Determines if a point is a part of circle perimeter.
        /// </summary>
        /// <remarks>
        /// <a href="https://mathworld.wolfram.com/Circle-LineIntersection.html">mathword</a>
        /// </remarks>
        public static class Includes
        {
            public static bool Point<N>(N radius, (N X, N Y) point)
                where N : IRootFunctions<N>
                => PT.Hypotenuse(point.X, point.Y) == radius;

            public static bool Point<N>((N X, N Y) center, N radius, (N X, N Y) point)
                where N : IRootFunctions<N>
                => PT.Hypotenuse(point.X - center.X, point.Y - center.Y) == radius;
        }
    }

    /// <summary>
    /// Plane region enclosed by a circle.
    /// </summary>
    public static class Region
    {
        /// <summary>
        /// Area of a plane region enclosed by circle.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Area_of_a_circle">Wikipedia</a>
        /// </remarks>
        public static class Area
        {
            public static N FromRadius<N>(N radius)
                where N : IFloatingPoint<N>
                => N.Pi * radius * radius;
        }

        public static class Includes
        {
            public static bool Point<N>(N radius, (N X, N Y) point)
                where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                => PT.Hypotenuse(point.X, point.Y) <= radius;

            public static bool Point<N>((N X, N Y) center, N radius, (N X, N Y) point)
                where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
                => PT.Hypotenuse(point.X - center.X, point.Y - center.Y) <= radius;
        }
    }

    /// <summary>
    /// Tangent line of a circle.
    /// </summary>
    public static class TangentLine
    {
        /// <summary>
        /// Get tangent line of a circle determined by center in origin and angle.
        /// </summary>
        public static (N A, N B, N C) FromAngle<N>(N radius, N angle)
            where N : ITrigonometricFunctions<N>
            => (
                Perimeter.Point.XFromAngle(radius, angle),
                Perimeter.Point.YFromAngle(radius, angle),
                -(radius * radius));
    }

    /// <summary>
    /// Common point(s) of a circle and it's tangent line.
    /// </summary>
    public static class TangentPoint
    {
        public static ((N X, N Y) Point1, (N X, N Y) Point2)? FromPoint<N>(N radius, (N X, N Y) point)
            where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
        {
            // distance from center to outer point
            var d = PT.Hypotenuse(point.X, point.Y);

            if (d < radius || radius <= N.Zero)
                return default;

            if (radius == d)
                return (point, point);

            var a = radius * radius / d;
            var q = radius * N.Sqrt((d * d) - (radius * radius)) / d;
            var cpNormalized = GeometricVector2.Cartesian.Normalized(point);
            var cpNormal = GeometricVector2.Normal1.FromCartesian(cpNormalized);
            var vaX = cpNormalized.X * a;
            var vaY = cpNormalized.Y * a;

            return (
                (vaX + cpNormal.X * q, vaY + cpNormal.Y * q),
                (vaX - cpNormal.X * q, vaY - cpNormal.Y * q));
        }
    }

    /// <summary>
    /// Arc of a circle.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Circular_arc">Wikipedia</a>
    /// <a href="https://mathworld.wolfram.com/Arc.html">Wolfram Mathworld</a>
    /// </remarks>
    public static class Arc
    {
        /// <summary>
        /// Length of a circular arc.
        /// </summary>
        public static class Length
        {
            public static N FromAngle<N>(N radius, N angle)
                where N : IFloatingPoint<N>
                => angle * radius;
        }
    }

    /// <summary>
    /// Chord of a circle.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Chord_(geometry)">Wikipedia</a>
    /// <a href="https://mathworld.wolfram.com/Chord.html">Wolfram Mathworld</a>
    /// </remarks>
    public static class Chord
    {
        /// <summary>
        /// The length of a chord of a circle.
        /// </summary>
        public static class Length
        {
            public static N FromAngle<N>(N radius, N angle)
                where N : IFloatingPoint<N>, ITrigonometricFunctions<N>
                => radius * N.CreateTruncating(2) * N.Sin(angle / N.CreateTruncating(2));
        }

        /// <summary>
        /// Sagitta of a circle determined by chord.
        /// </summary>
        public static class Sagitta
        {
            public static N FromAngle<N>(N radius, N angle)
                where N : IFloatingPoint<N>, ITrigonometricFunctions<N>
                => radius * (N.One - N.Cos(angle / N.CreateTruncating(2)));
        }

        /// <summary>
        /// Apothem of circle determined by chord.
        /// </summary>
        public static class Apothem
        {
            public static N FromAngle<N>(N radius, N angle)
                where N : IFloatingPoint<N>, ITrigonometricFunctions<N>
                => radius * N.Cos(angle / N.CreateTruncating(2));
        }
    }

    /// <summary>
    /// Sector of a circle or circular sector.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Circular_sector">Wikipedia</a>
    /// <a href="https://mathworld.wolfram.com/CircularSector.html">Wolfram Mathworld</a>
    /// </remarks>
    public static class Sector
    {
        /// <summary>
        /// Perimeter of the circular sector.
        /// </summary>
        public static class Perimeter
        {
            /// <summary>
            /// Length of the perimeter (circumference) of the circular sector.
            /// </summary>
            public static class Length
            {
                public static N FromAngle<N>(N radius, N angle)
                    where N : IFloatingPoint<N>
                    => radius + radius + Arc.Length.FromAngle(radius, angle);

                public static N FromArcLength<N>(N radius, N arcLength)
                    where N : IFloatingPoint<N>
                    => radius + radius + arcLength;
            }
        }

        /// <summary>
        /// Plane region enclosed by a circular sector.
        /// </summary>
        public static class Region
        {
            /// <summary>
            /// The area of plane region enclosed by a circular sector.
            /// </summary>
            public static class Area
            {
                public static N FromArcAngle<N>(N radius, N arcAngle)
                    where N : IFloatingPoint<N>
                    => radius * radius * arcAngle / N.CreateTruncating(2);

                public static N FromArcLength<N>(N radius, N length)
                    where N : IFloatingPoint<N>
                    => radius * length / N.CreateTruncating(2);
            }
        }
    }

    /// <summary>
    /// Segment of a circle or circular segment.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Circular_segment">Wikipedia</a>
    /// <a href="https://mathworld.wolfram.com/CircularSegment.html">Wolfram Mathworld</a>
    /// </remarks>
    public static class Segment
    {
        /// <summary>
        /// Radius of a circular segment.
        /// </summary>
        public static class Radius
        {
            public static N FromSagittaAndChord<N>(N sagitta, N chordLength)
                where N : INumberBase<N>
                => (sagitta / N.CreateTruncating(2)) + (chordLength * chordLength / (N.CreateTruncating(8) * sagitta));
        }

        /// <summary>
        /// Sagitta of a circular segment.
        /// </summary>
        public static class Sagitta
        {
            public static N FromAngle<N>(N radius, N angle)
                where N : ITrigonometricFunctions<N>
                => N.CreateTruncating(2) * radius * N.Sin(angle / N.CreateTruncating(2));
        }

        /// <summary>
        /// Apothem of a circular segment.
        /// </summary>
        public static class Apothem
        {
            public static N FromAngle<N>(N radius, N angle)
                where N : ITrigonometricFunctions<N>
                => radius * N.Cos(angle / N.CreateTruncating(2));
        }

        /// <summary>
        /// Perimeter of a circular segment.
        /// </summary>
        public static class Perimeter
        {
            /// <summary>
            /// Length of perimeter of a circular segment.
            /// </summary>
            public static class Length
            {
                public static N FromAngle<N>(N radius, N angle)
                    where N : IFloatingPoint<N>, ITrigonometricFunctions<N>
                    => Arc.Length.FromAngle(radius, angle) + Chord.Length.FromAngle(radius, angle);
            }
        }

        /// <summary>
        /// Enclosed plane region of a circular segment.
        /// </summary>
        public static class Region
        {
            /// <summary>
            /// The area of plane region enclosed by a circular segment.
            /// </summary>
            public static class Area
            {
                public static N FromAngle<N>(N radius, N angle)
                    where N : IFloatingPoint<N>, ITrigonometricFunctions<N>
                    => (radius * radius) / N.CreateTruncating(2) * (angle - N.Sin(angle));
            }
        }
    }
}

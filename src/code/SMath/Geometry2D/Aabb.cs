using System.Numerics;
using System.Runtime.CompilerServices;

namespace SMath.Geometry2D;

/// <summary>
/// Axis aligned bounding box in two dimensions determined by its minimal and maximal corner.
/// </summary>
/// <remarks>
/// It is the broad phase primitive of virtually every 2D collision detection.
/// <a href="https://en.wikipedia.org/wiki/Minimum_bounding_box">Wikipedia</a>
/// </remarks>
public static class Aabb
{
    /// <summary> Box from a center point and a half of its size. </summary>
    public static ((N X, N Y) Min, (N X, N Y) Max) FromCenter<N>((N X, N Y) center, (N X, N Y) extents)
        where N : INumberBase<N>
        => ((center.X - extents.X, center.Y - extents.Y),
            (center.X + extents.X, center.Y + extents.Y));

    /// <summary> Box from a corner point and a size. </summary>
    public static ((N X, N Y) Min, (N X, N Y) Max) FromSize<N>((N X, N Y) min, (N X, N Y) size)
        where N : INumberBase<N>
        => (min, (min.X + size.X, min.Y + size.Y));

    /// <summary> The smallest box containing all the given points. </summary>
    public static ((N X, N Y) Min, (N X, N Y) Max) FromPoints<N>(ReadOnlySpan<(N X, N Y)> points)
        where N : INumber<N>
    {
        if (points.Length == 0)
            throw new ArgumentException("At least one point is required.", nameof(points));

        var min = points[0];
        var max = points[0];

        for (var i = 1; i < points.Length; i++)
        {
            min = (N.Min(min.X, points[i].X), N.Min(min.Y, points[i].Y));
            max = (N.Max(max.X, points[i].X), N.Max(max.Y, points[i].Y));
        }

        return (min, max);
    }

    /// <summary> Center point of a box. </summary>
    public static (N X, N Y) Center<N>(((N X, N Y) Min, (N X, N Y) Max) box)
        where N : INumberBase<N>
    {
        var two = N.CreateTruncating(2);
        return ((box.Min.X + box.Max.X) / two, (box.Min.Y + box.Max.Y) / two);
    }

    /// <summary> Size of a box. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (N X, N Y) Size<N>(((N X, N Y) Min, (N X, N Y) Max) box)
        where N : INumberBase<N>
        => (box.Max.X - box.Min.X, box.Max.Y - box.Min.Y);

    /// <summary> Area of a box. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static N Area<N>(((N X, N Y) Min, (N X, N Y) Max) box)
        where N : INumberBase<N>
        => (box.Max.X - box.Min.X) * (box.Max.Y - box.Min.Y);

    /// <summary> Perimeter of a box. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static N Perimeter<N>(((N X, N Y) Min, (N X, N Y) Max) box)
        where N : INumberBase<N>
        => N.CreateTruncating(2) * ((box.Max.X - box.Min.X) + (box.Max.Y - box.Min.Y));

    /// <summary> Determine if a box contains a point. Boundary is included. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains<N>(((N X, N Y) Min, (N X, N Y) Max) box, (N X, N Y) point)
        where N : IComparisonOperators<N, N, bool>
        => point.X >= box.Min.X && point.X <= box.Max.X
        && point.Y >= box.Min.Y && point.Y <= box.Max.Y;

    /// <summary> Determine if a box contains another box entirely. </summary>
    public static bool Contains<N>(((N X, N Y) Min, (N X, N Y) Max) box, ((N X, N Y) Min, (N X, N Y) Max) other)
        where N : IComparisonOperators<N, N, bool>
        => other.Min.X >= box.Min.X && other.Max.X <= box.Max.X
        && other.Min.Y >= box.Min.Y && other.Max.Y <= box.Max.Y;

    /// <summary> Determine if two boxes overlap. Touching boxes are overlapping. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Overlaps<N>(((N X, N Y) Min, (N X, N Y) Max) box1, ((N X, N Y) Min, (N X, N Y) Max) box2)
        where N : IComparisonOperators<N, N, bool>
        => box1.Min.X <= box2.Max.X && box1.Max.X >= box2.Min.X
        && box1.Min.Y <= box2.Max.Y && box1.Max.Y >= box2.Min.Y;

    /// <summary> The smallest box containing both boxes. </summary>
    public static ((N X, N Y) Min, (N X, N Y) Max) Union<N>(((N X, N Y) Min, (N X, N Y) Max) box1,
        ((N X, N Y) Min, (N X, N Y) Max) box2)
        where N : INumber<N>
        => ((N.Min(box1.Min.X, box2.Min.X), N.Min(box1.Min.Y, box2.Min.Y)),
            (N.Max(box1.Max.X, box2.Max.X), N.Max(box1.Max.Y, box2.Max.Y)));

    /// <summary> Common part of two boxes or null if they do not overlap. </summary>
    public static ((N X, N Y) Min, (N X, N Y) Max)? Intersection<N>(((N X, N Y) Min, (N X, N Y) Max) box1,
        ((N X, N Y) Min, (N X, N Y) Max) box2)
        where N : INumber<N>
    {
        var min = (X: N.Max(box1.Min.X, box2.Min.X), Y: N.Max(box1.Min.Y, box2.Min.Y));
        var max = (X: N.Min(box1.Max.X, box2.Max.X), Y: N.Min(box1.Max.Y, box2.Max.Y));

        return min.X <= max.X && min.Y <= max.Y ? (min, max) : null;
    }

    /// <summary> Box grown in all directions by a margin. A negative margin shrinks it. </summary>
    public static ((N X, N Y) Min, (N X, N Y) Max) Expanded<N>(((N X, N Y) Min, (N X, N Y) Max) box, N margin)
        where N : INumberBase<N>
        => ((box.Min.X - margin, box.Min.Y - margin),
            (box.Max.X + margin, box.Max.Y + margin));

    /// <summary> Box moved by an offset. </summary>
    public static ((N X, N Y) Min, (N X, N Y) Max) Translated<N>(((N X, N Y) Min, (N X, N Y) Max) box, (N X, N Y) offset)
        where N : INumberBase<N>
        => ((box.Min.X + offset.X, box.Min.Y + offset.Y),
            (box.Max.X + offset.X, box.Max.Y + offset.Y));

    /// <summary>
    /// Box grown to contain the whole sweep of a moving box. It is used for a broad phase of a fast object.
    /// </summary>
    public static ((N X, N Y) Min, (N X, N Y) Max) Swept<N>(((N X, N Y) Min, (N X, N Y) Max) box, (N X, N Y) velocity)
        where N : INumber<N>
        => Union(box, Translated(box, velocity));

    /// <summary> The closest point of a box to a point. </summary>
    public static (N X, N Y) ClosestPoint<N>(((N X, N Y) Min, (N X, N Y) Max) box, (N X, N Y) point)
        where N : INumber<N>
        => (N.Clamp(point.X, box.Min.X, box.Max.X),
            N.Clamp(point.Y, box.Min.Y, box.Max.Y));

    /// <summary> Euclidean distance of a point and a box. It is zero for a contained point. </summary>
    public static N Distance<N>(((N X, N Y) Min, (N X, N Y) Max) box, (N X, N Y) point)
        where N : INumber<N>, IRootFunctions<N>
    {
        var dx = N.Max(N.Max(box.Min.X - point.X, point.X - box.Max.X), N.Zero);
        var dy = N.Max(N.Max(box.Min.Y - point.Y, point.Y - box.Max.Y), N.Zero);

        return PT.Hypotenuse(dx, dy);
    }

    /// <summary>
    /// The shortest translation separating two overlapping boxes.
    /// </summary>
    /// <remarks>
    /// Apply it to the first box to push it out of the second one.
    /// <a href="https://en.wikipedia.org/wiki/Hyperplane_separation_theorem">Wikipedia</a>
    /// </remarks>
    /// <returns> The separating vector or null if the boxes do not overlap. </returns>
    public static (N X, N Y)? Penetration<N>(((N X, N Y) Min, (N X, N Y) Max) box1,
        ((N X, N Y) Min, (N X, N Y) Max) box2)
        where N : INumber<N>
    {
        var right = box2.Max.X - box1.Min.X;
        var left = box2.Min.X - box1.Max.X;
        var up = box2.Max.Y - box1.Min.Y;
        var down = box2.Min.Y - box1.Max.Y;

        if (right <= N.Zero || left >= N.Zero || up <= N.Zero || down >= N.Zero)
            return null;

        var dx = right < -left ? right : left;
        var dy = up < -down ? up : down;

        return N.Abs(dx) < N.Abs(dy) ? (dx, N.Zero) : (N.Zero, dy);
    }

    /// <summary>
    /// Vertices of a box ordered counterclockwise starting at the minimal corner.
    /// </summary>
    public static ((N X, N Y) V1, (N X, N Y) V2, (N X, N Y) V3, (N X, N Y) V4) Vertices<N>(
        ((N X, N Y) Min, (N X, N Y) Max) box)
        where N : INumberBase<N>
        => (box.Min,
            (box.Max.X, box.Min.Y),
            box.Max,
            (box.Min.X, box.Max.Y));

    /// <summary>
    /// Ray and box intersection by the slab method.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Slab_method">Wikipedia</a>
    /// </remarks>
    /// <param name="box"> Box. </param>
    /// <param name="origin"> Origin point of the ray. </param>
    /// <param name="direction"> Direction of the ray, it does not need to be normalized. </param>
    /// <returns>
    /// Parameters of the entry and exit point of the ray or null if the ray misses the box.
    /// A negative entry parameter means the origin lies inside of the box.
    /// </returns>
    public static (N Entry, N Exit)? RayIntersection<N>(((N X, N Y) Min, (N X, N Y) Max) box,
        (N X, N Y) origin, (N X, N Y) direction)
        where N : IFloatingPointIeee754<N>
    {
        var inverseX = N.One / direction.X;
        var inverseY = N.One / direction.Y;

        var tx1 = (box.Min.X - origin.X) * inverseX;
        var tx2 = (box.Max.X - origin.X) * inverseX;
        var ty1 = (box.Min.Y - origin.Y) * inverseY;
        var ty2 = (box.Max.Y - origin.Y) * inverseY;

        var entry = N.Max(N.Min(tx1, tx2), N.Min(ty1, ty2));
        var exit = N.Min(N.Max(tx1, tx2), N.Max(ty1, ty2));

        return exit >= N.Zero && entry <= exit ? (entry, exit) : null;
    }

    /// <summary>
    /// Time of the first contact of a moving box and a static box.
    /// </summary>
    /// <remarks>
    /// It is the swept test preventing a fast object from tunneling through a thin obstacle.
    /// </remarks>
    /// <param name="box"> Moving box. </param>
    /// <param name="velocity"> Movement of the box over the whole time step. </param>
    /// <param name="obstacle"> Static box. </param>
    /// <returns>
    /// Fraction of the time step at the moment of the contact in range [0,1]
    /// together with the normal of the touched edge, or null if the boxes do not meet.
    /// </returns>
    public static (N Time, (N X, N Y) Normal)? Sweep<N>(((N X, N Y) Min, (N X, N Y) Max) box, (N X, N Y) velocity,
        ((N X, N Y) Min, (N X, N Y) Max) obstacle)
        where N : IFloatingPointIeee754<N>
    {
        // reduce the problem to a ray against the Minkowski sum of both boxes
        var minX = obstacle.Min.X - (box.Max.X - box.Min.X);
        var minY = obstacle.Min.Y - (box.Max.Y - box.Min.Y);

        var inverseX = N.One / velocity.X;
        var inverseY = N.One / velocity.Y;

        var tx1 = (minX - box.Min.X) * inverseX;
        var tx2 = (obstacle.Max.X - box.Min.X) * inverseX;
        var ty1 = (minY - box.Min.Y) * inverseY;
        var ty2 = (obstacle.Max.Y - box.Min.Y) * inverseY;

        var entryX = N.Min(tx1, tx2);
        var entryY = N.Min(ty1, ty2);
        var entry = N.Max(entryX, entryY);
        var exit = N.Min(N.Max(tx1, tx2), N.Max(ty1, ty2));

        if (entry > exit || entry < N.Zero || entry > N.One)
            return null;

        var normal = entryX > entryY
            ? (velocity.X < N.Zero ? (N.One, N.Zero) : (-N.One, N.Zero))
            : (velocity.Y < N.Zero ? (N.Zero, N.One) : (N.Zero, -N.One));

        return (entry, normal);
    }
}

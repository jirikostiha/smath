using System.Numerics;

namespace SMath.FunctionsN;

/// <summary>
/// Euclidean distance in n dimensions.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Euclidean_distance">Wikipedia</a>
/// <a href="https://xlinux.nist.gov/dads/HTML/euclidndstnc.html">NIST</a>
/// </remarks>
public static class EuclideanDistance
{
    /// <summary>
    /// String representation of the function.
    /// </summary>
    public static string PlainTextFormula
        => "sqrt(sum(xi^2))";

    /// <summary>
    /// Euclidean distance of a point and the origin.
    /// </summary>
    public static N Eval<N>(IEnumerable<N> point)
        where N : IRootFunctions<N>
    {
        var sum = N.Zero;
        foreach (var coordinate in point)
            sum += coordinate * coordinate;

        return N.Sqrt(sum);
    }

    /// <summary>
    /// Euclidean distance of a point and the origin.
    /// </summary>
    public static N Eval<N>(ReadOnlySpan<N> point)
        where N : IRootFunctions<N>
    {
        var sum = N.Zero;
        for (int i = 0; i < point.Length; i++)
            sum += point[i] * point[i];

        return N.Sqrt(sum);
    }

    /// <summary>
    /// Euclidean distance of two points.
    /// </summary>
    public static N Eval<N>(IEnumerable<N> point1, IEnumerable<N> point2)
        where N : IRootFunctions<N>
    {
        var sum = N.Zero;

        using (var enumerator1 = point1.GetEnumerator())
        using (var enumerator2 = point2.GetEnumerator())
        {
            while (true)
            {
                var moved1 = enumerator1.MoveNext();
                var moved2 = enumerator2.MoveNext();

                // one sequence ran out sooner than the other
                if (moved1 != moved2)
                    throw new ArgumentException("Inconsistent length of sequences.");

                if (!moved1)
                    break;

                var difference = enumerator1.Current - enumerator2.Current;
                sum += difference * difference;
            }
        }

        return N.Sqrt(sum);
    }

    /// <summary>
    /// Euclidean distance of two points.
    /// </summary>
    public static N Eval<N>(ReadOnlySpan<N> point1, ReadOnlySpan<N> point2)
        where N : IRootFunctions<N>
    {
        if (point1.Length != point2.Length)
            throw new ArgumentException("Inconsistent length of sequences.");

        var sum = N.Zero;
        for (int i = 0; i < point1.Length; i++)
        {
            var difference = point1[i] - point2[i];
            sum += difference * difference;
        }

        return N.Sqrt(sum);
    }
}

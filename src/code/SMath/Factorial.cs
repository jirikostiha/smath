using System.Numerics;

namespace SMath;

/// <summary>
/// Factorial.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Factorial">Wikipedia</a>
/// <a href="https://mathworld.wolfram.com/Factorial.html">Wolfram Mathworld</a>
/// </remarks>
public static class Factorial
{
    public static string PlainTextFormula => "n! = 1 * 2 * ... * n";

    /// <summary>
    /// Factorial of a non negative number.
    /// </summary>
    /// <remarks> Factorial of zero is the empty product, one. </remarks>
    /// <exception cref="ArgumentOutOfRangeException"> Number is negative. </exception>
    public static NInt Eval<NInt>(NInt n)
        where NInt : IBinaryInteger<NInt>
    {
        if (NInt.IsNegative(n))
            throw new ArgumentOutOfRangeException(nameof(n), "Factorial is not defined for a negative number.");

        var product = NInt.One;
        for (var i = NInt.CreateChecked(2); i <= n; i++)
            product *= i;

        return product;
    }

    /// <summary>
    /// Falling factorial, n * (n - 1) * ... * (n - k + 1).
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Falling_and_rising_factorials">Wikipedia</a>
    /// </remarks>
    public static class Falling
    {
        /// <summary>
        /// Product of <paramref name="k"/> numbers descending from <paramref name="n"/>.
        /// </summary>
        /// <remarks> The empty product is one. </remarks>
        /// <exception cref="ArgumentOutOfRangeException"> Count of factors is negative. </exception>
        public static NInt Eval<NInt>(NInt n, NInt k)
            where NInt : IBinaryInteger<NInt>
        {
            if (NInt.IsNegative(k))
                throw new ArgumentOutOfRangeException(nameof(k), "Count of factors cannot be negative.");

            var product = NInt.One;
            for (var i = NInt.Zero; i < k; i++)
                product *= n - i;

            return product;
        }
    }

    /// <summary>
    /// Rising factorial, n * (n + 1) * ... * (n + k - 1).
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Falling_and_rising_factorials">Wikipedia</a>
    /// </remarks>
    public static class Rising
    {
        /// <summary>
        /// Product of <paramref name="k"/> numbers ascending from <paramref name="n"/>.
        /// </summary>
        /// <remarks> The empty product is one. </remarks>
        /// <exception cref="ArgumentOutOfRangeException"> Count of factors is negative. </exception>
        public static NInt Eval<NInt>(NInt n, NInt k)
            where NInt : IBinaryInteger<NInt>
        {
            if (NInt.IsNegative(k))
                throw new ArgumentOutOfRangeException(nameof(k), "Count of factors cannot be negative.");

            var product = NInt.One;
            for (var i = NInt.Zero; i < k; i++)
                product *= n + i;

            return product;
        }
    }
}

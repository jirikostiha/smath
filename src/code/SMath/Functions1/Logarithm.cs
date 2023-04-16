namespace Wayout.Mathematics.Functions
{
    using System;

    /// <summary>
    /// Logarithm function
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Logarithm">wikipedia</a>
    /// </remarks>
    public static class Logarithm
    {
        public static double f(double x1, int @base) => Log(x1, @base);
    }

    /// <summary>
    /// Common logarithm function
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Natural_logarithm">wikipedia</a>
    /// </remarks>
    public static class CommonLogarithm
    {
        public static double f(double x1) => Log10(x1);
    }

    /// <summary>
    /// Natural logarithm function
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Natural_logarithm">wikipedia</a>
    /// </remarks>
    public static class NaturalLogarithm
    {
        public static double f(double x1) => Log(x1);
    }
}

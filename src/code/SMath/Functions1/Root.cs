namespace Wayout.Mathematics.Functions
{
    using System;

    /// <summary>
    /// Square root function
    /// </summary>
    /// <remarks>
    /// <a href="">wikipedia</a>
    /// </remarks>
    public static class Root2
	{
        public const double DomainMin = 0;
        public const double DomainMax = double.PositiveInfinity;

        public const double ImageMin = 0;
        public const double ImageMax = double.PositiveInfinity;

        public const bool IsEven = false;
        public const bool IsOdd = false;

        public const double InterceptsX1At = 0;

        public const double MinimumX1 = 0;
        public const double MinimumX2 = 0;

        public static double f(double x1) => Sqrt(x1);

		public const string Formula = "sqrt(x1)"; // or root2
	}

    /// <summary>
    /// Cube root function
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Cube_root">wikipedia</a>
    /// </remarks>
    public static class Root3
    {
        public static double f(double x1) => Pow(x1, 1.0 / 3.0);

        //public const string Formula = "√x1";
    }

    public static class Root4
    {
        public static double f(double x1) => Pow(x1, 1.0 / 4.0);
    }

    public static class Root5
    {
        public static double f(double x1) => Pow(x1, 1.0 / 5.0);
    }

    public static class Root6
    {
        public static double f(double x1) => Pow(x1, 1.0 / 6.0);
    }

    public static class Root7
    {
        public static double f(double x1) => Pow(x1, 1.0 / 7.0);
    }

    public static class Root8
    {
        public static double f(double x1) => Pow(x1, 1.0 / 8.0);
    }
}

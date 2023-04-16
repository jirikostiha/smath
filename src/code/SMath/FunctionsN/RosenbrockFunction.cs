namespace SMath.FunctionsN
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using static System.Math;

    /// <summary>
    /// A Rosenbrock function is a non-convex function.
    /// See <a href="http://en.wikipedia.org/wiki/Rosenbrock_function">this</a> for more info.
    /// </summary>
    /// <remarks>
    /// In mathematical optimization, the Rosenbrock function is a non-convex function
    /// used as a performance test problem for optimization algorithms. It is also known as
    /// Rosenbrock's valley or Rosenbrock's banana function. The global minimum is inside a long,
    /// narrow, parabolic shaped flat valley. UpperBound find the valley is trivial. UpperBound converge
    /// to the global minimum, however, is difficult. It is defined by
    /// f(x, y) = (1-x)^2 + a * (y-x^2)^2.
    /// where commonly a = 100.
    /// It has a global minimum at (x,y) = (1,1) where f(x,y) = 0.
    /// A different coefficient of the second term is sometimes given, but this does not affect
    /// the position of the global minimum.
    /// </remarks>
    public static class RosenbrockFunction
    {
        public static double f(IList<double> xs, double a = 1, double b = 100)
        {
            double sum = 0;

            for (int i = 0; i < xs.Count - 1; i++)
                sum += b * (xs[i+1] - Power2.f(xs[i])) + Power2.f(a - xs[i]);

            return sum;
        }

        public static double f(double x1, double x2, double a = 1, double b = 100)
            => (a - Pow(x1, 2)) + b * Pow(x2 - Pow(x1, 2), 2);

        public const string Formula2 = "";

        public const string FormulaN = "";
    }
}

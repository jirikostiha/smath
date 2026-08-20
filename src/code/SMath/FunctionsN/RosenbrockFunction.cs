using System.Numerics;
using SMath.Functions1;

namespace SMath.FunctionsN;

/// <summary>
/// Rosenbrock function, also known as Rosenbrock's valley or banana function, in n dimensions.
/// </summary>
/// <remarks>
/// A non convex function used as a performance test problem for optimization algorithms.
/// The global minimum lies inside a long, narrow, parabolic shaped flat valley. Finding the
/// valley is trivial, converging to the global minimum is not.
/// The minimum is at the point whose coordinates are all equal to <c>a</c> and its value is zero.
/// <a href="https://en.wikipedia.org/wiki/Rosenbrock_function">Wikipedia</a>
/// <a href="https://www.sfu.ca/~ssurjano/rosen.html">Simon Fraser University</a>
/// </remarks>
public static class RosenbrockFunction
{
    /// <summary>
    /// String representation of the function.
    /// </summary>
    public static string PlainTextFormula
        => "sum((a - xi)^2 + b*(xi+1 - xi^2)^2)";

    /// <summary>
    /// Commonly used value of the valley position parameter.
    /// </summary>
    public static N DefaultA<N>()
        where N : INumberBase<N>
        => N.One;

    /// <summary>
    /// Commonly used value of the valley steepness parameter.
    /// </summary>
    public static N DefaultB<N>()
        where N : INumberBase<N>
        => N.CreateChecked(100);

    /// <summary>
    /// Value of the global minimum.
    /// </summary>
    public static N GlobalMinimum<N>()
        where N : INumberBase<N>
        => N.Zero;

    /// <summary>
    /// Evaluates the function value in two dimensions.
    /// </summary>
    /// <param name="x1"> First coordinate. </param>
    /// <param name="x2"> Second coordinate. </param>
    /// <param name="a"> Position of the valley. </param>
    /// <param name="b"> Steepness of the valley. </param>
    public static N Eval<N>(N x1, N x2, N a, N b)
        where N : INumberBase<N>
        => Power2.Eval(a - x1) + (b * Power2.Eval(x2 - Power2.Eval(x1)));

    /// <summary>
    /// Evaluates the function value in two dimensions with the default parameters.
    /// </summary>
    /// <param name="x1"> First coordinate. </param>
    /// <param name="x2"> Second coordinate. </param>
    public static N Eval<N>(N x1, N x2)
        where N : INumberBase<N>
        => Eval(x1, x2, DefaultA<N>(), DefaultB<N>());

    /// <summary>
    /// Evaluates the function value.
    /// </summary>
    /// <param name="point"> Coordinates of the point. </param>
    /// <param name="a"> Position of the valley. </param>
    /// <param name="b"> Steepness of the valley. </param>
    public static N Eval<N>(ReadOnlySpan<N> point, N a, N b)
        where N : INumberBase<N>
    {
        var sum = N.Zero;
        for (int i = 0; i < point.Length - 1; i++)
            sum += Eval(point[i], point[i + 1], a, b);

        return sum;
    }

    /// <summary>
    /// Evaluates the function value with the default parameters.
    /// </summary>
    /// <param name="point"> Coordinates of the point. </param>
    public static N Eval<N>(ReadOnlySpan<N> point)
        where N : INumberBase<N>
        => Eval(point, DefaultA<N>(), DefaultB<N>());

    /// <summary>
    /// Evaluates the function value.
    /// </summary>
    /// <param name="point"> Coordinates of the point. </param>
    /// <param name="a"> Position of the valley. </param>
    /// <param name="b"> Steepness of the valley. </param>
    public static N Eval<N>(IEnumerable<N> point, N a, N b)
        where N : INumberBase<N>
    {
        var sum = N.Zero;

        using (var enumerator = point.GetEnumerator())
        {
            if (!enumerator.MoveNext())
                return sum;

            var previous = enumerator.Current;
            while (enumerator.MoveNext())
            {
                sum += Eval(previous, enumerator.Current, a, b);
                previous = enumerator.Current;
            }
        }

        return sum;
    }

    /// <summary>
    /// Evaluates the function value with the default parameters.
    /// </summary>
    /// <param name="point"> Coordinates of the point. </param>
    public static N Eval<N>(IEnumerable<N> point)
        where N : INumberBase<N>
        => Eval(point, DefaultA<N>(), DefaultB<N>());
}

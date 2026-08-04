using System.Runtime.CompilerServices;

namespace SMath.Pathfinding;

/// <summary>
/// Estimate of the remaining cost from a cell to the target cell of a search.
/// </summary>
/// <remarks>
/// The estimate must never exceed the real cost, otherwise the found path is not the shortest one.
/// Implement it by a <see langword="struct"/> and pass it as a generic type argument
/// to let the runtime devirtualize and inline the calls.
/// <a href="https://en.wikipedia.org/wiki/Admissible_heuristic">Wikipedia</a>
/// </remarks>
public interface IGridHeuristic
{
    /// <summary> Estimate the cost between two cells. </summary>
    int Estimate(int fromX, int fromY, int toX, int toY);
}

/// <summary>
/// Costs of a single step over cells of a two dimensional grid.
/// </summary>
/// <remarks>
/// Costs are scaled integers to keep the searches exact and branch free.
/// The scale is chosen so that the diagonal step is the closest integer multiple of sqrt(2).
/// </remarks>
public static class GridStep
{
    /// <summary> Cost of a step in an axis direction. </summary>
    public const int StraightCost = 1000;

    /// <summary> Cost of a step in a diagonal direction, it is <see cref="StraightCost"/> * sqrt(2). </summary>
    public const int DiagonalCost = 1414;

    /// <summary> Convert a scaled cost to a real distance in cells. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static N ToDistance<N>(int cost)
        where N : System.Numerics.INumberBase<N>
        => N.CreateChecked(cost) / N.CreateChecked(StraightCost);
}

/// <summary>
/// Heuristic returning zero. It turns the A* search into the Dijkstra search.
/// </summary>
public readonly struct ZeroHeuristic : IGridHeuristic
{
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Estimate(int fromX, int fromY, int toX, int toY)
        => 0;
}

/// <summary>
/// Manhattan or taxicab heuristic. It is admissible for the 4-neighborhood only.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Taxicab_geometry">Wikipedia</a>
/// </remarks>
public readonly struct ManhattanHeuristic : IGridHeuristic
{
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Estimate(int fromX, int fromY, int toX, int toY)
        => GridStep.StraightCost * (Math.Abs(toX - fromX) + Math.Abs(toY - fromY));
}

/// <summary>
/// Chebyshev heuristic. It is admissible for the 8-neighborhood with an equally priced diagonal step.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Chebyshev_distance">Wikipedia</a>
/// </remarks>
public readonly struct ChebyshevHeuristic : IGridHeuristic
{
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Estimate(int fromX, int fromY, int toX, int toY)
        => GridStep.StraightCost * Math.Max(Math.Abs(toX - fromX), Math.Abs(toY - fromY));
}

/// <summary>
/// Octile heuristic. It is the exact distance over an empty 8-neighborhood grid
/// and the heuristic of choice for it.
/// </summary>
public readonly struct OctileHeuristic : IGridHeuristic
{
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Estimate(int fromX, int fromY, int toX, int toY)
    {
        var dx = Math.Abs(toX - fromX);
        var dy = Math.Abs(toY - fromY);

        // straight steps for the whole span plus the discount of the shorter, diagonally walked one
        return (GridStep.StraightCost * (dx + dy))
            + ((GridStep.DiagonalCost - (2 * GridStep.StraightCost)) * Math.Min(dx, dy));
    }
}

/// <summary>
/// Euclidean heuristic. It is admissible for any neighborhood but weaker than the octile one.
/// </summary>
/// <remarks>
/// <a href="https://en.wikipedia.org/wiki/Euclidean_distance">Wikipedia</a>
/// </remarks>
public readonly struct EuclideanHeuristic : IGridHeuristic
{
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Estimate(int fromX, int fromY, int toX, int toY)
    {
        var dx = toX - fromX;
        var dy = toY - fromY;

        return (int)(GridStep.StraightCost * Math.Sqrt((double)((dx * dx) + (dy * dy))));
    }
}

/// <summary>
/// Heuristic multiplied by a weight. Weight over one trades the optimality of the path for the speed of the search.
/// </summary>
/// <remarks>
/// The found path is at most weight times longer than the shortest one.
/// <a href="https://en.wikipedia.org/wiki/A*_search_algorithm">Wikipedia</a>
/// </remarks>
/// <typeparam name="THeuristic"> Underlying admissible heuristic. </typeparam>
public readonly struct WeightedHeuristic<THeuristic> : IGridHeuristic
    where THeuristic : IGridHeuristic
{
    private readonly THeuristic _heuristic;
    private readonly int _numerator;
    private readonly int _denominator;

    /// <param name="heuristic"> Underlying admissible heuristic. </param>
    /// <param name="numerator"> Numerator of the weight. </param>
    /// <param name="denominator"> Denominator of the weight. </param>
    public WeightedHeuristic(THeuristic heuristic, int numerator, int denominator)
    {
        if (numerator <= 0)
            throw new ArgumentOutOfRangeException(nameof(numerator), numerator, "Numerator must be positive.");
        if (denominator <= 0)
            throw new ArgumentOutOfRangeException(nameof(denominator), denominator, "Denominator must be positive.");

        _heuristic = heuristic;
        _numerator = numerator;
        _denominator = denominator;
    }

    /// <inheritdoc/>
    public int Estimate(int fromX, int fromY, int toX, int toY)
        => _heuristic.Estimate(fromX, fromY, toX, toY) * _numerator / _denominator;
}

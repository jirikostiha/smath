using SMath.Grid2D;

namespace SMath.Pathfinding;

/// <summary>
/// Traversal weight of cells of a two dimensional grid.
/// </summary>
/// <remarks>
/// Weight is a multiplier of the step cost, one is the plain terrain,
/// non-positive value marks an impassable cell.
/// Weights lower than one break admissibility of the heuristics.
/// Implement it by a <see langword="struct"/> and pass it as a generic type argument
/// to let the runtime devirtualize and inline the calls.
/// </remarks>
public interface IGridCost
{
    /// <summary> Traversal weight of a cell. Non-positive value means impassable. </summary>
    int Weight(int x, int y);
}

/// <summary>
/// Cost of cells of a grid where every cell is passable with the plain weight.
/// </summary>
public readonly struct OpenGridCost : IGridCost
{
    /// <inheritdoc/>
    public int Weight(int x, int y)
        => 1;
}

/// <summary>
/// Cost of cells of a grid stored in a row-major buffer.
/// </summary>
public readonly struct GridCostMap : IGridCost
{
    private readonly int[] _weights;

    public GridCostMap(int[] weights, int width)
    {
        ArgumentNullException.ThrowIfNull(weights);
        if (width <= 0)
            throw new ArgumentOutOfRangeException(nameof(width), width, "Width must be positive.");

        _weights = weights;
        Width = width;
    }

    /// <summary> Count of cells in x direction. </summary>
    public int Width { get; }

    /// <inheritdoc/>
    public int Weight(int x, int y)
        => _weights[(y * Width) + x];
}

/// <summary>
/// Cost of cells of a grid determined by passability of cells.
/// </summary>
/// <typeparam name="TPassable"> Predicate matching the passable cells. </typeparam>
public readonly struct GridPassabilityCost<TPassable> : IGridCost
    where TPassable : IGridPredicate
{
    private readonly TPassable _isPassable;

    public GridPassabilityCost(TPassable isPassable)
        => _isPassable = isPassable;

    /// <inheritdoc/>
    public int Weight(int x, int y)
        => _isPassable.Test(x, y) ? 1 : 0;
}

/// <summary>
/// Cost of cells of a grid determined by a delegate.
/// </summary>
public readonly struct GridCostFunction : IGridCost
{
    private readonly Func<int, int, int> _weight;

    public GridCostFunction(Func<int, int, int> weight)
        => _weight = weight ?? throw new ArgumentNullException(nameof(weight));

    /// <inheritdoc/>
    public int Weight(int x, int y)
        => _weight(x, y);
}

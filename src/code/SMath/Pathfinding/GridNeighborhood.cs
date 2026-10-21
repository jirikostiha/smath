using System.Runtime.CompilerServices;
using SMath.Grid2D;

namespace SMath.Pathfinding;

/// <summary>
/// Steps from a cell of a two dimensional grid to its neighbors allowed by a connectivity.
/// </summary>
internal static class GridNeighborhood
{
    /// <summary> Increment of a direction index over <see cref="GridDirection.All"/>, the axial directions are the even ones. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int DirectionIncrement(GridConnectivity connectivity)
        => connectivity == GridConnectivity.Axial ? 2 : 1;

    /// <summary>
    /// Determine if a step from a cell in a direction is allowed: the neighbor lies in the grid, it is passable
    /// and a diagonal step does not slip past an impassable cell unless the corners may be cut.
    /// The neighbor and its traversal weight are returned for the cost of the step.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryStep<TCost>(TCost cost, int x, int y, int direction, int width, int height,
        GridConnectivity connectivity, out int nx, out int ny, out int weight)
        where TCost : IGridCost
    {
        weight = 0;

        return TryNeighbor(x, y, direction, width, height, out nx, out ny)
            && IsAllowed(cost, x, y, nx, ny, direction, connectivity, out weight);
    }

    /// <summary> Determine if the neighbor of a cell in a direction lies in the grid. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryNeighbor(int x, int y, int direction, int width, int height, out int nx, out int ny)
    {
        var delta = GridDirection.Delta(direction);
        nx = x + delta.X;
        ny = y + delta.Y;

        return Grid.Contains(nx, ny, width, height);
    }

    /// <summary>
    /// Determine if a step from a cell to its neighbor in the grid is allowed by the weights and the corner rule,
    /// see <see cref="TryStep{TCost}"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAllowed<TCost>(TCost cost, int x, int y, int nx, int ny, int direction,
        GridConnectivity connectivity, out int weight)
        where TCost : IGridCost
    {
        weight = cost.Weight(nx, ny);
        if (weight <= 0)
            return false;

        return GridDirection.IsAxial(direction)
            || connectivity == GridConnectivity.DiagonalCornerCutting
            || (cost.Weight(nx, y) > 0 && cost.Weight(x, ny) > 0);
    }
}

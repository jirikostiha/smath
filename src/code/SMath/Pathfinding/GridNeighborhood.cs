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
    /// </summary>
    /// <param name="weight"> Traversal weight of the neighbor. </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryStep<TCost>(TCost cost, int x, int y, int direction, int width, int height,
        GridConnectivity connectivity, out int nx, out int ny, out int weight)
        where TCost : IGridCost
    {
        var delta = GridDirection.Delta(direction);
        nx = x + delta.X;
        ny = y + delta.Y;
        weight = 0;

        if (!Grid.Contains(nx, ny, width, height))
            return false;

        weight = cost.Weight(nx, ny);
        if (weight <= 0)
            return false;

        return GridDirection.IsAxial(direction)
            || connectivity == GridConnectivity.DiagonalCornerCutting
            || (cost.Weight(nx, y) > 0 && cost.Weight(x, ny) > 0);
    }
}

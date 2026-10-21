using SMath.Grid2D;

namespace SMath.Pathfinding;

/// <summary>
/// Breadth first searches over cells of a two dimensional grid with a uniform step cost.
/// </summary>
/// <remarks>
/// It is significantly faster than a Dijkstra search but it applies to a uniform cost only.
/// Use it for reachability, distance fields, connected regions and flood fills.
/// <a href="https://en.wikipedia.org/wiki/Breadth-first_search">Wikipedia</a>
/// </remarks>
public static class GridFloodFill
{
    /// <summary> Value of a cell not reached by a search. </summary>
    public const int Unreached = -1;

    /// <summary>
    /// Calculate the count of steps from any source cell to every reachable cell of a grid.
    /// </summary>
    /// <typeparam name="TPassable"> Predicate matching the passable cells. </typeparam>
    /// <param name="sources"> Cells the search starts at, they get the zero distance. </param>
    /// <param name="width"> Count of cells in x direction. </param>
    /// <param name="height"> Count of cells in y direction. </param>
    /// <param name="isPassable"> Predicate matching the passable cells. </param>
    /// <param name="distances"> Row-major buffer of the resulting distances, unreached cells get <see cref="Unreached"/>. </param>
    /// <param name="queue"> Reusable buffer of at least width*height items. </param>
    /// <param name="connectivity"> Allowed steps. </param>
    /// <param name="maxDistance"> Upper limit of the distance, cells over it are left unreached. </param>
    /// <returns> Count of the reached cells. </returns>
    public static int Distances<TPassable>(ReadOnlySpan<(int X, int Y)> sources, int width, int height,
        TPassable isPassable, Span<int> distances, Span<int> queue,
        GridConnectivity connectivity = GridConnectivity.Axial, int maxDistance = int.MaxValue)
        where TPassable : IGridPredicate
    {
        var cellCount = width * height;
        distances[..cellCount].Fill(Unreached);

        var tail = 0;
        foreach (var source in sources)
        {
            if (!Grid.Contains(source, width, height))
                continue;

            var index = Grid.Index(source, width);
            if (distances[index] != Unreached)
                continue;

            distances[index] = 0;
            queue[tail++] = index;
        }

        var cost = new GridPassabilityCost<TPassable>(isPassable);
        var head = 0;

        while (head < tail)
        {
            var current = queue[head++];
            var distance = distances[current];
            if (distance < maxDistance)
                Expand(current, width, height, cost, connectivity, distances, distance + 1, queue, ref tail);
        }

        return tail;
    }

    /// <summary>
    /// Calculate the count of steps from a source cell to every reachable cell of a grid.
    /// </summary>
    public static int Distances<TPassable>((int X, int Y) source, int width, int height,
        TPassable isPassable, Span<int> distances, Span<int> queue,
        GridConnectivity connectivity = GridConnectivity.Axial, int maxDistance = int.MaxValue)
        where TPassable : IGridPredicate
    {
        Span<(int X, int Y)> sources = stackalloc (int X, int Y)[1] { source };
        return Distances(sources, width, height, isPassable, distances, queue, connectivity, maxDistance);
    }

    /// <summary>
    /// Determine if there is a path of passable cells between two cells.
    /// </summary>
    /// <remarks>
    /// It is the cheapest way to reject an unreachable target before running a full search.
    /// </remarks>
    public static bool AreConnected<TPassable>((int X, int Y) from, (int X, int Y) to, int width, int height,
        TPassable isPassable, Span<int> distances, Span<int> queue,
        GridConnectivity connectivity = GridConnectivity.Axial)
        where TPassable : IGridPredicate
    {
        if (!Grid.Contains(from, width, height) || !Grid.Contains(to, width, height))
            return false;

        Distances(from, width, height, isPassable, distances, queue, connectivity);

        return distances[Grid.Index(to, width)] != Unreached;
    }

    /// <summary>
    /// Label every connected region of passable cells of a grid by its own index.
    /// </summary>
    /// <typeparam name="TPassable"> Predicate matching the passable cells. </typeparam>
    /// <param name="width"> Count of cells in x direction. </param>
    /// <param name="height"> Count of cells in y direction. </param>
    /// <param name="isPassable"> Predicate matching the passable cells. </param>
    /// <param name="labels"> Row-major buffer of the resulting region indexes, impassable cells get <see cref="Unreached"/>. </param>
    /// <param name="queue"> Reusable buffer of at least width*height items. </param>
    /// <param name="connectivity"> Allowed steps. </param>
    /// <returns> Count of the found regions. </returns>
    public static int Regions<TPassable>(int width, int height, TPassable isPassable,
        Span<int> labels, Span<int> queue, GridConnectivity connectivity = GridConnectivity.Axial)
        where TPassable : IGridPredicate
    {
        var cellCount = width * height;
        labels[..cellCount].Fill(Unreached);

        var cost = new GridPassabilityCost<TPassable>(isPassable);
        var regionCount = 0;

        for (var seed = 0; seed < cellCount; seed++)
        {
            var (seedY, seedX) = Math.DivRem(seed, width);
            if (labels[seed] != Unreached || !isPassable.Test(seedX, seedY))
                continue;

            var label = regionCount++;
            labels[seed] = label;

            var head = 0;
            var tail = 0;
            queue[tail++] = seed;

            while (head < tail)
                Expand(queue[head++], width, height, cost, connectivity, labels, label, queue, ref tail);
        }

        return regionCount;
    }

    // writes the value to every unreached neighbor the cell may step to and queues it
    private static void Expand<TPassable>(int current, int width, int height, GridPassabilityCost<TPassable> cost,
        GridConnectivity connectivity, Span<int> values, int value, Span<int> queue, ref int tail)
        where TPassable : IGridPredicate
    {
        var directionIncrement = GridNeighborhood.DirectionIncrement(connectivity);
        var (y, x) = Math.DivRem(current, width);

        for (var direction = 0; direction < GridDirection.Count; direction += directionIncrement)
        {
            // the cheap test of a reached cell goes before the predicate calls of the step rule
            if (!GridNeighborhood.TryNeighbor(x, y, direction, width, height, out var nx, out var ny))
                continue;

            var neighbor = (ny * width) + nx;
            if (values[neighbor] != Unreached || !GridNeighborhood.IsAllowed(cost, x, y, nx, ny, direction, connectivity, out _))
                continue;

            values[neighbor] = value;
            queue[tail++] = neighbor;
        }
    }
}

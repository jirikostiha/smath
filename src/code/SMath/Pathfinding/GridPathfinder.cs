using SMath.Grid2D;

namespace SMath.Pathfinding;

/// <summary>
/// Shortest path searches over cells of a two dimensional grid.
/// </summary>
/// <remarks>
/// All buffers are allocated once per instance and reused by every search,
/// therefore a repeated search does not allocate at all.
/// Cells are stamped by a search generation instead of being cleared,
/// so the cost of a search is proportional to the count of the visited cells,
/// not to the size of the grid.
/// An instance is not thread safe, use one instance per thread.
/// <a href="https://en.wikipedia.org/wiki/A*_search_algorithm">Wikipedia</a>
/// </remarks>
public sealed class GridPathfinder
{
    private readonly IndexedMinHeap _open;
    private readonly int[] _costs;
    private readonly int[] _cameFrom;
    private readonly int[] _stamps;
    private int _generation;

    /// <summary> Buffer index of the target cell of the last successful search. </summary>
    private int _target = -1;

    public GridPathfinder(int width, int height)
    {
        if (width <= 0)
            throw new ArgumentOutOfRangeException(nameof(width), width, "Width must be positive.");
        if (height <= 0)
            throw new ArgumentOutOfRangeException(nameof(height), height, "Height must be positive.");

        Width = width;
        Height = height;

        var cellCount = width * height;
        _open = new IndexedMinHeap(cellCount);
        _costs = new int[cellCount];
        _cameFrom = new int[cellCount];
        _stamps = new int[cellCount];
    }

    /// <summary> Count of cells in x direction. </summary>
    public int Width { get; }

    /// <summary> Count of cells in y direction. </summary>
    public int Height { get; }

    /// <summary> Count of cells of the grid. </summary>
    public int CellCount
        => _costs.Length;

    /// <summary> Count of cells taken out of the open set by the last search. </summary>
    public int VisitedCellCount { get; private set; }

    /// <summary>
    /// Cost of the path found by the last search scaled by <see cref="GridStep.StraightCost"/>.
    /// It is -1 if no path was found.
    /// </summary>
    public int PathCost
        => _target >= 0 ? _costs[_target] : -1;

    /// <summary> Count of cells of the path found by the last search including both ends. </summary>
    public int PathCellCount
    {
        get
        {
            if (_target < 0)
                return 0;

            var count = 1;
            for (var cell = _target; _cameFrom[cell] != cell; cell = _cameFrom[cell])
                count++;

            return count;
        }
    }

    /// <summary>
    /// Find the shortest path between two cells.
    /// </summary>
    /// <typeparam name="TCost"> Traversal weight of cells. </typeparam>
    /// <typeparam name="THeuristic"> Estimate of the remaining cost. </typeparam>
    /// <param name="start"> Start cell. Its weight is not tested. </param>
    /// <param name="target"> Target cell. </param>
    /// <param name="cost"> Traversal weight of cells. </param>
    /// <param name="heuristic"> Estimate of the remaining cost. Use <see cref="ZeroHeuristic"/> for the Dijkstra search. </param>
    /// <param name="connectivity"> Allowed steps. </param>
    /// <returns> True if a path exists. Read it by <see cref="GetPath(Span{ValueTuple{int, int}})"/> or <see cref="Path"/>. </returns>
    public bool FindPath<TCost, THeuristic>((int X, int Y) start, (int X, int Y) target,
        TCost cost, THeuristic heuristic, GridConnectivity connectivity = GridConnectivity.Axial)
        where TCost : IGridCost
        where THeuristic : IGridHeuristic
    {
        _target = -1;
        VisitedCellCount = 0;

        if (!Grid.Contains(start, Width, Height) || !Grid.Contains(target, Width, Height))
            return false;
        if (cost.Weight(target.X, target.Y) <= 0)
            return false;

        NextGeneration();

        var startIndex = Grid.Index(start, Width);
        var targetIndex = Grid.Index(target, Width);

        _costs[startIndex] = 0;
        _cameFrom[startIndex] = startIndex;
        _stamps[startIndex] = _generation;
        _open.Push(startIndex, heuristic.Estimate(start.X, start.Y, target.X, target.Y));

        var directionStep = connectivity == GridConnectivity.Axial ? 2 : 1;
        var cutCorners = connectivity == GridConnectivity.DiagonalCornerCutting;
        var directions = GridDirection.All;

        while (!_open.IsEmpty)
        {
            var current = _open.Pop();
            VisitedCellCount++;

            if (current == targetIndex)
            {
                _target = targetIndex;
                return true;
            }

            var x = current % Width;
            var y = current / Width;
            var currentCost = _costs[current];

            for (var direction = 0; direction < GridDirection.Count; direction += directionStep)
            {
                var delta = directions[direction];
                var nx = x + delta.X;
                var ny = y + delta.Y;

                if (!Grid.Contains(nx, ny, Width, Height))
                    continue;

                var weight = cost.Weight(nx, ny);
                if (weight <= 0)
                    continue;

                var isDiagonal = (direction & 1) != 0;
                if (isDiagonal && !cutCorners
                    && (cost.Weight(nx, y) <= 0 || cost.Weight(x, ny) <= 0))
                    continue;

                var stepCost = isDiagonal ? GridStep.DiagonalCost : GridStep.StraightCost;
                var neighborCost = currentCost + (stepCost * weight);
                var neighbor = (ny * Width) + nx;

                if (_stamps[neighbor] == _generation && neighborCost >= _costs[neighbor])
                    continue;

                _stamps[neighbor] = _generation;
                _costs[neighbor] = neighborCost;
                _cameFrom[neighbor] = current;
                _open.Push(neighbor, neighborCost + heuristic.Estimate(nx, ny, target.X, target.Y));
            }
        }

        return false;
    }

    /// <summary>
    /// Find the shortest path between two cells over cells matching a passability predicate.
    /// </summary>
    public bool FindPath<TPassable>((int X, int Y) start, (int X, int Y) target, TPassable isPassable,
        GridConnectivity connectivity = GridConnectivity.Axial)
        where TPassable : IGridPredicate
        => connectivity == GridConnectivity.Axial
            ? FindPath(start, target, new GridPassabilityCost<TPassable>(isPassable), default(ManhattanHeuristic), connectivity)
            : FindPath(start, target, new GridPassabilityCost<TPassable>(isPassable), default(OctileHeuristic), connectivity);

    /// <summary>
    /// Write the path found by the last search into a buffer ordered from the start to the target cell.
    /// </summary>
    /// <returns> Count of written cells, see <see cref="PathCellCount"/>. It is 0 if no path was found. </returns>
    public int GetPath(Span<(int X, int Y)> path)
    {
        if (_target < 0)
            return 0;

        var count = 0;
        for (var cell = _target; ; cell = _cameFrom[cell])
        {
            path[count++] = (cell % Width, cell / Width);
            if (_cameFrom[cell] == cell)
                break;
        }

        path[..count].Reverse();

        return count;
    }

    /// <summary>
    /// Write buffer indexes of the path found by the last search into a buffer
    /// ordered from the start to the target cell.
    /// </summary>
    /// <returns> Count of written cells, see <see cref="PathCellCount"/>. It is 0 if no path was found. </returns>
    public int GetPathIndexes(Span<int> path)
    {
        if (_target < 0)
            return 0;

        var count = 0;
        for (var cell = _target; ; cell = _cameFrom[cell])
        {
            path[count++] = cell;
            if (_cameFrom[cell] == cell)
                break;
        }

        path[..count].Reverse();

        return count;
    }

    /// <summary>
    /// Cells of the path found by the last search ordered from the target to the start cell.
    /// </summary>
    /// <remarks>
    /// It is the cheapest way to read the path, no buffer of the path length is needed.
    /// </remarks>
    public IEnumerable<(int X, int Y)> ReversePath()
    {
        if (_target < 0)
            yield break;

        for (var cell = _target; ; cell = _cameFrom[cell])
        {
            yield return (cell % Width, cell / Width);
            if (_cameFrom[cell] == cell)
                yield break;
        }
    }

    /// <summary>
    /// Cells of the path found by the last search ordered from the start to the target cell.
    /// </summary>
    public IEnumerable<(int X, int Y)> Path()
    {
        var count = PathCellCount;
        if (count == 0)
            return Enumerable.Empty<(int X, int Y)>();

        var path = new (int X, int Y)[count];
        GetPath(path);

        return path;
    }

    /// <summary>
    /// Calculate the cost of the cheapest path from any source cell to every cell of the grid.
    /// It is the multi-source Dijkstra search, the base of a flow field.
    /// </summary>
    /// <typeparam name="TCost"> Traversal weight of cells. </typeparam>
    /// <param name="sources"> Cells the search starts at, they get the zero cost. </param>
    /// <param name="cost"> Traversal weight of cells. </param>
    /// <param name="distances">
    /// Row-major buffer of the resulting costs scaled by <see cref="GridStep.StraightCost"/>.
    /// Unreachable cells get <see cref="int.MaxValue"/>.
    /// </param>
    /// <param name="connectivity"> Allowed steps. </param>
    /// <param name="maxCost"> Upper limit of the cost, cells over it are left unreachable. </param>
    /// <returns> Count of the reached cells. </returns>
    public int Fill<TCost>(ReadOnlySpan<(int X, int Y)> sources, TCost cost, Span<int> distances,
        GridConnectivity connectivity = GridConnectivity.Axial, int maxCost = int.MaxValue)
        where TCost : IGridCost
    {
        _target = -1;
        VisitedCellCount = 0;
        distances[..CellCount].Fill(int.MaxValue);

        NextGeneration();

        foreach (var source in sources)
        {
            if (!Grid.Contains(source, Width, Height))
                continue;

            var sourceIndex = Grid.Index(source, Width);
            _costs[sourceIndex] = 0;
            _cameFrom[sourceIndex] = sourceIndex;
            _stamps[sourceIndex] = _generation;
            _open.Push(sourceIndex, 0);
        }

        var directionStep = connectivity == GridConnectivity.Axial ? 2 : 1;
        var cutCorners = connectivity == GridConnectivity.DiagonalCornerCutting;
        var directions = GridDirection.All;
        var reachedCount = 0;

        while (!_open.IsEmpty)
        {
            var current = _open.Pop();
            var currentCost = _costs[current];

            distances[current] = currentCost;
            reachedCount++;
            VisitedCellCount++;

            var x = current % Width;
            var y = current / Width;

            for (var direction = 0; direction < GridDirection.Count; direction += directionStep)
            {
                var delta = directions[direction];
                var nx = x + delta.X;
                var ny = y + delta.Y;

                if (!Grid.Contains(nx, ny, Width, Height))
                    continue;

                var weight = cost.Weight(nx, ny);
                if (weight <= 0)
                    continue;

                var isDiagonal = (direction & 1) != 0;
                if (isDiagonal && !cutCorners
                    && (cost.Weight(nx, y) <= 0 || cost.Weight(x, ny) <= 0))
                    continue;

                var stepCost = isDiagonal ? GridStep.DiagonalCost : GridStep.StraightCost;
                var neighborCost = currentCost + (stepCost * weight);
                if (neighborCost > maxCost)
                    continue;

                var neighbor = (ny * Width) + nx;
                if (_stamps[neighbor] == _generation && neighborCost >= _costs[neighbor])
                    continue;

                _stamps[neighbor] = _generation;
                _costs[neighbor] = neighborCost;
                _cameFrom[neighbor] = current;
                _open.Push(neighbor, neighborCost);
            }
        }

        return reachedCount;
    }

    /// <summary>
    /// Calculate the cost of the cheapest path from a source cell to every cell of the grid.
    /// </summary>
    public int Fill<TCost>((int X, int Y) source, TCost cost, Span<int> distances,
        GridConnectivity connectivity = GridConnectivity.Axial, int maxCost = int.MaxValue)
        where TCost : IGridCost
    {
        Span<(int X, int Y)> sources = stackalloc (int X, int Y)[1] { source };
        return Fill(sources, cost, distances, connectivity, maxCost);
    }

    private void NextGeneration()
    {
        _open.Clear();

        if (++_generation != int.MinValue)
            return;

        // the stamp wrapped around, drop all of them at once
        Array.Clear(_stamps);
        _generation = 1;
    }
}

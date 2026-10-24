using SMath.Grid2D;

namespace SMath.Pathfinding;

/// <summary>Field of directions leading every cell of a two dimensional grid towards the closest target.</summary>
/// <remarks>
/// It is the tool of choice when many agents head to the same few targets.
/// One build costs a single Dijkstra search over the grid,
/// afterwards every agent gets its step in constant time.
/// <a href="https://en.wikipedia.org/wiki/Vector_field">Wikipedia</a>
/// </remarks>
public static class FlowField
{
    /// <summary>Build a field of directions from a field of costs.</summary>
    /// <param name="distances"> Row-major costs, see <see cref="GridPathfinder.Fill{TCost}(ReadOnlySpan{ValueTuple{int, int}}, TCost, Span{int}, GridConnectivity, int)"/>. </param>
    /// <param name="width"> Count of cells in x direction. </param>
    /// <param name="height"> Count of cells in y direction. </param>
    /// <param name="directions">
    /// Row-major buffer of the resulting direction indexes, see <see cref="GridDirection"/>.
    /// Targets and unreachable cells get <see cref="GridDirection.None"/>.
    /// With <see cref="GridConnectivity.Diagonal"/> an unreached axial neighbor blocks the diagonal steps past it,
    /// so after a fill limited by a maximal cost a cell next to the limit may get no direction.
    /// </param>
    /// <param name="connectivity"> Allowed steps. </param>
    /// <returns> Count of cells with a direction. </returns>
    public static int FromDistances(ReadOnlySpan<int> distances, int width, int height, Span<byte> directions,
        GridConnectivity connectivity = GridConnectivity.Axial)
    {
        var directionIncrement = GridNeighborhood.DirectionIncrement(connectivity);
        var keepOffCorners = connectivity == GridConnectivity.Diagonal;
        var deltas = GridDirection.All;
        var count = 0;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var index = (y * width) + x;
                var distance = distances[index];

                directions[index] = GridDirection.None;
                if (distance == int.MaxValue || distance == 0)
                    continue;

                var bestDistance = distance;
                for (var direction = 0; direction < GridDirection.Count; direction += directionIncrement)
                {
                    var delta = deltas[direction];
                    var nx = x + delta.X;
                    var ny = y + delta.Y;

                    if (!Grid.Contains(nx, ny, width, height))
                        continue;

                    // an unreached axial neighbor of a reached cell is impassable or over the cost limit,
                    // a diagonal step past it slips through the corner the search refused
                    if (keepOffCorners && !GridDirection.IsAxial(direction)
                        && (distances[(y * width) + nx] == int.MaxValue || distances[(ny * width) + x] == int.MaxValue))
                        continue;

                    var neighborDistance = distances[(ny * width) + nx];
                    if (neighborDistance < bestDistance)
                    {
                        bestDistance = neighborDistance;
                        directions[index] = (byte)direction;
                    }
                }

                if (directions[index] != GridDirection.None)
                    count++;
            }
        }

        return count;
    }

    /// <summary>Cell the field leads to from a cell.</summary>
    /// <returns> The next cell or the very same cell if it is a target or unreachable. </returns>
    public static (int X, int Y) Next(ReadOnlySpan<byte> directions, int width, (int X, int Y) cell)
    {
        var direction = directions[(cell.Y * width) + cell.X];

        return direction == GridDirection.None
            ? cell
            : GridDirection.Step(cell, direction);
    }

    /// <summary>
    /// Cells the field leads through from a cell towards a target.
    /// The start cell is included, the target cell is the last one.
    /// </summary>
    public static IEnumerable<(int X, int Y)> Path(byte[] directions, int width, (int X, int Y) from)
    {
        ArgumentNullException.ThrowIfNull(directions);

        var cell = from;

        while (true)
        {
            yield return cell;

            var next = Next(directions, width, cell);
            if (next == cell)
                yield break;

            cell = next;
        }
    }
}

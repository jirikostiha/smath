using System.Linq;
using SMath.Grid2D;
using SMath.Pathfinding;
using Xunit;

namespace SMath;

public class FlowFieldTests
{
    private readonly struct OpenMap : IGridPredicate
    {
        public bool Test(int x, int y)
            => true;
    }

    [Fact]
    public void FromDistances_OfTargetCell_HasNoDirection()
    {
        var distances = new[] { 0, 1, 2, 3 };
        var directions = new byte[distances.Length];

        var count = FlowField.FromDistances(distances, 4, 1, directions);

        Assert.Equal(3, count);
        Assert.Equal(GridDirection.None, directions[0]);
    }

    [Fact]
    public void FromDistances_OfUnreachableCell_HasNoDirection()
    {
        var distances = new[] { 0, 1, int.MaxValue, int.MaxValue };
        var directions = new byte[distances.Length];

        FlowField.FromDistances(distances, 4, 1, directions);

        Assert.Equal(GridDirection.None, directions[2]);
        Assert.Equal(GridDirection.None, directions[3]);
    }

    [Fact]
    public void Next_LeadsDownhill()
    {
        var distances = new[] { 0, 1, 2, 3 };
        var directions = new byte[distances.Length];

        FlowField.FromDistances(distances, 4, 1, directions);

        Assert.Equal((2, 0), FlowField.Next(directions, 4, (3, 0)));
        Assert.Equal((0, 0), FlowField.Next(directions, 4, (0, 0)));
    }

    [Fact]
    public void Path_EndsAtTarget()
    {
        var pathfinder = new GridPathfinder(5, 5);
        var distances = new int[25];
        var directions = new byte[25];

        pathfinder.Fill((0, 0), new GridPassabilityCost<OpenMap>(default), distances);
        FlowField.FromDistances(distances, 5, 5, directions);

        var path = FlowField.Path(directions, 5, (4, 4)).ToArray();

        Assert.Equal((4, 4), path[0]);
        Assert.Equal((0, 0), path[^1]);
        Assert.Equal(9, path.Length);
    }
}

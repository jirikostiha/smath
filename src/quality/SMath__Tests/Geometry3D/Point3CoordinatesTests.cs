using System.Linq;
using Xunit;

namespace SMath.Geometry3D;

public class Point3CoordinatesTests
{
    private static readonly (int X, int Y, int Z) Center = (1, 1, 1);

    /// <summary> All coordinates of a box around the center, the reference the generators are checked against. </summary>
    private static IEnumerable<(int X, int Y, int Z)> Box((int X, int Y, int Z) center, int reach)
    {
        for (var x = center.X - reach; x <= center.X + reach; x++)
            for (var y = center.Y - reach; y <= center.Y + reach; y++)
                for (var z = center.Z - reach; z <= center.Z + reach; z++)
                    yield return (x, y, z);
    }

    private static (int X, int Y, int Z)[] Sorted(IEnumerable<(int X, int Y, int Z)> coordinates)
        => coordinates.OrderBy(c => c.X).ThenBy(c => c.Y).ThenBy(c => c.Z).ToArray();

    private static bool Within((int X, int Y, int Z) c, (int X, int Y, int Z) bottom, (int X, int Y, int Z) top)
        => c.X >= bottom.X && c.X <= top.X
        && c.Y >= bottom.Y && c.Y <= top.Y
        && c.Z >= bottom.Z && c.Z <= top.Z;

    [Fact]
    public void CoordinatesAtManhattanDistance_FormsTheOctahedronSurface()
    {
        var coords = Point3.CoordinatesAtManhattanDistance(Center, 2).ToArray();

        Assert.Equal(18, coords.Length);
        Assert.Equal(coords.Length, coords.Distinct().Count());
        Assert.Equal(
            Sorted(Box(Center, 2).Where(c => Point3.ManhattanDistance(c, Center) == 2)),
            Sorted(coords));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void CoordinatesAtManhattanDistance_OfNonPositiveDistance_IsEmpty(int distance)
    {
        Assert.Empty(Point3.CoordinatesAtManhattanDistance(Center, distance));
        Assert.Empty(Point3.CoordinatesAtManhattanDistance(Center, distance, (-5, -5, -5), (5, 5, 5)));
    }

    [Fact]
    public void CoordinatesAtManhattanDistanceWithLimits_KeepsOnlyCoordinatesInBounds()
    {
        var bottom = (0, 0, 0);
        var top = (5, 5, 5);

        var coords = Point3.CoordinatesAtManhattanDistance(Center, 2, bottom, top).ToArray();

        Assert.Equal(
            Sorted(Box(Center, 2).Where(c => Point3.ManhattanDistance(c, Center) == 2 && Within(c, bottom, top))),
            Sorted(coords));
    }

    [Fact]
    public void CoordinatesAtManhattanDistanceWithLimitsWiderThanDistance_SameAsWithoutLimits()
    {
        var unlimited = Point3.CoordinatesAtManhattanDistance(Center, 3).ToArray();
        var limited = Point3.CoordinatesAtManhattanDistance(Center, 3, (-100, -100, -100), (100, 100, 100)).ToArray();

        Assert.Equal(unlimited, limited);
    }

    [Fact]
    public void CoordinatesUpToManhattanDistance_FormsTheSolidOctahedron()
    {
        var coords = Point3.CoordinatesUpToManhattanDistance(Center, 2).ToArray();

        Assert.Equal(25, coords.Length);
        Assert.Equal(coords.Length, coords.Distinct().Count());
        Assert.Equal(
            Sorted(Box(Center, 2).Where(c => Point3.ManhattanDistance(c, Center) <= 2)),
            Sorted(coords));
    }

    [Fact]
    public void CoordinatesUpToManhattanDistance_ZeroDistance_ContainsCenterOnly()
    {
        Assert.Equal(new[] { Center }, Point3.CoordinatesUpToManhattanDistance(Center, 0).ToArray());
        Assert.Equal(new[] { Center }, Point3.CoordinatesUpToManhattanDistance(Center, 0, (0, 0, 0), (2, 2, 2)).ToArray());
        Assert.Empty(Point3.CoordinatesUpToManhattanDistance(Center, -1));
    }

    [Fact]
    public void CoordinatesUpToManhattanDistanceWithLimits_KeepsOnlyCoordinatesInBounds()
    {
        var bottom = (0, -1, 0);
        var top = (2, 2, 1);

        var coords = Point3.CoordinatesUpToManhattanDistance(Center, 2, bottom, top).ToArray();

        Assert.Equal(
            Sorted(Box(Center, 2).Where(c => Point3.ManhattanDistance(c, Center) <= 2 && Within(c, bottom, top))),
            Sorted(coords));
    }

    [Fact]
    public void CoordinatesInManhattanDistanceRange_IsTheDifferenceOfTwoBalls()
    {
        var coords = Point3.CoordinatesInManhattanDistanceRange(Center, 2, 3).ToArray();

        Assert.Equal(coords.Length, coords.Distinct().Count());
        Assert.Equal(
            Sorted(Box(Center, 3).Where(c =>
            {
                var distance = Point3.ManhattanDistance(c, Center);
                return distance >= 2 && distance <= 3;
            })),
            Sorted(coords));
    }

    [Fact]
    public void CoordinatesInManhattanDistanceRange_NegativeMinimum_IsClampedToZero()
    {
        Assert.Equal(
            Sorted(Point3.CoordinatesUpToManhattanDistance(Center, 2)),
            Sorted(Point3.CoordinatesInManhattanDistanceRange(Center, -3, 2)));
    }

    [Fact]
    public void CoordinatesInManhattanDistanceRange_MaximumBelowMinimum_IsEmpty()
    {
        Assert.Empty(Point3.CoordinatesInManhattanDistanceRange(Center, 3, 2));
        Assert.Empty(Point3.CoordinatesInManhattanDistanceRange(Center, 3, 2, (0, 0, 0), (5, 5, 5)));
    }

    [Fact]
    public void CoordinatesInManhattanDistanceRangeWithLimits_KeepsOnlyCoordinatesInBounds()
    {
        var bottom = (0, 0, 0);
        var top = (3, 3, 2);

        var coords = Point3.CoordinatesInManhattanDistanceRange(Center, 1, 3, bottom, top).ToArray();

        Assert.Equal(
            Sorted(Box(Center, 3).Where(c =>
            {
                var distance = Point3.ManhattanDistance(c, Center);
                return distance >= 1 && distance <= 3 && Within(c, bottom, top);
            })),
            Sorted(coords));
    }

    [Fact]
    public void CoordinatesAtChebyshevDistance_FormsTheCubeSurface()
    {
        var coords = Point3.CoordinatesAtChebyshevDistance(Center, 2).ToArray();

        Assert.Equal(98, coords.Length); // 5^3 - 3^3
        Assert.Equal(coords.Length, coords.Distinct().Count());
        Assert.Equal(
            Sorted(Box(Center, 2).Where(c => Point3.ChebyshevDistance(c, Center) == 2)),
            Sorted(coords));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void CoordinatesAtChebyshevDistance_OfNonPositiveDistance_IsEmpty(int distance)
    {
        Assert.Empty(Point3.CoordinatesAtChebyshevDistance(Center, distance));
        Assert.Empty(Point3.CoordinatesAtChebyshevDistance(Center, distance, (-5, -5, -5), (5, 5, 5)));
    }

    [Fact]
    public void CoordinatesAtChebyshevDistanceWithLimits_KeepsOnlyCoordinatesInBounds()
    {
        var bottom = (0, 0, 0);
        var top = (5, 5, 2);

        var coords = Point3.CoordinatesAtChebyshevDistance(Center, 2, bottom, top).ToArray();

        Assert.Equal(coords.Length, coords.Distinct().Count());
        Assert.Equal(
            Sorted(Box(Center, 2).Where(c => Point3.ChebyshevDistance(c, Center) == 2 && Within(c, bottom, top))),
            Sorted(coords));
    }

    [Theory]
    [InlineData(-3, 2, 2)]
    [InlineData(2, 9, 2)]
    [InlineData(2, 2, -4)]
    public void CoordinatesAtChebyshevDistanceWithCenterOutOfLimits_NoCoords(int centerX, int centerY, int centerZ)
    {
        var coords = Point3.CoordinatesAtChebyshevDistance((centerX, centerY, centerZ), 1, (0, 0, 0), (5, 5, 5)).ToArray();

        Assert.Empty(coords);
    }

    [Fact]
    public void CoordinatesAtChebyshevDistanceWithLimitsWiderThanDistance_SameAsWithoutLimits()
    {
        var unlimited = Point3.CoordinatesAtChebyshevDistance(Center, 2).ToArray();
        var limited = Point3.CoordinatesAtChebyshevDistance(Center, 2, (-100, -100, -100), (100, 100, 100)).ToArray();

        Assert.Equal(unlimited, limited);
    }

    [Fact]
    public void CoordinatesUpToChebyshevDistance_FormsTheSolidCube()
    {
        var coords = Point3.CoordinatesUpToChebyshevDistance(Center, 2).ToArray();

        Assert.Equal(125, coords.Length);
        Assert.Equal(coords.Length, coords.Distinct().Count());
        Assert.Equal(Sorted(Box(Center, 2)), Sorted(coords));
    }

    [Fact]
    public void CoordinatesUpToChebyshevDistance_ZeroDistance_ContainsCenter()
    {
        Assert.Equal(new[] { Center }, Point3.CoordinatesUpToChebyshevDistance(Center, 0).ToArray());
        Assert.Equal(new[] { Center }, Point3.CoordinatesUpToChebyshevDistance(Center, 0, (0, 0, 0), (2, 2, 2)).ToArray());
        Assert.Empty(Point3.CoordinatesUpToChebyshevDistance(Center, -1));
    }

    [Fact]
    public void CoordinatesUpToChebyshevDistance_ZeroDistance_IsConsistentWithManhattan()
    {
        Assert.Equal(
            Point3.CoordinatesUpToManhattanDistance(Center, 0).ToArray(),
            Point3.CoordinatesUpToChebyshevDistance(Center, 0).ToArray());
    }

    [Fact]
    public void CoordinatesUpToChebyshevDistanceWithLimits_KeepsOnlyCoordinatesInBounds()
    {
        var bottom = (0, -1, 0);
        var top = (2, 2, 1);

        var coords = Point3.CoordinatesUpToChebyshevDistance(Center, 2, bottom, top).ToArray();

        Assert.Equal(
            Sorted(Box(Center, 2).Where(c => Within(c, bottom, top))),
            Sorted(coords));
    }

    [Fact]
    public void CoordinatesInChebyshevDistanceRange_IsTheDifferenceOfTwoCubes()
    {
        var coords = Point3.CoordinatesInChebyshevDistanceRange(Center, 2, 3).ToArray();

        Assert.Equal(coords.Length, coords.Distinct().Count());
        Assert.Equal(
            Sorted(Box(Center, 3).Where(c =>
            {
                var distance = Point3.ChebyshevDistance(c, Center);
                return distance >= 2 && distance <= 3;
            })),
            Sorted(coords));
    }

    [Fact]
    public void CoordinatesInChebyshevDistanceRange_MaximumBelowMinimum_IsEmpty()
    {
        Assert.Empty(Point3.CoordinatesInChebyshevDistanceRange(Center, 3, 2));
        Assert.Empty(Point3.CoordinatesInChebyshevDistanceRange(Center, 3, 2, (0, 0, 0), (5, 5, 5)));
    }

    [Fact]
    public void CoordinatesInChebyshevDistanceRangeWithLimits_KeepsOnlyCoordinatesInBounds()
    {
        var bottom = (0, 0, 0);
        var top = (3, 3, 2);

        var coords = Point3.CoordinatesInChebyshevDistanceRange(Center, 1, 3, bottom, top).ToArray();

        Assert.Equal(
            Sorted(Box(Center, 3).Where(c =>
            {
                var distance = Point3.ChebyshevDistance(c, Center);
                return distance >= 1 && distance <= 3 && Within(c, bottom, top);
            })),
            Sorted(coords));
    }
}

using System;
using SMath.Geometry2D;
using Xunit;

namespace SMath;

public class SteeringTests
{
    private const double Tolerance = 1e-10;

    [Fact]
    public void Seek_HeadsToTargetAtFullSpeed()
    {
        var velocity = Steering.Seek((0d, 0d), (3d, 4d), 10d);

        Assert.Equal(6d, velocity.X, Tolerance);
        Assert.Equal(8d, velocity.Y, Tolerance);
    }

    [Fact]
    public void Seek_OfTheVeryPosition_IsZero()
        => Assert.Equal((0d, 0d), Steering.Seek((1d, 1d), (1d, 1d), 10d));

    [Fact]
    public void Flee_IsOppositeOfSeek()
    {
        var seek = Steering.Seek((0d, 0d), (3d, 4d), 10d);
        var flee = Steering.Flee((0d, 0d), (3d, 4d), 10d);

        Assert.Equal(-seek.X, flee.X, Tolerance);
        Assert.Equal(-seek.Y, flee.Y, Tolerance);
    }

    [Fact]
    public void Arrive_OutOfBrakingRadius_IsFullSpeed()
    {
        var velocity = Steering.Arrive((0d, 0d), (10d, 0d), 5d, 2d);

        Assert.Equal(5d, velocity.X, Tolerance);
    }

    [Fact]
    public void Arrive_InBrakingRadius_SlowsDown()
    {
        var velocity = Steering.Arrive((0d, 0d), (1d, 0d), 4d, 2d);

        Assert.Equal(2d, velocity.X, Tolerance);
    }

    [Fact]
    public void Arrive_AtTarget_IsZero()
        => Assert.Equal((0d, 0d), Steering.Arrive((1d, 1d), (1d, 1d), 4d, 2d));

    [Fact]
    public void Pursue_LeadsTheTarget()
    {
        // the target at (10,0) runs up, the agent must aim over it
        var velocity = Steering.Pursue((0d, 0d), (10d, 0d), (0d, 10d), 10d);

        Assert.True(velocity.Y > 0d);
    }

    [Fact]
    public void Evade_IsOppositeOfPursue()
    {
        var pursue = Steering.Pursue((0d, 0d), (10d, 0d), (0d, 10d), 10d);
        var evade = Steering.Evade((0d, 0d), (10d, 0d), (0d, 10d), 10d);

        Assert.Equal(-pursue.X, evade.X, Tolerance);
        Assert.Equal(-pursue.Y, evade.Y, Tolerance);
    }

    [Fact]
    public void Separate_PushesAwayFromNeighbor()
    {
        ReadOnlySpan<(double X, double Y)> neighbors = stackalloc (double X, double Y)[] { (1d, 0d) };

        var velocity = Steering.Separate((0d, 0d), neighbors, 3d, 5d);

        Assert.Equal(-5d, velocity.X, Tolerance);
        Assert.Equal(0d, velocity.Y, Tolerance);
    }

    [Fact]
    public void Separate_OfFarNeighbors_IsZero()
    {
        ReadOnlySpan<(double X, double Y)> neighbors = stackalloc (double X, double Y)[] { (100d, 0d) };

        Assert.Equal((0d, 0d), Steering.Separate((0d, 0d), neighbors, 3d, 5d));
    }

    [Fact]
    public void Separate_OfBalancedNeighbors_IsZero()
    {
        ReadOnlySpan<(double X, double Y)> neighbors = stackalloc (double X, double Y)[] { (1d, 0d), (-1d, 0d) };

        Assert.Equal((0d, 0d), Steering.Separate((0d, 0d), neighbors, 3d, 5d));
    }

    [Fact]
    public void Cohere_HeadsToTheAverageOfNeighbors()
    {
        ReadOnlySpan<(double X, double Y)> neighbors = stackalloc (double X, double Y)[] { (2d, 0d), (4d, 0d) };

        var velocity = Steering.Cohere((0d, 0d), neighbors, 5d);

        Assert.Equal(5d, velocity.X, Tolerance);
    }

    [Fact]
    public void Cohere_OfNoNeighbors_IsZero()
        => Assert.Equal((0d, 0d), Steering.Cohere((0d, 0d), ReadOnlySpan<(double X, double Y)>.Empty, 5d));

    [Fact]
    public void Align_MatchesTheAverageHeading()
    {
        ReadOnlySpan<(double X, double Y)> velocities = stackalloc (double X, double Y)[] { (1d, 0d), (3d, 0d) };

        var velocity = Steering.Align(velocities, 5d);

        Assert.Equal(5d, velocity.X, Tolerance);
    }

    [Fact]
    public void Align_OfNoNeighbors_IsZero()
        => Assert.Equal((0d, 0d), Steering.Align(ReadOnlySpan<(double X, double Y)>.Empty, 5d));

    [Fact]
    public void Steer_OverTheForce_IsClamped()
    {
        var velocity = Steering.Steer((0d, 0d), (10d, 0d), 2d);

        Assert.Equal(2d, velocity.X, Tolerance);
    }

    [Fact]
    public void Steer_InTheForce_ReachesDesiredVelocity()
        => Assert.Equal((1d, 0d), Steering.Steer((0d, 0d), (1d, 0d), 2d));

    [Fact]
    public void ClampSpeed_OverMaxSpeed_KeepsDirection()
    {
        var velocity = Steering.ClampSpeed((3d, 4d), 5d);

        Assert.Equal(3d, velocity.X, Tolerance);
        Assert.Equal(4d, velocity.Y, Tolerance);

        var clamped = Steering.ClampSpeed((6d, 8d), 5d);

        Assert.Equal(3d, clamped.X, Tolerance);
        Assert.Equal(4d, clamped.Y, Tolerance);
    }

    [Fact]
    public void ClampSpeed_OfZeroVelocity_IsZero()
        => Assert.Equal((0d, 0d), Steering.ClampSpeed((0d, 0d), 5d));
}

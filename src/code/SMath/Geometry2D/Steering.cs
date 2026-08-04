using System.Numerics;

namespace SMath.Geometry2D;

/// <summary>
/// Steering behaviors of an agent moving in two dimensions.
/// </summary>
/// <remarks>
/// Every behavior returns a desired velocity, blend them by a weighted sum
/// and clamp the result to the maximal speed of the agent.
/// <a href="https://en.wikipedia.org/wiki/Motion_planning">Wikipedia</a>
/// </remarks>
public static class Steering
{
    /// <summary>
    /// Velocity heading straight to a target at the full speed.
    /// </summary>
    public static (N X, N Y) Seek<N>((N X, N Y) position, (N X, N Y) target, N maxSpeed)
        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
    {
        var dx = target.X - position.X;
        var dy = target.Y - position.Y;
        var distance = PT.Hypotenuse(dx, dy);

        return distance > N.Zero
            ? (dx / distance * maxSpeed, dy / distance * maxSpeed)
            : (N.Zero, N.Zero);
    }

    /// <summary>
    /// Velocity heading straight away from a target at the full speed.
    /// </summary>
    public static (N X, N Y) Flee<N>((N X, N Y) position, (N X, N Y) target, N maxSpeed)
        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
    {
        var velocity = Seek(position, target, maxSpeed);
        return (-velocity.X, -velocity.Y);
    }

    /// <summary>
    /// Velocity heading to a target with the speed decreasing inside of a braking radius.
    /// </summary>
    /// <param name="position"> Position of the agent. </param>
    /// <param name="target"> Target point. </param>
    /// <param name="maxSpeed"> Maximal speed of the agent. </param>
    /// <param name="brakingRadius"> Distance at which the agent starts to slow down. </param>
    public static (N X, N Y) Arrive<N>((N X, N Y) position, (N X, N Y) target, N maxSpeed, N brakingRadius)
        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
    {
        var dx = target.X - position.X;
        var dy = target.Y - position.Y;
        var distance = PT.Hypotenuse(dx, dy);

        if (distance <= N.Zero)
            return (N.Zero, N.Zero);

        var speed = distance < brakingRadius
            ? maxSpeed * distance / brakingRadius
            : maxSpeed;

        return (dx / distance * speed, dy / distance * speed);
    }

    /// <summary>
    /// Velocity heading to the predicted future position of a moving target.
    /// </summary>
    /// <remarks>
    /// The lead time is estimated from the distance and the speed of the agent.
    /// </remarks>
    public static (N X, N Y) Pursue<N>((N X, N Y) position, (N X, N Y) target, (N X, N Y) targetVelocity, N maxSpeed)
        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
    {
        var distance = Point2.Distance(position, target);
        var leadTime = maxSpeed > N.Zero ? distance / maxSpeed : N.Zero;

        var predicted = (target.X + (targetVelocity.X * leadTime),
                         target.Y + (targetVelocity.Y * leadTime));

        return Seek(position, predicted, maxSpeed);
    }

    /// <summary>
    /// Velocity heading away from the predicted future position of a moving hunter.
    /// </summary>
    public static (N X, N Y) Evade<N>((N X, N Y) position, (N X, N Y) hunter, (N X, N Y) hunterVelocity, N maxSpeed)
        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
    {
        var velocity = Pursue(position, hunter, hunterVelocity, maxSpeed);
        return (-velocity.X, -velocity.Y);
    }

    /// <summary>
    /// Velocity pushing an agent away from its neighbors.
    /// The weight of a neighbor falls with its distance.
    /// </summary>
    /// <remarks>
    /// It is the local avoidance keeping a crowd of agents from overlapping.
    /// </remarks>
    /// <param name="position"> Position of the agent. </param>
    /// <param name="neighbors"> Positions of the nearby agents. </param>
    /// <param name="radius"> Distance at which a neighbor stops to matter. </param>
    /// <param name="maxSpeed"> Maximal speed of the agent. </param>
    public static (N X, N Y) Separate<N>((N X, N Y) position, ReadOnlySpan<(N X, N Y)> neighbors, N radius, N maxSpeed)
        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
    {
        var sumX = N.Zero;
        var sumY = N.Zero;

        foreach (var neighbor in neighbors)
        {
            var dx = position.X - neighbor.X;
            var dy = position.Y - neighbor.Y;
            var distance = PT.Hypotenuse(dx, dy);

            if (distance <= N.Zero || distance >= radius)
                continue;

            // the closer the neighbor the stronger the push
            var weight = (radius - distance) / (distance * radius);
            sumX += dx * weight;
            sumY += dy * weight;
        }

        var length = PT.Hypotenuse(sumX, sumY);

        return length > N.Zero
            ? (sumX / length * maxSpeed, sumY / length * maxSpeed)
            : (N.Zero, N.Zero);
    }

    /// <summary>
    /// Velocity heading to the average position of the neighbors.
    /// </summary>
    public static (N X, N Y) Cohere<N>((N X, N Y) position, ReadOnlySpan<(N X, N Y)> neighbors, N maxSpeed)
        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
    {
        if (neighbors.Length == 0)
            return (N.Zero, N.Zero);

        var sumX = N.Zero;
        var sumY = N.Zero;

        foreach (var neighbor in neighbors)
        {
            sumX += neighbor.X;
            sumY += neighbor.Y;
        }

        var count = N.CreateTruncating(neighbors.Length);

        return Seek(position, (sumX / count, sumY / count), maxSpeed);
    }

    /// <summary>
    /// Velocity matching the average heading of the neighbors.
    /// </summary>
    public static (N X, N Y) Align<N>(ReadOnlySpan<(N X, N Y)> neighborVelocities, N maxSpeed)
        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
    {
        if (neighborVelocities.Length == 0)
            return (N.Zero, N.Zero);

        var sumX = N.Zero;
        var sumY = N.Zero;

        foreach (var velocity in neighborVelocities)
        {
            sumX += velocity.X;
            sumY += velocity.Y;
        }

        var length = PT.Hypotenuse(sumX, sumY);

        return length > N.Zero
            ? (sumX / length * maxSpeed, sumY / length * maxSpeed)
            : (N.Zero, N.Zero);
    }

    /// <summary>
    /// Velocity turned towards a desired velocity by at most a maximal steering force.
    /// </summary>
    /// <remarks>
    /// It gives the agent an inertia so it does not change its heading instantly.
    /// </remarks>
    /// <param name="velocity"> Current velocity of the agent. </param>
    /// <param name="desiredVelocity"> Desired velocity, see the other behaviors. </param>
    /// <param name="maxForce"> Maximal change of the velocity over the time step. </param>
    public static (N X, N Y) Steer<N>((N X, N Y) velocity, (N X, N Y) desiredVelocity, N maxForce)
        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
    {
        var dx = desiredVelocity.X - velocity.X;
        var dy = desiredVelocity.Y - velocity.Y;
        var length = PT.Hypotenuse(dx, dy);

        if (length <= maxForce)
            return desiredVelocity;

        var scale = maxForce / length;

        return (velocity.X + (dx * scale), velocity.Y + (dy * scale));
    }

    /// <summary>
    /// Velocity shortened to a maximal speed. Its direction is kept.
    /// </summary>
    public static (N X, N Y) ClampSpeed<N>((N X, N Y) velocity, N maxSpeed)
        where N : IRootFunctions<N>, IComparisonOperators<N, N, bool>
    {
        var speed = PT.Hypotenuse(velocity.X, velocity.Y);

        if (speed <= maxSpeed || speed <= N.Zero)
            return velocity;

        var scale = maxSpeed / speed;

        return (velocity.X * scale, velocity.Y * scale);
    }
}

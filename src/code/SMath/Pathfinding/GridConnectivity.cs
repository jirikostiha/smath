namespace SMath.Pathfinding;

/// <summary>
/// Neighborhood of a cell of a two dimensional grid used by a search.
/// </summary>
public enum GridConnectivity
{
    /// <summary> Steps in axes directions only, also known as the von Neumann neighborhood. </summary>
    Axial = 4,

    /// <summary>
    /// Steps in axes and diagonal directions, also known as the Moore neighborhood.
    /// A diagonal step is allowed only if both adjacent axial cells are passable,
    /// so an agent never slips through a corner of an obstacle.
    /// </summary>
    Diagonal = 8,

    /// <summary>
    /// Steps in axes and diagonal directions with corner cutting allowed.
    /// A diagonal step is allowed even between two obstacles.
    /// </summary>
    DiagonalCornerCutting = 9,
}

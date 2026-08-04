using System.Runtime.CompilerServices;

namespace SMath.Grid2D;

/// <summary>
/// Predicate over cells of a two dimensional grid.
/// </summary>
/// <remarks>
/// Implement it by a <see langword="struct"/> and pass it as a generic type argument
/// to let the runtime devirtualize and inline the calls.
/// </remarks>
public interface IGridPredicate
{
    /// <summary> Evaluate the predicate for a cell. </summary>
    bool Test(int x, int y);
}

/// <summary>
/// Predicate over cells of a two dimensional grid defined by a delegate.
/// </summary>
public readonly struct GridPredicate : IGridPredicate
{
    private readonly Func<int, int, bool> _predicate;

    public GridPredicate(Func<int, int, bool> predicate)
        => _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));

    /// <inheritdoc/>
    public bool Test(int x, int y)
        => _predicate(x, y);
}

/// <summary>
/// Predicate matching the cells another predicate does not match.
/// </summary>
/// <remarks>
/// Use it to pass a predicate of the passable cells where a predicate of the obstacles is expected
/// and the other way round.
/// </remarks>
/// <typeparam name="TPredicate"> Negated predicate. </typeparam>
public readonly struct GridNegatedPredicate<TPredicate> : IGridPredicate
    where TPredicate : IGridPredicate
{
    private readonly TPredicate _predicate;

    public GridNegatedPredicate(TPredicate predicate)
        => _predicate = predicate;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Test(int x, int y)
        => !_predicate.Test(x, y);
}

/// <summary>
/// Predicate matching cells of a grid marked by a non-zero value in a row-major mask.
/// Cells outside of the grid are matching too.
/// </summary>
public readonly struct GridMaskPredicate : IGridPredicate
{
    private readonly byte[] _mask;

    public GridMaskPredicate(byte[] mask, int width, int height)
    {
        ArgumentNullException.ThrowIfNull(mask);

        _mask = mask;
        Width = width;
        Height = height;
    }

    /// <summary> Count of cells in x direction. </summary>
    public int Width { get; }

    /// <summary> Count of cells in y direction. </summary>
    public int Height { get; }

    /// <inheritdoc/>
    public bool Test(int x, int y)
        => !Grid.Contains(x, y, Width, Height) || _mask[(y * Width) + x] != 0;
}

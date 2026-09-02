using System.Numerics;

namespace SMath;

/// <summary>
/// List extensions.
/// </summary>
public static class ListExtension
{
    /// <summary>
    /// Finds the kth smallest element of a list by a selected numeric value.
    /// </summary>
    public static T KthSmallestElement<T, N>(this IList<T> list, int k, Func<T, N> valueSelector)
        where N : IComparisonOperators<N, N, bool>
    {
        ArgumentNullException.ThrowIfNull(list);
        ArgumentNullException.ThrowIfNull(valueSelector);

        if (list.Count == 0)
            throw new ArgumentException("List cannot be empty.", nameof(list));

        if (k < 1 || k > list.Count)
            throw new ArgumentOutOfRangeException(nameof(k), k, "Rank k has to be between 1 and list count.");

        return QuickSelect(list, k - 1, 0, list.Count - 1, valueSelector, smallestFirst: true);
    }

    /// <summary>
    /// Finds the kth smallest element of a list.
    /// </summary>
    public static N KthSmallestElement<N>(this IList<N> list, int k)
        where N : IComparisonOperators<N, N, bool>
        => list.KthSmallestElement(k, x => x);

    /// <summary>
    /// Finds the kth smallest element within a range of a list.
    /// </summary>
    /// <remarks>
    /// Uses the quickselect algorithm. Note that the list is partially reordered in place.
    /// </remarks>
    /// <typeparam name="T"> Type of element. </typeparam>
    /// <typeparam name="N"> Type of numeric value. </typeparam>
    /// <param name="list"> List of elements. </param>
    /// <param name="k"> Rank of the element to find (1-based). </param>
    /// <param name="start"> Starting index. </param>
    /// <param name="end"> Ending index. </param>
    /// <param name="valueSelector"> Numeric value selector from element. </param>
    /// <returns> Kth smallest element. </returns>
    public static T KthSmallestElement<T, N>(this IList<T> list, int k, int start, int end, Func<T, N> valueSelector)
        where N : IComparisonOperators<N, N, bool>
    {
        ArgumentNullException.ThrowIfNull(list);
        ArgumentNullException.ThrowIfNull(valueSelector);

        if (list.Count == 0)
            throw new ArgumentException("List cannot be empty.", nameof(list));

        if (start < 0 || start >= list.Count)
            throw new ArgumentOutOfRangeException(nameof(start), start, "Start index has to be within list bounds.");

        if (end < 0 || end >= list.Count)
            throw new ArgumentOutOfRangeException(nameof(end), end, "End index has to be within list bounds.");

        if (start > end)
            throw new ArgumentException("Start index cannot be greater than end index.");

        if (k < 1 || k > list.Count)
            throw new ArgumentOutOfRangeException(nameof(k), k, "Rank k has to be between 1 and list count.");

        if (k - 1 < start || k - 1 > end)
            throw new ArgumentOutOfRangeException(nameof(k), k, "Rank k has to be within the specified range [start + 1, end + 1].");

        return QuickSelect(list, k - 1, start, end, valueSelector, smallestFirst: true);
    }

    /// <summary>
    /// Finds the kth largest element of a list by a selected numeric value.
    /// </summary>
    public static T KthLargestElement<T, N>(this IList<T> list, int k, Func<T, N> valueSelector)
        where N : IComparisonOperators<N, N, bool>
    {
        ArgumentNullException.ThrowIfNull(list);
        ArgumentNullException.ThrowIfNull(valueSelector);

        if (list.Count == 0)
            throw new ArgumentException("List cannot be empty.", nameof(list));

        if (k < 1 || k > list.Count)
            throw new ArgumentOutOfRangeException(nameof(k), k, "Rank k has to be between 1 and list count.");

        return QuickSelect(list, k - 1, 0, list.Count - 1, valueSelector, smallestFirst: false);
    }

    /// <summary>
    /// Finds the kth largest element of a list.
    /// </summary>
    public static N KthLargestElement<N>(this IList<N> list, int k)
        where N : IComparisonOperators<N, N, bool>
        => list.KthLargestElement(k, x => x);

    /// <summary>
    /// Finds the kth largest element within a range of a list.
    /// </summary>
    /// <remarks>
    /// Uses the quickselect algorithm. Note that the list is partially reordered in place.
    /// </remarks>
    /// <typeparam name="T"> Type of element. </typeparam>
    /// <typeparam name="N"> Type of numeric value. </typeparam>
    /// <param name="list"> List of elements. </param>
    /// <param name="k"> Rank of the element to find (1-based). </param>
    /// <param name="start"> Starting index. </param>
    /// <param name="end"> Ending index. </param>
    /// <param name="valueSelector"> Numeric value selector from element. </param>
    /// <returns> Kth largest element. </returns>
    public static T KthLargestElement<T, N>(this IList<T> list, int k, int start, int end, Func<T, N> valueSelector)
        where N : IComparisonOperators<N, N, bool>
    {
        ArgumentNullException.ThrowIfNull(list);
        ArgumentNullException.ThrowIfNull(valueSelector);

        if (list.Count == 0)
            throw new ArgumentException("List cannot be empty.", nameof(list));

        if (start < 0 || start >= list.Count)
            throw new ArgumentOutOfRangeException(nameof(start), start, "Start index has to be within list bounds.");

        if (end < 0 || end >= list.Count)
            throw new ArgumentOutOfRangeException(nameof(end), end, "End index has to be within list bounds.");

        if (start > end)
            throw new ArgumentException("Start index cannot be greater than end index.");

        if (k < 1 || k > list.Count)
            throw new ArgumentOutOfRangeException(nameof(k), k, "Rank k has to be between 1 and list count.");

        if (k - 1 < start || k - 1 > end)
            throw new ArgumentOutOfRangeException(nameof(k), k, "Rank k has to be within the specified range [start + 1, end + 1].");

        return QuickSelect(list, k - 1, start, end, valueSelector, smallestFirst: false);
    }

    private static T QuickSelect<T, N>(IList<T> list, int kIndex, int start, int end, Func<T, N> valueSelector, bool smallestFirst)
        where N : IComparisonOperators<N, N, bool>
    {
        while (start < end)
        {
            int q = Partition(list, start, end, valueSelector, smallestFirst);
            if (q == kIndex)
                return list[kIndex];
            else if (q > kIndex)
                end = q - 1;
            else
                start = q + 1;
        }

        return list[start];
    }

    /// <summary>
    /// Lomuto partition around the last element as pivot. When <paramref name="smallestFirst"/>
    /// is true elements are arranged in ascending order relative to the pivot, otherwise descending.
    /// </summary>
    private static int Partition<T, N>(IList<T> list, int p, int r, Func<T, N> valueSelector, bool smallestFirst)
        where N : IComparisonOperators<N, N, bool>
    {
        var pivot = valueSelector(list[r]);
        int i = p - 1;

        for (int j = p; j < r; j++)
        {
            var value = valueSelector(list[j]);
            if (smallestFirst ? value <= pivot : value >= pivot)
            {
                i++;
                Swap(list, i, j);
            }
        }
        i++;
        Swap(list, i, r);

        return i;
    }

    private static void Swap<T>(IList<T> list, int i, int j)
        => (list[i], list[j]) = (list[j], list[i]);
}

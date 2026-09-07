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

    /// <summary>
    /// Quickselect narrowed by a three way partition around a median of three pivot.
    /// </summary>
    /// <remarks>
    /// A single pivot partition around a fixed element degrades to a quadratic number of
    /// comparisons on the two inputs which occur the most in practice, an already ordered
    /// range and a range of repeated values. The median of three keeps the ordered range
    /// split in half, and collapsing the run equal to the pivot in one step takes the
    /// repeated values out of the recursion altogether, so both stay linear.
    /// </remarks>
    private static T QuickSelect<T, N>(IList<T> list, int kIndex, int start, int end, Func<T, N> valueSelector, bool smallestFirst)
        where N : IComparisonOperators<N, N, bool>
    {
        while (start < end)
        {
            var pivot = valueSelector(list[MedianOfThree(list, start, end, valueSelector, smallestFirst)]);

            // [start, lower) precedes the pivot, [lower, upper] equals it, (upper, end] follows it
            int lower = start;
            int index = start;
            int upper = end;
            while (index <= upper)
            {
                var value = valueSelector(list[index]);
                if (Precedes(value, pivot, smallestFirst))
                {
                    Swap(list, lower, index);
                    lower++;
                    index++;
                }
                else if (Precedes(pivot, value, smallestFirst))
                {
                    Swap(list, index, upper);
                    upper--;
                }
                else
                    index++;
            }

            // the whole run equal to the pivot is in its final place, so it is never revisited
            if (kIndex < lower)
                end = lower - 1;
            else if (kIndex > upper)
                start = upper + 1;
            else
                return list[kIndex];
        }

        return list[start];
    }

    /// <summary>
    /// Index of the median of the first, the middle and the last element of the range.
    /// </summary>
    private static int MedianOfThree<T, N>(IList<T> list, int start, int end, Func<T, N> valueSelector, bool smallestFirst)
        where N : IComparisonOperators<N, N, bool>
    {
        int middle = start + ((end - start) / 2);
        var first = valueSelector(list[start]);
        var center = valueSelector(list[middle]);
        var last = valueSelector(list[end]);

        if (Precedes(first, center, smallestFirst))
        {
            if (Precedes(center, last, smallestFirst))
                return middle;

            return Precedes(first, last, smallestFirst) ? end : start;
        }

        if (Precedes(first, last, smallestFirst))
            return start;

        return Precedes(center, last, smallestFirst) ? end : middle;
    }

    /// <summary>
    /// Whether <paramref name="value"/> comes before <paramref name="other"/> in the requested order.
    /// </summary>
    private static bool Precedes<N>(N value, N other, bool smallestFirst)
        where N : IComparisonOperators<N, N, bool>
        => smallestFirst ? value < other : value > other;

    private static void Swap<T>(IList<T> list, int i, int j)
        => (list[i], list[j]) = (list[j], list[i]);
}

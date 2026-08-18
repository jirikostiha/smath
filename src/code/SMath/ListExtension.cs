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
        => list.KthSmallestElement(k, 0, list.Count - 1, valueSelector);

    /// <summary>
    /// Finds the kth smallest element of a list.
    /// </summary>
    public static N KthSmallestElement<N>(this IList<N> list, int k)
        where N : IComparisonOperators<N, N, bool>
        => list.KthSmallestElement(k, 0, list.Count - 1, x => x);

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
        if (start >= end)
            return list[k - 1];

        // Index where the list is split by the pivot.
        int q = Partition(list, start, end, valueSelector, smallestFirst: true);

        // If the pivot lands at index k-1 it is the solution.
        if (q == k - 1)
            return list[k - 1];
        else if (q > k - 1)
            return KthSmallestElement(list, k, start, q - 1, valueSelector);
        else
            return KthSmallestElement(list, k, q + 1, end, valueSelector);
    }

    /// <summary>
    /// Finds the kth largest element of a list by a selected numeric value.
    /// </summary>
    public static T KthLargestElement<T, N>(this IList<T> list, int k, Func<T, N> valueSelector)
        where N : IComparisonOperators<N, N, bool>
        => list.KthLargestElement(k, 0, list.Count - 1, valueSelector);

    /// <summary>
    /// Finds the kth largest element of a list.
    /// </summary>
    public static N KthLargestElement<N>(this IList<N> list, int k)
        where N : IComparisonOperators<N, N, bool>
        => list.KthLargestElement(k, 0, list.Count - 1, x => x);

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
        if (start >= end)
            return list[k - 1];

        // Index where the list is split by the pivot.
        int q = Partition(list, start, end, valueSelector, smallestFirst: false);

        // If the pivot lands at index k-1 it is the solution.
        if (q == k - 1)
            return list[k - 1];
        else if (q > k - 1)
            return KthLargestElement(list, k, start, q - 1, valueSelector);
        else
            return KthLargestElement(list, k, q + 1, end, valueSelector);
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

using System.Runtime.CompilerServices;

namespace SMath.Pathfinding;

/// <summary>
/// Binary min-heap of integer nodes from a fixed range with an O(log n) decrease-key operation.
/// </summary>
/// <remarks>
/// Every node can be present at most once, therefore the heap never grows over its capacity
/// and no lazy deletion of stale entries is needed.
/// It is the priority queue of choice for graph searches over a known count of nodes.
/// <a href="https://en.wikipedia.org/wiki/Binary_heap">Wikipedia</a>
/// </remarks>
public sealed class IndexedMinHeap
{
    private const int NotInHeap = -1;

    /// <summary> Heap position to node. </summary>
    private readonly int[] _heap;

    /// <summary> Node to heap position. </summary>
    private readonly int[] _positions;

    /// <summary> Node to priority. </summary>
    private readonly int[] _priorities;

    private int _count;

    /// <param name="capacity"> Count of nodes. Valid nodes are 0 to capacity-1. </param>
    public IndexedMinHeap(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Capacity must be positive.");

        _heap = new int[capacity];
        _positions = new int[capacity];
        _priorities = new int[capacity];

        Array.Fill(_positions, NotInHeap);
    }

    /// <summary> Count of nodes the heap can hold. </summary>
    public int Capacity
        => _heap.Length;

    /// <summary> Count of nodes in the heap. </summary>
    public int Count
        => _count;

    /// <summary> Determine if the heap holds no node. </summary>
    public bool IsEmpty
        => _count == 0;

    /// <summary> Remove all nodes. It is proportional to the count of held nodes, not to the capacity. </summary>
    public void Clear()
    {
        for (var i = 0; i < _count; i++)
            _positions[_heap[i]] = NotInHeap;

        _count = 0;
    }

    /// <summary> Determine if a node is in the heap. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(int node)
        => _positions[node] != NotInHeap;

    /// <summary> Priority of a node. It is valid only for a node in the heap. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int PriorityOf(int node)
        => _priorities[node];

    /// <summary>
    /// Insert a node or decrease its priority if it is already in the heap.
    /// </summary>
    /// <returns> True if the node was inserted or its priority decreased. </returns>
    public bool Push(int node, int priority)
    {
        var position = _positions[node];

        if (position == NotInHeap)
        {
            _priorities[node] = priority;
            _heap[_count] = node;
            _positions[node] = _count;
            SiftUp(_count++);

            return true;
        }

        if (priority >= _priorities[node])
            return false;

        _priorities[node] = priority;
        SiftUp(position);

        return true;
    }

    /// <summary> Node with the lowest priority. </summary>
    public int Peek()
        => _count > 0
            ? _heap[0]
            : throw new InvalidOperationException("The heap is empty.");

    /// <summary> Remove and get the node with the lowest priority. </summary>
    public int Pop()
    {
        if (_count == 0)
            throw new InvalidOperationException("The heap is empty.");

        var root = _heap[0];
        _positions[root] = NotInHeap;

        var last = --_count;
        if (last > 0)
        {
            var node = _heap[last];
            _heap[0] = node;
            _positions[node] = 0;
            SiftDown(0);
        }

        return root;
    }

    private void SiftUp(int position)
    {
        var node = _heap[position];
        var priority = _priorities[node];

        while (position > 0)
        {
            var parentPosition = (position - 1) >> 1;
            var parent = _heap[parentPosition];
            if (_priorities[parent] <= priority)
                break;

            _heap[position] = parent;
            _positions[parent] = position;
            position = parentPosition;
        }

        _heap[position] = node;
        _positions[node] = position;
    }

    private void SiftDown(int position)
    {
        var node = _heap[position];
        var priority = _priorities[node];
        var half = _count >> 1;

        while (position < half)
        {
            var childPosition = (position << 1) + 1;
            var child = _heap[childPosition];
            var childPriority = _priorities[child];

            var rightPosition = childPosition + 1;
            if (rightPosition < _count)
            {
                var right = _heap[rightPosition];
                var rightPriority = _priorities[right];
                if (rightPriority < childPriority)
                {
                    childPosition = rightPosition;
                    child = right;
                    childPriority = rightPriority;
                }
            }

            if (childPriority >= priority)
                break;

            _heap[position] = child;
            _positions[child] = position;
            position = childPosition;
        }

        _heap[position] = node;
        _positions[node] = position;
    }
}

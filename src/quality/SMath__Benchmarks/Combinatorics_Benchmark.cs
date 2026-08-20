using BenchmarkDotNet.Attributes;
using SMath.Combinatorics;

namespace SMath.Benchmarks;

[MemoryDiagnoser]
public class Combinatorics_Benchmark
{
    private const int N = 20;
    private const int K = 3;

    [Benchmark]
    public int Combinations_Count()
    {
        return Combinations.Count(N, K);
    }

    [Benchmark]
    public int Combinations_Count_All()
    {
        return Combinations.Count(N);
    }

    [Benchmark]
    public int Combinations_Tuple2()
    {
        int count = 0;
        foreach (var _ in Combinations.Tuple2(N))
            count++;
        return count;
    }

    [Benchmark]
    public int Combinations_Tuple3()
    {
        int count = 0;
        foreach (var _ in Combinations.Tuple3(N))
            count++;
        return count;
    }

    [Benchmark]
    public int Combinations_Tuple4()
    {
        int count = 0;
        foreach (var _ in Combinations.Tuple4(N))
            count++;
        return count;
    }

    [Benchmark]
    public int Combinations_Tuples()
    {
        int count = 0;
        foreach (var _ in Combinations.Tuples(N, K))
            count++;
        return count;
    }

    [Benchmark]
    public int CombinationsWithRepetition_Count()
    {
        return CombinationsWithRepetition.Count(N, K);
    }

    [Benchmark]
    public int Permutations_Count()
    {
        return Permutations.Count(N, K);
    }

    [Benchmark]
    public int PermutationsWithRepetition_Count()
    {
        return PermutationsWithRepetition.Count(N, K);
    }
}

using BenchmarkDotNet.Attributes;
using SMath;

namespace SMath.Benchmarks;

[MemoryDiagnoser]
public class BinaryIntegerExtension_Benchmark
{
    private const int N = 10000;

    [Benchmark]
    public int HammingDistance_Int()
    {
        int sum = 0;
        for (int i = 0; i < N; i++)
            sum += (i).HammingDistanceTo(i + 12345);
        return sum;
    }

    [Benchmark]
    public long HammingDistance_Long()
    {
        long sum = 0;
        for (long i = 0; i < N; i++)
            sum += i.HammingDistanceTo(i + 123456789L);
        return sum;
    }

    [Benchmark]
    public int GCD_Int()
    {
        int sum = 0;
        for (int i = 1; i <= N; i++)
            sum += BinaryIntegerExtension.GreatestCommonDivisor(i, i + 12);
        return sum;
    }

    [Benchmark]
    public int Pow_Int()
    {
        int sum = 0;
        for (int i = 0; i < 1000; i++)
            sum += 3.Pow(i % 10);
        return sum;
    }

    [Benchmark]
    public uint ToGrayCode()
    {
        uint sum = 0;
        for (uint i = 0; i < (uint)N; i++)
            sum += i.ToGrayCode();
        return sum;
    }

    [Benchmark]
    public uint FromGrayCode()
    {
        uint sum = 0;
        for (uint i = 0; i < (uint)N; i++)
            sum += i.FromGrayCode();
        return sum;
    }
}

using BenchmarkDotNet.Attributes;
using SMath;

namespace SMath.Benchmarks;

[MemoryDiagnoser]
public class Summation_Benchmark
{
    private static readonly double[] Data = Enumerable.Range(1, 10000).Select(x => (double)x).ToArray();
    private static readonly List<double> DataList = Data.ToList();

    [Benchmark]
    public double Eval_ReadOnlySpan()
    {
        return Summation.Eval(new ReadOnlySpan<double>(Data));
    }

    [Benchmark]
    public double Eval_ReadOnlySpan_WithCount()
    {
        return Summation.Eval(new ReadOnlySpan<double>(Data), out int _);
    }

    [Benchmark]
    public double Eval_IEnumerable()
    {
        return Summation.Eval((IEnumerable<double>)DataList);
    }
}

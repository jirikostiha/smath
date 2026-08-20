using BenchmarkDotNet.Attributes;
using SMath.Statistics;

namespace SMath.Benchmarks;

[MemoryDiagnoser]
public class Variance_Benchmark
{
    private static readonly double[] Data = Enumerable.Range(1, 1000).Select(x => (double)x * 1.5).ToArray();
    private static readonly List<double> DataList = Data.ToList();

    [Benchmark]
    public double Sample_ReadOnlySpan()
    {
        return Variance.Sample.Eval(new ReadOnlySpan<double>(Data));
    }

    [Benchmark]
    public double Sample_IEnumerable()
    {
        return Variance.Sample.Eval((IEnumerable<double>)DataList);
    }

    [Benchmark]
    public double Population_ReadOnlySpan()
    {
        return Variance.Population.Eval(new ReadOnlySpan<double>(Data));
    }

    [Benchmark]
    public double Population_IEnumerable()
    {
        return Variance.Population.Eval((IEnumerable<double>)DataList);
    }
}

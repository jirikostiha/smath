using BenchmarkDotNet.Attributes;
using SMath.Statistics;

namespace SMath.Benchmarks;

[MemoryDiagnoser]
public class Covariance_Benchmark
{
    private static readonly double[] Data1 = Enumerable.Range(1, 1000).Select(x => (double)x * 1.5).ToArray();
    private static readonly double[] Data2 = Enumerable.Range(1, 1000).Select(x => (double)x * 2.3 + 5).ToArray();
    private static readonly List<double> DataList1 = Data1.ToList();
    private static readonly List<double> DataList2 = Data2.ToList();

    [Benchmark]
    public double Eval_ReadOnlySpan()
    {
        return Covariance.Eval(new ReadOnlySpan<double>(Data1), new ReadOnlySpan<double>(Data2));
    }

    [Benchmark]
    public double Eval_IEnumerable()
    {
        return Covariance.Eval(DataList1, DataList2);
    }
}

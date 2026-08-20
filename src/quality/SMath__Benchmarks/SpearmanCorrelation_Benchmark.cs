using BenchmarkDotNet.Attributes;
using SMath.Statistics;

namespace SMath.Benchmarks;

[MemoryDiagnoser]
public class SpearmanCorrelation_Benchmark
{
    private static readonly double[] Data1 = Enumerable.Range(1, 500).Select(x => (double)x * 1.5).ToArray();
    private static readonly double[] Data2 = Enumerable.Range(1, 500).Select(x => (double)x * 2.3 + (x % 5)).ToArray();
    private static readonly List<double> DataList1 = Data1.ToList();
    private static readonly List<double> DataList2 = Data2.ToList();

    [Benchmark]
    public double Eval_ReadOnlySpan()
    {
        return SpearmanRankCorrelation.Eval(new ReadOnlySpan<double>(Data1), new ReadOnlySpan<double>(Data2));
    }

    [Benchmark]
    public double Eval_IEnumerable()
    {
        return SpearmanRankCorrelation.Eval(DataList1, DataList2);
    }
}

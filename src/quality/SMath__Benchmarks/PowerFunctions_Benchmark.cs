using BenchmarkDotNet.Attributes;
using SMath.Functions1;

namespace SMath.Benchmarks;

public class PowerFunctions_Benchmark
{
    private const int N = 10000;

    [Benchmark]
    public double Power2_Eval()
    {
        double sum = 0;
        for (double i = 0; i < N; i++)
            sum += Power2.Eval(i);
        return sum;
    }

    [Benchmark]
    public double Power4_Eval()
    {
        double sum = 0;
        for (double i = 0; i < N; i++)
            sum += Power4.Eval(i * 0.01);
        return sum;
    }

    [Benchmark]
    public double Power6_Eval()
    {
        double sum = 0;
        for (double i = 0; i < N; i++)
            sum += Power6.Eval(i * 0.01);
        return sum;
    }

    [Benchmark]
    public double Power8_Eval()
    {
        double sum = 0;
        for (double i = 0; i < N; i++)
            sum += Power8.Eval(i * 0.01);
        return sum;
    }
}

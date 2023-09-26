using BenchmarkDotNet.Attributes;

namespace SMath.Functions1;

public class PT_Hypotenuse_Benchmark
{
    private const int N = 10000;

    [Benchmark]
    public double SMath()
    {
        var sum = 0d;
        for (double i = 0; i < N; i++)
            sum += PT.Hypotenuse(i, i + 3);

        return sum;
    }

    [Benchmark]
    public double Double()
    {
        var sum = 0d;
        for (double i = 0; i < N; i++)
            sum += Math.Sqrt((i * i) + ((i + 3) * (i + 3)));

        return sum;
    }
}
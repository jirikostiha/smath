using BenchmarkDotNet.Attributes;

namespace SMath.Functions1;

public class Sine_Eval_Benchmark
{
    private const int N = 10000;

    [Benchmark]
    public double SMath_Double()
    {
        var sum = 0d;
        for (double i = 0; i < N; i++)
        {
            sum += Sine.Eval(i);
            sum += Sine.Eval(i / 2d);
            sum += Sine.Eval(i / 3d);
            sum += Sine.Eval(i / 4d);
            sum += Sine.Eval(i / 5d);
            sum += Sine.Eval(i / 6d);
            sum += Sine.Eval(i / 7d);
            sum += Sine.Eval(i / 8d);
        }

        return sum;
    }

    [Benchmark]
    public double SystemMath_Double()
    {
        var sum = 0d;
        for (double i = 0; i < N; i++)
        {
            sum += Math.Sin(i);
            sum += Math.Sin(i / 2d);
            sum += Math.Sin(i / 3d);
            sum += Math.Sin(i / 4d);
            sum += Math.Sin(i / 5d);
            sum += Math.Sin(i / 6d);
            sum += Math.Sin(i / 7d);
            sum += Math.Sin(i / 8d);
        }

        return sum;
    }
}
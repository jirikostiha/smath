using BenchmarkDotNet.Attributes;
using SMath.Geometry2D;

namespace SMath.Benchmarks;

public class Line_Benchmark
{
    private const int N = 10000;

    [Benchmark]
    public double Slope_FromAngle()
    {
        double sum = 0;
        for (double i = 0; i < N; i++)
            sum += Line.Slope.FromAngle(i * 0.001);
        return sum;
    }

    [Benchmark]
    public double FromTwoPoints()
    {
        double sum = 0;
        for (double i = 0; i < N; i++)
        {
            var line = Line.FromTwoPoints((i, i + 1), (i + 2, i + 5));
            sum += line.A + line.B + line.C;
        }
        return sum;
    }

    [Benchmark]
    public double Distance_PointToLine()
    {
        double sum = 0;
        var line = (1.0, 2.0, -3.0);
        for (double i = 0; i < N; i++)
            sum += Line.And.Point.Distance.FromGeneralForm(line, (i, i + 1));
        return sum;
    }
}

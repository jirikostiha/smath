using BenchmarkDotNet.Attributes;
using SMath;

namespace SMath.Benchmarks;

public class Determinant_Benchmark
{
    private const int N = 10000;

    [Benchmark]
    public double Determinant_2x2_FromCells()
    {
        double sum = 0;
        for (double i = 1; i <= N; i++)
            sum += Determinant.FromCells(i, i + 1, i + 2, i + 3);
        return sum;
    }

    [Benchmark]
    public double Determinant_2x2_FromRows()
    {
        double sum = 0;
        for (double i = 1; i <= N; i++)
            sum += Determinant.FromRows((i, i + 1), (i + 2, i + 3));
        return sum;
    }

    [Benchmark]
    public double Determinant_3x3_FromCells()
    {
        double sum = 0;
        for (double i = 1; i <= N; i++)
            sum += Determinant.FromCells(i, i + 1, i + 2, i + 3, i + 4, i + 5, i + 6, i + 7, i + 8);
        return sum;
    }
}

using BenchmarkDotNet.Attributes;
using SMath.Geometry2D;

namespace SMath.Benchmarks;

public class Circle_Benchmark
{
    private const int N = 10000;

    [Benchmark]
    public int Point_Intersection()
    {
        int hits = 0;
        double radius = 5.0;
        for (double i = 0; i < N; i++)
        {
            if (Circle.Perimeter.And.Point.Intersection.FromRadius(radius, (3.0, 4.0)))
                hits++;
        }
        return hits;
    }

    [Benchmark]
    public double Point_FromAngle()
    {
        double sum = 0;
        double radius = 5.0;
        for (double i = 0; i < N; i++)
        {
            var pt = Circle.Perimeter.Point.FromAngle(radius, i * 0.001);
            sum += pt.X + pt.Y;
        }
        return sum;
    }
}

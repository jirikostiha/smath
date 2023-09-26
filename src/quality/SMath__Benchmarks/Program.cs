using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using SMath.Functions1;

namespace SMath;

public class Program
{
    public static void Main(string[] args)
    {
        var config = DefaultConfig.Instance.WithArtifactsPath(@"./../../../../../../benchmarks");

        //BenchmarkRunner.Run<Sine_Eval_Benchmark>(config);
        //BenchmarkRunner.Run<PT_Hypotenuse_Benchmark>(config);
        BenchmarkRunner.Run<PearsonCorrelation_Benchmark>(config);
    }
}
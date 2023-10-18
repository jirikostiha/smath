using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace SMath;

public static class Program
{
    public static void Main(string[] args)
    {
        var outputFolder = Path.GetFullPath(Directory.GetCurrentDirectory().EndsWith(typeof(Program).Assembly.GetName().Name ?? string.Empty)
            ? @"./../../../benchmarks"
            : @"./../../../../../../benchmarks");

        ManualConfig config = ManualConfig.Create(DefaultConfig.Instance.WithArtifactsPath(outputFolder))
            .WithOption(ConfigOptions.JoinSummary, true)
            .WithOption(ConfigOptions.DisableLogFile, true);

        BenchmarkRunner.Run(typeof(Program).Assembly, config);

        //BenchmarkRunner.Run<Sine_Eval_Benchmark>(config);
        //BenchmarkRunner.Run<PT_Hypotenuse_Benchmark>(config);
        //BenchmarkRunner.Run<PearsonCorrelation_Benchmark>(config);
    }
}
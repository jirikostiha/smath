﻿using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System.Runtime.CompilerServices;

namespace SMath;

public static class Program
{
    private static string GetSourceFilePathName([CallerFilePath] string? callerFilePath = null) => callerFilePath ?? string.Empty;

    public static void Main(string[] args)
    {
        Console.WriteLine("directory: " + Directory.GetCurrentDirectory());

        if (!Directory.GetCurrentDirectory().EndsWith(typeof(Program).Assembly.GetName().Name ?? string.Empty))
        {
            //HACK: workaround for executing from VS because the output path of binaries is in different folder than usual
            Directory.SetCurrentDirectory(Path.GetDirectoryName(GetSourceFilePathName()) ?? string.Empty);
            Console.WriteLine("changed to: " + Directory.GetCurrentDirectory());
        }

        ManualConfig config = ManualConfig.Create(DefaultConfig.Instance.WithArtifactsPath("./../../../benchmarks"))
            .WithOption(ConfigOptions.JoinSummary, true)
            .WithOption(ConfigOptions.DisableLogFile, true);

        BenchmarkRunner.Run(typeof(Program).Assembly, config);

        //BenchmarkRunner.Run<Sine_Eval_Benchmark>(config);
        //BenchmarkRunner.Run<PT_Hypotenuse_Benchmark>(config);
        //BenchmarkRunner.Run<PearsonCorrelation_Benchmark>(config);
    }
}
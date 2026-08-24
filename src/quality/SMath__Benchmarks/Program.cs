using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Running;
using System.Runtime.CompilerServices;

namespace SMath;

public static class Program
{
    private static string GetSourceFilePathName([CallerFilePath] string? callerFilePath = null) => callerFilePath ?? string.Empty;

    public static void Main(string[] args)
    {
        Console.WriteLine("directory: " + Directory.GetCurrentDirectory());

        if (!Directory.GetCurrentDirectory().EndsWith(typeof(Program).Assembly.GetName().Name ?? string.Empty, StringComparison.Ordinal))
        {
            //HACK: workaround for executing from VS because the output path of binaries is in different folder than usual
            Directory.SetCurrentDirectory(Path.GetDirectoryName(GetSourceFilePathName()) ?? string.Empty);
            Console.WriteLine("changed to: " + Directory.GetCurrentDirectory());
        }

        ManualConfig config = ManualConfig.Create(DefaultConfig.Instance.WithArtifactsPath("./../../../benchmarks"))
            .AddExporter(JsonExporter.FullCompressed)
            .WithOption(ConfigOptions.JoinSummary, true)
            .WithOption(ConfigOptions.DisableLogFile, true);

        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
    }
}
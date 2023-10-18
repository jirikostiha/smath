using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Collections;
using SMath.Statistics;

namespace SMath.Functions1;

[JsonExporterAttribute.Full]
[JsonExporterAttribute.FullCompressed]
[JsonExporter("-custom", indentJson: true, excludeMeasurements: true)]
public class PearsonCorrelation_Benchmark
{
    public static double[] Sequence1
        => new[] { 400.1, 225, 755, 829, 252, 952, 61, 95, 107, 603, 601, 711, 85, 791, 364, 656, 658, 634, 318, 8, 760, 201, 409,
            201, 86, 579, 44, 937, 482, 319, 878, 418, 10, 248, 138, 748, 222, 775, 710, 71, 732, 139, 142, 586, 801, 272, 690, 693,
            716, 163, 734, 523, 210, 61, 163, 83, 857, 261, 171, 289, 697, 904, 380, 478, 796, 697, 388, 814, 494, 758, 455, 227, 838,
            122, 352, 220, 768, 49, 131, 15, 90, 461, 593, 867, 801, 369, 152, 922, 32, 442, 610, 122, 871, 449, 960, 299, 475, 319, 865, 47 };

    public static double[] Sequence2
        => new[] { 526.2, 965, 870, 664, 614, 171, 65, 795, 347, 337, 839, 342, 728, 671, 801, 471, 7, 739, 978, 964, 529, 186, 113,
            510, 784, 528, 964, 66, 133, 625, 86, 219, 479, 201, 903, 879, 75, 421, 98, 79, 95, 117, 570, 391, 854, 400, 326, 617, 586,
            521, 646, 521, 616, 666, 67, 627, 337, 826, 529, 628, 178, 736, 590, 511, 132, 480, 442, 392, 388, 398, 86, 142, 314, 350,
            805, 913, 118, 538, 167, 76, 240, 208, 5, 19, 41, 129, 341, 987, 603, 215, 26, 406, 945, 241, 723, 278, 64, 639, 210, 147 };

    private const int N = 1000;

    [Benchmark]
    public void EvalEnumerableOfDouble()
    {
        int count = N;
        for (int i = 0; i < count; i++)
            PearsonCorrelation.Eval(Sequence1.AsEnumerable(), Sequence2.AsEnumerable());
    }

    [Benchmark]
    public void EvalArrayOfDouble()
    {
        int count = N;
        for (int i = 0; i < count; i++)
            PearsonCorrelation.Eval(Sequence1, Sequence2);
    }

    [Benchmark]
    public void EvalSpanOfDouble()
    {
        int count = N;
        for (int i = 0; i < count; i++)
            PearsonCorrelation.Eval(new ReadOnlySpan<double>(Sequence1), new ReadOnlySpan<double>(Sequence2));
    }

    [Benchmark]
    public void EvalArrayOfDoublePerf()
    {
        int count = N;
        for (int i = 0; i < count; i++)
            PearsonCorrelation.EvalPerf(Sequence1, Sequence2, 0);
    }
}
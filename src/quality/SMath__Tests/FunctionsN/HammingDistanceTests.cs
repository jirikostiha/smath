using System;
using System.Linq;
using Xunit;

namespace SMath.FunctionsN;

public class HammingDistanceTests
{
    private static readonly string[] SequenceA = ["a", "b", "c", "d"];
    private static readonly string[] SequenceB = ["a", "x", "c", "y"];
    private static readonly string[] Shorter = ["a", "b"];

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("count(xi != yi)", HammingDistance.PlainTextFormula);
    }

    [Fact]
    public void Eval_Span()
    {
        Assert.Equal(
            2,
            HammingDistance.Eval(new ReadOnlySpan<string>(SequenceA), new ReadOnlySpan<string>(SequenceB)));
        Assert.Equal(
            0,
            HammingDistance.Eval(new ReadOnlySpan<string>(SequenceA), new ReadOnlySpan<string>(SequenceA)));
        Assert.Equal(0, HammingDistance.Eval<string>([], []));
    }

    [Fact]
    public void Eval_SpanMatchesEnumerable()
    {
        Assert.Equal(
            HammingDistance.Eval(SequenceA.AsEnumerable(), SequenceB.AsEnumerable()),
            HammingDistance.Eval(new ReadOnlySpan<string>(SequenceA), new ReadOnlySpan<string>(SequenceB)));
    }

    [Fact]
    public void Eval_Characters()
    {
        Assert.Equal(3, HammingDistance.Eval("karolin".AsEnumerable(), "kathrin".AsEnumerable()));
    }

    [Fact]
    public void Eval_CustomComparer()
    {
        Assert.Equal(0, HammingDistance.Eval(
            new[] { "A", "B" }.AsEnumerable(),
            new[] { "a", "b" }.AsEnumerable(),
            StringComparer.OrdinalIgnoreCase));
    }

    [Fact]
    public void Eval_InconsistentLengthThrows()
    {
        Assert.Throws<ArgumentException>(() =>
            HammingDistance.Eval(new ReadOnlySpan<string>(SequenceA), new ReadOnlySpan<string>(Shorter)));
        Assert.Throws<ArgumentException>(() =>
            HammingDistance.Eval(SequenceA.AsEnumerable(), Shorter.AsEnumerable()));
    }
}

namespace SMath.Statistics;

/// <summary>
/// Running means, second moments and co-moment of paired values (Welford's online update).
/// </summary>
/// <remarks>
/// The textbook form <c>sum(ab) - sum(a) * sum(b) / n</c> subtracts two nearly equal large numbers
/// when the data sit far from zero, and loses every significant digit. The deviations from the
/// running means stay small, so this update keeps its precision independent of the offset.
/// <a href="https://en.wikipedia.org/wiki/Algorithms_for_calculating_variance#Covariance">Wikipedia</a>
/// </remarks>
internal struct CoMomentAccumulator
{
    private double _meanA;
    private double _meanB;
    private double _momentA;   // sum of (a - meanA)^2
    private double _momentB;   // sum of (b - meanB)^2
    private double _coMoment;  // sum of (a - meanA) * (b - meanB)

    public long Count { get; private set; }

    public void Add(double a, double b)
    {
        Count++;
        var deltaA = a - _meanA;
        var deltaB = b - _meanB;
        _meanA += deltaA / Count;
        _meanB += deltaB / Count;
        // the second factor is taken from the already updated mean, that is what makes
        // the update exact rather than an approximation of the two pass formula
        _momentA += deltaA * (a - _meanA);
        _momentB += deltaB * (b - _meanB);
        _coMoment += deltaA * (b - _meanB);
    }

    /// <summary> Sample covariance, undefined for less than two pairs. </summary>
    public readonly double SampleCovariance
        => Count > 1 ? _coMoment / (Count - 1) : double.NaN;

    /// <summary> Pearson correlation coefficient, undefined for less than two pairs or a constant sequence. </summary>
    public readonly double PearsonCoefficient
    {
        get
        {
            if (Count < 2)
                return double.NaN;

            var denominator = double.Sqrt(_momentA * _momentB);
            return denominator != 0 ? _coMoment / denominator : double.NaN;
        }
    }
}

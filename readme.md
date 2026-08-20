<p align="center">
  <img src="src/code/SMath/icon.png" alt="SMath" width="50"/>
</p>

# SMath

![GitHub repo size](https://img.shields.io/github/repo-size/jirikostiha/smath)
![GitHub code size](https://img.shields.io/github/languages/code-size/jirikostiha/smath)
![Nuget](https://img.shields.io/nuget/dt/SMath)  
[![Build](https://github.com/jirikostiha/smath/actions/workflows/build.yml/badge.svg)](https://github.com/jirikostiha/smath/actions/workflows/build.yml)
[![Code Analysis](https://github.com/jirikostiha/smath/actions/workflows/analyse-code.yml/badge.svg)](https://github.com/jirikostiha/smath/actions/workflows/analyse-code.yml)
[![Code Lint](https://github.com/jirikostiha/smath/actions/workflows/lint-code.yml/badge.svg)](https://github.com/jirikostiha/smath/actions/workflows/lint-code.yml)

Geometry, statistics and combinatorics for .NET, written against
[generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math).
Every formula is generic over the numeric type, so the same call site works for `double`,
`float`, `decimal` or `Half` without overloads or casting.

## Design

**Generic over the number type.** Constraints express the mathematics rather than a concrete
type: a formula needing a square root asks for `IRootFunctions<N>`, one needing only addition
asks for `INumberBase<N>`. Passing a type that cannot support the operation is a compile error.

**Static and allocation free.** There are no wrapper structs for points or vectors. Coordinates
are plain tuples such as `(N X, N Y)`, so values stay on the stack and interoperate with any
other library. All entry points are static.

**Names that read as the formula.** Types are nested by the quantity being computed and the
input it comes from, giving call sites like `Circle.Region.Area.FromRadius(r)` or
`Line.Segment.Length.FromTwoPoints(a, b)`.

**Functions describe themselves.** A function of one variable reports its parity, continuity, domain, image
and plain text formula next to `Eval` and `DerivativeEval`, so a caller can reason about the function without
hard coding its properties.

**Span overloads on the hot paths.** Statistics and grid traversal accept `ReadOnlySpan<T>`
next to `IEnumerable<T>`, avoiding enumerator allocation and interface dispatch per element.
Coordinate generators additionally offer buffer filling overloads, so a caller can size a
buffer with the matching count helper and reuse or `stackalloc` it.

## Installation

```sh
dotnet add package SMath
```

## Contents

| Area | Types |
| --- | --- |
| Geometry 1D | `LineSegment` |
| Geometry 2D | `Point2`, `Line` (ray, segment, projection, intersection), `Circle` (arc, chord, sector, segment, tangents, distance, intersection), `Ellipse`, `Hyperbola`, `Parabola`, `Polygon` (shoelace area, centroid, containment), `Triangle` (equilateral, isosceles, right), `Rectangle`, `Square`, `Rhombus`, `RegularHexagon`, `GeometricVector2` (polar/cartesian, normals, rotation, reflection, dot/cross product), `Function1Geometry` |
| Geometry 3D | `Point3` (distances, neighbors, grid traversal), `Sphere`, `SphereCap`, `BallCap`, `SphericalShell`, `Ellipsoid`, `Cuboid` |
| Statistics | `ArithmeticMean` (also weighted), `GeometricMean`, `HarmonicMean`, `QuadraticMean`, `CubicMean`, `GeneralizedMean`, `Median`, `Mode`, `Variance`, `StandardDeviation`, `Moment`, `Skewness`, `Kurtosis`, `Covariance`, `PearsonCorrelation` (cross, auto, weighted), `SpearmanRankCorrelation`, `KendallCorrelation`, `CramerCorrelation`, `Histogram` |
| Combinatorics | `Permutations`, `PermutationsWithRepetition`, `Combinations`, `CombinationsWithRepetition` |
| Sequences | `ArithmeticSequence`, `GeometricSequence`, `FibonacciSequence`, `GeneralisedFibonacciSequence`, `CollatzConjecture` |
| Series | `ArithmeticSeries`, `GeometricSeries` |
| Expansions | `BinomialCoefficient`, `PascalsTriangle` |
| Functions (1D) | `Identity`, `Sine`, `Cosine`, `Tangent`, `Cotangent`, `Power2` to `Power8`, `Root2` to `Root8`, `Reciprocal`, `Exponential`, `NaturalExponential`, `Logarithm`, `Polynomial`, `SigmoidFunction`, `BipolarSigmoid`, `LogisticFunction`, `GaussianFunction`, `SignFunction`, `StepFunction`, `UnitStepFunction` |
| Functions (2D & nD) | `AckleyFunction`, `BoothFunction`, `RastriginFunction`, `RosenbrockFunction` |
| Distances (nD) | `EuclideanDistance`, `ManhattanDistance`, `ChebyshevDistance`, `HammingDistance` |
| General & Extensions | `BinaryIntegerExtension` (Hamming distance, Gray code, GCD, Pow), `Factorial`, `Determinant`, `PythagorasTheorem`, `Summation` |

Distance metrics available on `Point2` and `Point3`: Euclidean, Manhattan, Chebyshev and
Minkowski, plus Canberra in 3D.

Both points also generate integer coordinates by metric: neighbors, the coordinates at an exact
distance, up to a distance or within a distance range, optionally limited by bounds. In 2D these
are the taxicab circle and disk and the Chebyshev ring and square, in 3D the taxicab sphere and
ball, an octahedron, and the Chebyshev shell and cube.

## Usage

Geometry, with the numeric type inferred from the arguments:

```cs
using SMath.Geometry2D;

// double and float from the same generic method
var tangentD = Circle.TangentLine.FromAngle(radius: 5d, angle: double.Pi / 4d);
var tangentF = Circle.TangentLine.FromAngle(radius: 5f, angle: float.Pi / 4f);

// tangent points from an external point, then the secant line through them
var points = Circle.TangentPoint.FromPoint(radius: 2d, (4, 4));
var secant = Line.FromTwoPoints(points.Value.Point1, points.Value.Point2);

var area = Circle.Region.Area.FromRadius(2d);
```

Statistics, over a sequence or a span:

```cs
using SMath.Statistics;

double[] values = [1, 2, 3, 4, 5];

var mean = ArithmeticMean.Eval(values);
var variance = Variance.Sample.Eval(values);
var deviation = StandardDeviation.Sample.Eval(values);

double[] other = [2, 1, 4, 3, 5];
var correlation = PearsonCorrelation.Eval<double>(values, other);

// pairs counted in proportion to their weight, the weights need not sum to one
double[] weights = [1, 1, 2, 3, 5];
var weightedCorrelation = PearsonCorrelation.Weighted.Eval<double>(values, other, weights);
```

Counting and enumerating, with the counts evaluated so that only the result has to fit
into the number type:

```cs
using SMath.Combinatorics;
using SMath.Expansions;

var hands = Combinations.Count(52, 5);           // 2598960
var wide = BinomialCoefficient.Eval(60L, 30L);   // the factorial form would overflow a long
var draws = PermutationsWithRepetition.Count(6, 3);

foreach (var indices in Combinations.Tuples(5, 3))
{
    // every 3 element combination of the indices 0 to 4, in lexicographic order
}
```

Sequences and their partial sums, over a sequence type of your choice:

```cs
using SMath.Sequences;
using SMath.Series;

var fibonacci = FibonacciSequence.Terms<long>(10);
var term = GeometricSequence.Term(initial: 8d, ratio: 0.5, n: 4);
var sum = GeometricSeries.Term(initial: 8d, ratio: 0.5, n: 4);
var limit = GeometricSeries.Limit(initial: 8d, ratio: 0.5);

Span<double> buffer = stackalloc double[16];
var count = ArithmeticSequence.Terms(initial: 1d, difference: 2d, count: 16, destination: buffer);
```

Grid traversal without allocating, using a count helper to size the buffer:

```cs
using SMath.Geometry2D;

var center = (X: 0, Y: 0);
const int radius = 3;

Span<(int X, int Y)> buffer = stackalloc (int X, int Y)[Point2.ManhattanDiskCount(radius)];
var count = Point2.CoordinatesUpToManhattanDistance(center, radius, buffer);

foreach (var coordinate in buffer[..count])
{
    // visit each coordinate within the taxicab disk
}
```

The same in three dimensions, walking a solid octahedron of voxels:

```cs
using SMath.Geometry3D;

var origin = (X: 0, Y: 0, Z: 0);

Span<(int X, int Y, int Z)> voxels = stackalloc (int X, int Y, int Z)[Point3.ManhattanBallCount(radius)];
var voxelCount = Point3.CoordinatesUpToManhattanDistance(origin, radius, voxels);

// only the shell, or the 26 neighbors of a voxel
var shell = Point3.CoordinatesAtChebyshevDistance(origin, radius);
var neighbors = Point3.AxialNeighbors(origin)
    .Concat(Point3.EdgeNeighbors(origin))
    .Concat(Point3.DiagonalNeighbors(origin));
```

Cross correlation over a range of lags, writing into a caller owned buffer:

```cs
using SMath.Statistics;

int[] lags = [-2, -1, 0, 1, 2];
var coefficients = new double[lags.Length];

PearsonCorrelation.Cross.Eval<double, int>(values, other, lags, coefficients);
```

## Contributing

Ideas, bug reports and pull requests are welcome. Open an
[issue](https://github.com/jirikostiha/smath/issues/new/choose) to propose a change, or send a
[pull request](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/creating-a-pull-request)
directly.

## License

Project is under [MIT](./LICENSE) license.

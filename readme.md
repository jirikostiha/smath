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

Geometry and statistics for .NET, written against
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

**Span overloads on the hot paths.** Statistics and grid traversal accept `ReadOnlySpan<T>`
next to `IEnumerable<T>`, avoiding enumerator allocation and interface dispatch per element.
Coordinate generators additionally offer buffer filling overloads, so a caller can size a
buffer with the matching count helper and reuse or `stackalloc` it.

## Contents

| Area | Types |
| --- | --- |
| Geometry 2D | `Point2`, `Line` (ray, segment, projection, intersection), `Circle` (arc, chord, sector, segment, tangents), `Ellipse`, `Rectangle`, `GeometricVector2` (polar/cartesian, normals, reflection, dot and cross product), `Function1Geometry` (tangent and normal lines) |
| Geometry 3D | `Point3`, `Sphere` |
| Statistics | `ArithmeticMean`, `Variance`, `StandardDeviation`, `Covariance`, `PearsonCorrelation` (cross and auto correlation), `Histogram` |
| General | `Summation`, `Product`, `Determinant`, `PythagorasTheorem`, single variable functions (`Sine`, `Cosine`, `Tangent`, `Cotangent`, `Power2`, `Power3`, `Identity`) |

Distance metrics available on `Point2` and `Point3`: Euclidean, Manhattan, Chebyshev and
Minkowski.

## Setup

```xml
<PackageReference Include="SMath" Version="X.X.X" />
```

Replace `X.X.X` with the current version from [NuGet](https://www.nuget.org/packages/SMath).
The package targets `net7.0` and runs on any newer runtime.

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

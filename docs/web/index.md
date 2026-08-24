# SMath Documentation

Geometry, statistics and combinatorics for .NET, written against [generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math).
Every formula is generic over the numeric type, so the same call site works for `double`, `float`, `decimal` or `Half` without overloads or casting.

---

## Design Principles

- **Generic over the number type:** Constraints express the mathematics rather than a concrete type: a formula needing a square root asks for `IRootFunctions<N>`, one needing only addition asks for `INumberBase<N>`. Passing a type that cannot support the operation is a compile error.
- **Static and allocation free:** There are no wrapper structs for points or vectors. Coordinates are plain tuples such as `(N X, N Y)`, so values stay on the stack and interoperate with any other library. All entry points are static.
- **Names that read as the formula:** Types are nested by the quantity being computed and the input it comes from, giving call sites like `Circle.Region.Area.FromRadius(r)` or `Line.Segment.Length.FromTwoPoints(a, b)`.
- **Functions describe themselves:** A function of one variable reports its parity, continuity, domain, image and plain text formula next to `Eval` and `DerivativeEval`, so a caller can reason about the function without hard coding its properties.
- **Span overloads on the hot paths:** Statistics and grid traversal accept `ReadOnlySpan<T>` next to `IEnumerable<T>`, avoiding enumerator allocation and interface dispatch per element. Coordinate generators additionally offer buffer filling overloads, so a caller can size a buffer with the matching count helper and reuse or `stackalloc` it.

---

## Installation

Install the NuGet package:

```shell
dotnet add package SMath
```

---

## Quick Navigation

- [API Reference](api/index.md) - Full reference documentation for all namespaces, types, and mathematical formulas.
- [GitHub Repository](https://github.com/jirikostiha/smath) - Source code, issues, and discussions.

---

## Overview of Modules

| Area | Types & Capabilities |
| :--- | :--- |
| **Geometry 1D** | `LineSegment` |
| **Geometry 2D** | `Point2`, `Line` (ray, segment, projection, intersection), `Circle` (arc, chord, sector, segment, tangents, distance, intersection), `Ellipse`, `Hyperbola`, `Parabola`, `Polygon` (shoelace area, centroid, containment), `Triangle`, `Rectangle`, `Square`, `Rhombus`, `RegularHexagon`, `GeometricVector2` (polar/cartesian, normals, rotation, reflection, dot/cross product), `Function1Geometry` |
| **Geometry 3D** | `Point3` (distances, neighbors, grid traversal), `Sphere`, `SphereCap`, `BallCap`, `SphericalShell`, `Ellipsoid`, `Cuboid` |
| **Statistics** | `ArithmeticMean` (also weighted), `GeometricMean`, `HarmonicMean`, `QuadraticMean`, `CubicMean`, `GeneralizedMean`, `Median`, `Mode`, `Variance`, `StandardDeviation`, `Moment`, `Skewness`, `Kurtosis`, `Covariance`, `PearsonCorrelation` (cross, auto, weighted), `SpearmanRankCorrelation`, `KendallCorrelation`, `CramerCorrelation`, `Histogram` |
| **Combinatorics** | `Permutations`, `PermutationsWithRepetition`, `Combinations`, `CombinationsWithRepetition` |
| **Sequences** | `ArithmeticSequence`, `GeometricSequence`, `FibonacciSequence`, `GeneralisedFibonacciSequence`, `CollatzConjecture` |
| **Series** | `ArithmeticSeries`, `GeometricSeries` |
| **Expansions** | `BinomialCoefficient`, `PascalsTriangle` |
| **Functions (1D)** | `Identity`, `Sine`, `Cosine`, `Tangent`, `Cotangent`, `Power2` to `Power8`, `Root2` to `Root8`, `Reciprocal`, `Exponential`, `NaturalExponential`, `Logarithm`, `Polynomial`, `SigmoidFunction`, `BipolarSigmoid`, `LogisticFunction`, `GaussianFunction`, `SignFunction`, `StepFunction`, `UnitStepFunction` |
| **Functions (2D & nD)** | `AckleyFunction`, `BoothFunction`, `RastriginFunction`, `RosenbrockFunction` |
| **Distances (nD)** | `EuclideanDistance`, `ManhattanDistance`, `ChebyshevDistance`, `HammingDistance` |
| **General & Extensions** | `BinaryIntegerExtension` (Hamming distance, Gray code, GCD, Pow), `Factorial`, `Determinant`, `PythagorasTheorem`, `Summation` |

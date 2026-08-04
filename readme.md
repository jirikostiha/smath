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


## Overview

SMath is a math library built on .NET 7 [generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math), offering a comprehensive set of static types for working with 2D geometry, grids, pathfinding and statistics.  


## Features

### Generic Math Capabilities  

Leverages .NET 7's new generic math features, allowing type-safe mathematical operations on various numeric types.

### Geometry (2D)

Handle 2D geometric computations with ease. Available types and operations include:

- **Point**  
  - Euclidean distance, Manhattan distance, Chebyshev distance, Minkowski distance  
- **Line**  
  - Ray, Line Segment  
  - Operations: Projection, Intersection, Inclusion  
- **Circle**  
  - Arc, Chord, Sector, Segment  
  - Operations: Perimeter, Region, Tangent Points, Inclusion  
- **Rectangles**  
  - Vertices  
  - Operations: Perimeter  
- **Aabb** (axis aligned bounding box)  
  - Construction: FromCenter, FromSize, FromPoints, Union, Intersection, Swept  
  - Queries: Contains, Overlaps, ClosestPoint, Distance, Penetration  
  - Continuous collision: RayIntersection, Sweep (slab method)  
- **Interpolation**  
  - Linear, Bilinear, Angular (shortest way, wrapping)  
  - Easing: SmoothStep, SmootherStep, Quadratic, Sine, ExponentialDecay (frame rate independent)  
  - Curves: quadratic and cubic Bezier, Catmull-Rom  
- **Steering** behaviors of an agent  
  - Seek, Flee, Arrive, Pursue, Evade  
  - Flocking: Separate, Cohere, Align  
  - Steer, ClampSpeed  

### Grid (2D)

Row-major grid of cells addressed by a struct predicate, so a map of any layout plugs in
without an allocation or a virtual call:

- **Grid**  
  - Index, Cell, Contains, Clamp, Wrap, neighborhood and ring enumeration  
- **GridDirection**  
  - Eight directions, step, opposite, turn, direction of a delta  
- **Raster**  
  - Line (Bresenham), ThickLine, Circle, Disc  
  - Line of sight: IsVisible, Cast  
  - Conversion of cells and points of a grid of a given cell size  

### Pathfinding

- **GridPathfinder** - A* and Dijkstra over a grid.
  Every buffer is allocated once per instance and reused, a repeated search does not allocate.
  Cells are stamped by a search generation instead of being cleared,
  so a search costs the count of the visited cells, not the size of the grid.
- **GridFloodFill** - breadth first distances, connectivity test and region labeling.
- **FlowField** - one field of directions leading many agents to the closest target in constant time per step.
- **Path2** - Simplify, PullString, Smooth, Length, PointAt of a found path.


### Statistical Analysis

Perform basic statistical calculations:

- Correlation
- Variance
- Covariance
- Standard Deviation
- Histograms


## Setup

Add nuget package to project.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="SMath" Version="X.X.X" />
  </ItemGroup>
</Project>
```
Replace 'X.X.X' with the appropriate version from [NuGet](https://www.nuget.org/packages/SMath).

## Usage

Here are some basic usage examples to get started with SMath:

### Geometry: Working with Circles and Lines

```cs
// Using double precision
var line1 = Circle.TangentLine.FromAngle(radius: 5d, angle: Math.PI / 4d);

// Using float precision
var line2 = Circle.TangentLine.FromAngle(radius: 5f, angle: MathF.PI / 4f);

// Find tangent points from a circle
var tangentPoints = Circle.TangentPoint.FromPoint(radius: 2d, (4, 4));
var secantLine = Line.FromTwoPoints(tangentPoints.Value.Point1, tangentPoints.Value.Point2);
```

### Pathfinding over a grid

```cs
using SMath.Grid2D;
using SMath.Pathfinding;

// any map plugs in as a struct predicate, no allocation and no virtual call
readonly struct Map : IGridPredicate
{
    private readonly bool[] _walls;
    private readonly int _width;

    public Map(bool[] walls, int width) => (_walls, _width) = (walls, width);

    public bool Test(int x, int y) => !_walls[(y * _width) + x];
}

var map = new Map(walls, width);
var pathfinder = new GridPathfinder(width, height); // allocate once, reuse forever

if (pathfinder.FindPath(start: (0, 0), target: (99, 99), map, GridConnectivity.Diagonal))
{
    Span<(int X, int Y)> path = stackalloc (int X, int Y)[pathfinder.PathCellCount];
    var count = pathfinder.GetPath(path);

    // drop the staircase artifacts of a grid search
    count = Path2.PullString(path[..count], new GridNegatedPredicate<Map>(map));
}

// many agents heading to one target are cheaper over a flow field
pathfinder.Fill(target, new GridPassabilityCost<Map>(map), distances);
FlowField.FromDistances(distances, width, height, directions);
var step = FlowField.Next(directions, width, agentCell);
```

### Statistical Calculations

```cs
using SMath.Statistics;

// Example of calculating variance
var values = new double[] { 1, 2, 3, 4, 5 };
double sampleVariance = Variance.Sample.Eval(values);
double populationVariance = Variance.Population.Eval(values);
Console.WriteLine($"Variance: {sampleVariance}");
```

## Contributing

Any ideas, contributions and bug reports are welcome!  

For new idea create an [issue](https://github.com/jirikostiha/smath/issues/new/choose).  
For bug report create an [issue](https://github.com/jirikostiha/smath/issues/new/choose).  
For contribution create a [pull request](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/creating-a-pull-request).  

## License

Project is under [MIT](./LICENSE) license.

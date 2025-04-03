<p align="center">
  <img src="\src\code\SMath\icon.png"" alt="SMath" width="50"/>
</p>

# SMath  

![GitHub repo size](https://img.shields.io/github/repo-size/jirikostiha/smath)
![GitHub code size](https://img.shields.io/github/languages/code-size/jirikostiha/smath)
![Nuget](https://img.shields.io/nuget/dt/SMath)  
[![Build](https://github.com/jirikostiha/smath/actions/workflows/build.yml/badge.svg)](https://github.com/jirikostiha/smath/actions/workflows/build.yml)
[![Code Analysis](https://github.com/jirikostiha/smath/actions/workflows/analyse-code.yml/badge.svg)](https://github.com/jirikostiha/smath/actions/workflows/analyse-code.yml)
[![Code Lint](https://github.com/jirikostiha/smath/actions/workflows/lint-code.yml/badge.svg)](https://github.com/jirikostiha/smath/actions/workflows/lint-code.yml)


## Overview

SMath is a math library built on .NET 7 [generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math), offering a comprehensive set of static types for working with 2D geometry and statistics.  


## Features

### Generic Math Capabilities  

Leverages .NET 7’s new generic math features, allowing type-safe mathematical operations on various numeric types.

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

### Statistical Calculations

```cs
// Example of calculating variance
var values = new double[] { 1, 2, 3, 4, 5 };
double variance = Statistics.Variance(values);
Console.WriteLine($"Variance: {variance}");
```

## Contributing

Any ideas, contributions and bug reports are welcome!  

For new idea create an [issue](https://github.com/jirikostiha/smath/issues/new/choose).  
For bug report create an [issue](https://github.com/jirikostiha/smath/issues/new/choose).  
For contribution create a [pull request](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/creating-a-pull-request).  

## License

Project is under [MIT](./LICENSE) license.

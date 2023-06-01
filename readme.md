# SMath  

![GitHub repo size](https://img.shields.io/github/repo-size/jirikostiha/smath)
![GitHub code size](https://img.shields.io/github/languages/code-size/jirikostiha/smath)
[![NuGet Downloads](https://img.shields.io/nuget/dt/SMath.svg)](https://www.nuget.org/packages/SMath/)
[![Build](https://github.com/jirikostiha/smath/actions/workflows/build.yml/badge.svg)](https://github.com/jirikostiha/smath/actions/workflows/build.yml)
[![Code Analysis](https://github.com/jirikostiha/smath/actions/workflows/analyse-code.yml/badge.svg)](https://github.com/jirikostiha/smath/actions/workflows/analyse-code.yml)
[![Code Lint](https://github.com/jirikostiha/smath/actions/workflows/lint-code.yml/badge.svg)](https://github.com/jirikostiha/smath/actions/workflows/lint-code.yml)

<pre>
SMath is
  generic math library.
  set of static types.
  a different approach to math.
</pre>

## Features

The library is based on [Generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math)
available since .NET 7.  

Currently includes:  
* 2D geometry  
* One-parametric functions

## Setup

Add nuget package to your project.

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
Replace 'X.X.X' with a specific version.

## Usage

```cs
// double numbers
var line1 = Circle.TangentLine.FromAngle(radius: 5d, angle: double.Pi / 4d);

// float numbers
var line2 = Circle.TangentLine.FromAngle(radius: 5f, angle: float.Pi / 4f);
```

```cs
var tangentPoints = Circle.TangentPoint.FromPoint(radius: 2d, (4, 4));
var secantLine = Line.FromTwoPoints(tangentPoints.Value.Point1, tangentPoints.Value.Point2);
```

## Contributing

Any ideas, contributions and bug reports are welcome!  

For new idea create an [issue](https://github.com/jirikostiha/smath/issues/new/choose).  
For bug report create an [issue](https://github.com/jirikostiha/smath/issues/new/choose).  
For contribution create a [pull request](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/creating-a-pull-request).  

## License

Project is under [MIT](./LICENSE) license.

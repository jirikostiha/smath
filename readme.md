# SMath  

![GitHub repo size](https://img.shields.io/github/repo-size/jirikostiha/smath)
![GitHub code size](https://img.shields.io/github/languages/code-size/jirikostiha/smath)  
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

2D geometry  
One parametric functions

## Setup

Add nuget package to your project.
Project file (`.csproj`) should look like this:

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
var line = Circle.TangentLine.FromAngle(radius, angle)
```

## Contributing

Any ideas, contributions and bug reports are welcome!  

For new idea create an [issue](https://github.com/jirikostiha/smath/issues/new/choose).  
For bug report create an [issue](https://github.com/jirikostiha/smath/issues/new/choose).  
For contribution create a [pull request](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/creating-a-pull-request).  

## License

Project is under [MIT](./LICENSE) license.

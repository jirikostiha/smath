---
name: verify
description: Verify a change of SMath at its surface, the NuGet package, by a sample app that references the packed package.
---

# Verify SMath through the package

SMath is a library, its surface is the package boundary, not `src/`.

1. Pack with a unique suffix, so no cached package is reused:
   `dotnet pack src/code/SMath -c Release -o <scratch>/feed -p:VersionSuffix=v<HHmmss>`
2. In `<scratch>/app` keep a `nuget.config` with `<clear />`, the local feed mapped to
   `SMath` (and `SPhysics`) and nuget.org for the rest, and a net8.0 console project with
   `<PackageReference Include="SMath" Version="$(SMathVersion)" />`.
3. Run `dotnet run -c Release -p:SMathVersion=1.6.0-v<HHmmss>` and check in
   `obj/project.assets.json` that the packed version was restored.
4. Drive the changed public API from `Program.cs`, then probe the edges (zero, NaN,
   int.MinValue, grid boundary, corner cases).

Gotchas: SPhysics references the released SMath package, so pack it in the same feed with
`-p:Version=<x>-v<HHmmss>` to check that the SPhysics binary runs against the new SMath.

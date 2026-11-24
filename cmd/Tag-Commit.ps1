#Requires -Version 7

<#
.SYNOPSIS
Tags the current commit with the product version.

.DESCRIPTION
Reads the product version, creates an annotated tag such as v1.4.2 or
v1.4.2-rc for the current commit and pushes the tags. The file carrying the
version is discovered automatically; see Get-VersionFile in Common.ps1.

.PARAMETER Prefix
The version to use in the tag, overriding the one in the version file. A caller that
has just set the version passes it, so -WhatIf, which leaves the file alone, reports
the tag that would be made. It is normalised to three parts, so 2.0 tags v2.0.0.

.PARAMETER Suffix
The stage suffix to use in the tag, overriding the one in the version file.

.PARAMETER VersionFile
An explicit path to the version file, overriding discovery.

.PARAMETER NoPush
Creates the tag locally without pushing it.

.EXAMPLE
.\Tag-Commit.ps1
Tags the current commit with the product version and pushes the tag.

.EXAMPLE
.\Tag-Commit.ps1 -Suffix rc
Tags the current commit as a release candidate.
#>
<#---
name: Tag-Commit
kind: cmd
description: Tags the current commit with the product version and pushes the tag. Use when publishing a release.
profiles: [dotnet]
version: 2.2.2
---#>
[CmdletBinding(SupportsShouldProcess)]
param(
    [version] $Prefix,
    [string] $Suffix,
    [string] $VersionFile,
    [switch] $NoPush
)

. (Join-Path $PSScriptRoot "Common.ps1")

$current = Get-ProductVersion -Path $VersionFile
# git runs against this script's repository, not whichever one the caller stands in.
$root = Get-RepositoryRoot

# The tag marks HEAD, so HEAD must carry the version: an uncommitted version file would tag the previous commit.
$versionFileChanges = git -C $root status --porcelain -- $current.Path
if ($LASTEXITCODE -ne 0) { throw ("git status failed (exit code: {0})." -f $LASTEXITCODE) }
if ($versionFileChanges) {
    throw ("The version file has uncommitted changes; commit them first: {0}" -f $current.Path)
}

$tagPrefix = if ($PSBoundParameters.ContainsKey('Prefix')) { ConvertTo-ThreePartVersion $Prefix } else { $current.Prefix }
$tagSuffix = if ($PSBoundParameters.ContainsKey('Suffix')) { $Suffix } else { $current.Suffix }
$tag = if ($tagSuffix) { "v$tagPrefix-$tagSuffix" } else { "v$tagPrefix" }

if ($PSCmdlet.ShouldProcess($current.Path, "Publish tag $tag")) {
    Write-Host ("Tagging commit {0} using file: {1}" -f $tag, (Get-Hyperlink -Path $current.Path)) -ForegroundColor Cyan

    git -C $root tag $tag
    if ($LASTEXITCODE -ne 0) { throw ("git tag failed (exit code: {0})." -f $LASTEXITCODE) }

    if (-not $NoPush) {
        # Only the new tag: --tags would also publish every unrelated local-only tag.
        git -C $root push origin "refs/tags/$tag"
        if ($LASTEXITCODE -ne 0) { throw ("git push failed (exit code: {0})." -f $LASTEXITCODE) }
    }
}

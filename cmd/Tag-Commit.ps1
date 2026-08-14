<#
.SYNOPSIS
Tags the current commit with the product version.

.DESCRIPTION
Reads the version from product_version.props and creates a git tag for the current commit, then pushes all tags to the remote repository.

.PARAMETER VersionFile
The path to the version properties file. Defaults to "..\product_version.props".

.PARAMETER Suffix
The version suffix to use for the tag. If provided, it overrides the version suffix in the properties file.

.EXAMPLE
.\Tag-Commit.ps1
Tags the current commit with the product version.
#>
[CmdletBinding(SupportsShouldProcess)]
param(
    [string] $VersionFile = "..\product_version.props",
    [string] $Suffix
)

. (Join-Path $PSScriptRoot "Common.ps1")

$scriptName = "tag commit"
$scriptVersion = "1.1"
$category = @("git")

if (-not (Test-Path $VersionFile)) {
    throw "Version file not found: $VersionFile"
}

$fullPath = (Resolve-Path $VersionFile).Path
$fileLink = Get-Hyperlink -Path $fullPath -Text $VersionFile

[xml]$versionFileXml = Get-Content $VersionFile -Raw
$versionPrefix = $versionFileXml.Project.PropertyGroup.VersionPrefix
$versionSuffix = $versionFileXml.Project.PropertyGroup.VersionSuffix

if ($PSBoundParameters.ContainsKey('Suffix')) {
    $versionSuffix = $Suffix
}

if (-not [string]::IsNullOrEmpty($versionSuffix)) {
    $tag = "v$versionPrefix-$versionSuffix"
}
else {
    $tag = "v$versionPrefix"
}

Write-Verbose "Publishing tag $tag"
if ($PSCmdlet.ShouldProcess($VersionFile, "Publish tag $tag")) {
    Write-Host "Tagging commit $tag using file: $fileLink" -ForegroundColor Cyan
    git tag $tag
    git push origin --tags
}

<#
.SYNOPSIS
Bumps the product version.

.DESCRIPTION
Bumps either the Build or Minor version in product_version.props.

.PARAMETER Minor
If set, bumps the minor version and resets build to 0. Otherwise, bumps the build version.

.PARAMETER VersionFile
The path to the version properties file. Defaults to "..\product_version.props".

.EXAMPLE
.\Bump-Version.ps1
Bumps the build version (e.g., 1.4.1 -> 1.4.2).

.EXAMPLE
.\Bump-Version.ps1 -Minor
Bumps the minor version (e.g., 1.4.1 -> 1.5.0).
#>
[CmdletBinding(SupportsShouldProcess)]
param(
    [switch] $Minor,
    [string] $VersionFile = "..\product_version.props"
)

. (Join-Path $PSScriptRoot "Common.ps1")

$scriptName = "bump product version"
$scriptVersion = "1.5"
$category = @("dotnet")

if (-not (Test-Path $VersionFile)) {
    $errorMsg = ("Version file not found: {0}" -f $VersionFile)
    throw $errorMsg
}

$fullPath = (Resolve-Path $VersionFile).Path
$fileLink = Get-Hyperlink -Path $fullPath -Text $VersionFile

[xml]$versionFileXml = Get-Content $VersionFile -Raw
$versionText = $versionFileXml.Project.PropertyGroup.VersionPrefix

try {
    $oldVersion = [version]$versionText
} catch {
    $errorMsg = ("Invalid version format in file: '{0}'" -f $versionText)
    throw $errorMsg
}

$newVersion = if ($Minor) {
    [version]"$($oldVersion.Major).$($oldVersion.Minor + 1).0"
} else {
    [version]"$($oldVersion.Major).$($oldVersion.Minor).$($oldVersion.Build + 1)"
}

Write-Host "Bumping file: $fileLink" -ForegroundColor Cyan
Write-Host "$oldVersion -> $newVersion" -ForegroundColor Yellow

if ($PSCmdlet.ShouldProcess($VersionFile, "Bump version from $oldVersion to $newVersion")) {
    $versionFileXml.Project.PropertyGroup.VersionPrefix = $newVersion.ToString()
    $versionFileXml.Save($fullPath)
    Write-Host "Version bumped successfully." -ForegroundColor Green
}

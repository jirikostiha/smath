<#
.SYNOPSIS
Commits and pushes the current version.

.DESCRIPTION
Reads the version from product_version.props, stages the file, commits it, and pushes to the remote repository.

.PARAMETER VersionFile
The path to the version properties file. Defaults to "..\product_version.props".

.EXAMPLE
.\Commit-Version.ps1
Commits and pushes the current version.
#>
[CmdletBinding(SupportsShouldProcess)]
param(
    [string] $VersionFile = (Join-Path $PSScriptRoot ".." "product_version.props")
)

. (Join-Path $PSScriptRoot "Common.ps1")

$scriptDescription = "commit product version file"
$scriptVersion = "1.2"
$category = @("dotnet", "git")

$fullPath = (Get-Item $VersionFile).FullName
$fileLink = Get-Hyperlink -Path $fullPath -Text $VersionFile

[xml]$versionFileXml = Get-Content $VersionFile
$versionText = $versionFileXml.Project.PropertyGroup.VersionPrefix
try {
    $version = [version]$versionText
} catch {
    $errorMsg = ("Invalid VersionPrefix format: '{0}'" -f $versionText)
    throw $errorMsg
}

Write-Verbose "Publishing product version $version"

if ($PSCmdlet.ShouldProcess($VersionFile, "Commit version $version")) {
    Write-Host "Committing version $version using file: $fileLink" -ForegroundColor Cyan
    git add $VersionFile
    git commit -m "product: bump to $version"
    git push
}

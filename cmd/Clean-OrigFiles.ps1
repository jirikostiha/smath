<#
.SYNOPSIS
Cleans Git merge artifacts.

.DESCRIPTION
Searches for and removes all files with the .orig extension recursively under the specified root directory.

.PARAMETER Root
The root directory to start searching from. Defaults to the parent directory.

.EXAMPLE
.\Clean-OrigFiles.ps1
Cleans all .orig files in the repository.

.EXAMPLE
.\Clean-OrigFiles.ps1 -Root "C:\Projects\MyRepo"
Cleans .orig files in a specific path.
#>
[CmdletBinding(SupportsShouldProcess)]
param(
    [string] $Root = (Get-Item .).Parent.FullName
)

$scriptName = "delete all *.orig files"
$scriptVersion = "1.1"
$category = @("dotnet")

Get-ChildItem -Path $Root -Recurse -File -Filter *.orig -ErrorAction SilentlyContinue | ForEach-Object {
    if ($PSCmdlet.ShouldProcess($_.FullName, "Remove file")) {
        Write-Host "Removing file: $($_.FullName)" -ForegroundColor Yellow
        Remove-Item -Force -Path $_.FullName
    }
}

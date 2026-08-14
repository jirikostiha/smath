<#
.SYNOPSIS
Cleans build artifacts.

.DESCRIPTION
Searches for and removes 'obj', 'bin', and 'asm' folders recursively under the specified root directory.

.PARAMETER Root
The root directory to start searching from. Defaults to the parent directory.

.EXAMPLE
.\Clean-Binaries.ps1
Cleans all build folders in the repository.

.EXAMPLE
.\Clean-Binaries.ps1 -Root "C:\Projects\MyRepo"
Cleans build folders in a specific path.
#>
[CmdletBinding(SupportsShouldProcess)]
param(
    [string] $Root = (Get-Item .).Parent.FullName
)

$scriptName = "delete obj, bin, artf folders"
$scriptVersion = "1.1.1"
$category = @("dotnet")

$targets = @("obj", "bin", "artf")

foreach ($name in $targets) {
    Write-Host "Searching for '$name' folders under $Root..." -ForegroundColor Magenta

    Get-ChildItem -Path $Root -Recurse -Directory -Filter $name -ErrorAction SilentlyContinue | ForEach-Object {
        if ($PSCmdlet.ShouldProcess($_.FullName, "Remove folder")) {
            Write-Host "Removing folder: $($_.FullName)" -ForegroundColor Yellow
            Remove-Item -Force -Recurse -Path $_.FullName
        }
    }
}

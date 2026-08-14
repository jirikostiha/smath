# Shared PowerShell logic for repository scripts

function Get-Hyperlink($Path, $Text = $Path) {
    $urlPath = $Path.Replace('\', '/')
    return "$([char]27)]8;;file:///$urlPath$([char]27)\$Text$([char]27)]8;;$([char]27)\"
}
param (
    [string]$modName,
    [string]$modVersion
)

# Configuration
$packageDir = '.\Package'
$artifactDir = '.\bin\Package\netstandard2.1'

# Make sure our CWD is where the script lives
Set-Location $PSScriptRoot

Write-Host ('Packaging {0} v{1}' -f $modName, $modVersion)

# Create the package structure
$bepInExDir = '{0}\BepInEx' -f $packageDir
$pluginsDir = '{0}\plugins\{1}' -f $bepInExDir, $modName
$null = mkdir $pluginsDir -ea 0

# Copy required files to the package structure
$artifactPath = ('{0}\{1}.dll' -f $artifactDir, $modName)
Copy-Item $artifactPath -Destination $pluginsDir
$assetPath = ('{0}\Assets\*' -f $artifactDir)
Copy-Item $assetPath -Destination $pluginsDir -Recurse

# This is specific to Sense, VS doesn't want to copy empty dirs so create one
$soundsDir = ('{0}\sounds\' -f $pluginsDir)
mkdir $soundsDir

# Create the archive
$archivePath = '{0}\{1}-{2}.7z' -f $packageDir, $modName, $modVersion
if (Test-Path $archivePath)
{
    Remove-Item $archivePath
}
7z a $archivePath $bepInExDir

Write-Host ('Mod packaging complete')
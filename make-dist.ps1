# Builds a portable, self-contained distribution of Youtube Zenni Tool:
#   - Release build
#   - dist\Youtube Zenni Tool\  (complete runnable folder)
#   - dist\Youtube Zenni Tool.zip
#
# Run from the project root:  .\make-dist.ps1
# Requires: dotnet SDK 8 (or newer) and the FFmpeg binaries staged in .\bin\x64
# (run .\get-deps.ps1 once first if bin\x64 is missing).

$ErrorActionPreference = "Stop"

$root = $PSScriptRoot
if (-not (Test-Path -LiteralPath "$root\YoutubeZenniTool.csproj")) {
    throw "Run this script from the project root (where YoutubeZenniTool.csproj lives)."
}

$exeName = "Youtube Zenni Tool"

# 1) Release publish
Write-Host "Building Release..."
dotnet publish "$root\YoutubeZenniTool.csproj" -c Release -v minimal
if ($LASTEXITCODE -ne 0) { throw "dotnet publish failed." }

$publishDir = "$root\bin\Release\net461\publish"
if (-not (Test-Path -LiteralPath $publishDir)) { throw "Publish output not found: $publishDir" }

# 2) Assemble dist folder
$distRoot = "$root\dist"
$appDir = Join-Path $distRoot $exeName
if (Test-Path -LiteralPath $distRoot) { Remove-Item -LiteralPath $distRoot -Recurse -Force }
New-Item -ItemType Directory -Path $appDir -Force | Out-Null

Write-Host "Copying application files..."
Get-ChildItem -LiteralPath $publishDir -File | Copy-Item -Destination $appDir

Write-Host "Copying ffmpeg presets..."
Copy-Item -LiteralPath "$root\ffmpeg code" -Destination $appDir -Recurse -Force

Write-Host "Copying ffmpeg binaries..."
$binX64 = "$root\bin\x64"
if (-not (Test-Path -LiteralPath $binX64)) { throw "Missing .\bin\x64 (run .\get-deps.ps1 first)." }
New-Item -ItemType Directory -Path "$appDir\bin\x64" -Force | Out-Null
Get-ChildItem -LiteralPath $binX64 -File | Copy-Item -Destination "$appDir\bin\x64"

Write-Host "Copying preset media (FIL2.png, conan2.png, videoBG.mp4, longtieng2.mp4, full.mp4)..."
$media = @("FIL2.png", "conan2.png", "videoBG.mp4", "longtieng2.mp4", "full.mp4")
foreach ($file in $media) {
    $src = Join-Path $root $file
    if (-not (Test-Path -LiteralPath $src)) {
        Write-Warning "Skipping missing media: $file"
        continue
    }
    Copy-Item -LiteralPath $src -Destination $appDir
}

Copy-Item -LiteralPath "$root\app.ico" -Destination $appDir -ErrorAction SilentlyContinue

# 3) Zip it
Write-Host "Creating zip..."
$zip = Join-Path $distRoot "$exeName.zip"
if (Test-Path -LiteralPath $zip) { Remove-Item -LiteralPath $zip -Force }
Compress-Archive -Path $appDir -DestinationPath $zip -CompressionLevel Optimal

Write-Host ""
Write-Host "Done."
Write-Host "  App folder: $appDir"
Write-Host "  Zip:        $zip"

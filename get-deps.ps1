# Downloads the runtime dependencies required by Youtube Zenni Tool:
#   - FFmpeg (x64 and x86)  -> .\bin\x64\ and .\bin\x86\
#   - yt-dlp (renamed to youtube-dl.exe, a maintained youtube-dl fork) -> .\bin\youtube-dl.exe
#
# Run once from the project root:  .\get-deps.ps1
# Requires internet access. Downloads from official GitHub release pages.

$ErrorActionPreference = "Stop"

$root = $PSScriptRoot
if (-not (Test-Path -LiteralPath "$root\YoutubeZenniTool.csproj")) {
    throw "Run this script from the project root (where YoutubeZenniTool.csproj lives)."
}

$binDir = Join-Path $root "bin"
New-Item -ItemType Directory -Path "$binDir\x64" -Force | Out-Null

$tmp = Join-Path $root ".deps-tmp"
if (Test-Path -LiteralPath $tmp) { Remove-Item -LiteralPath $tmp -Recurse -Force }
New-Item -ItemType Directory -Path $tmp -Force | Out-Null

$urls = @{
    "ffmpeg-x64" = "https://github.com/BtbN/FFmpeg-Builds/releases/download/latest/ffmpeg-master-latest-win64-gpl.zip"
    "yt-dlp"     = "https://github.com/yt-dlp/yt-dlp/releases/latest/download/yt-dlp.exe"
}

function Download([string]$name, [string]$url, [string]$dest) {
    Write-Host "Downloading $name ..." -ForegroundColor Cyan
    Invoke-WebRequest -Uri $url -OutFile $dest -UseBasicParsing
}

# --- FFmpeg x64 ---
$zip64 = Join-Path $tmp "ffmpeg-x64.zip"
Download "FFmpeg x64" $urls["ffmpeg-x64"] $zip64
Write-Host "Extracting FFmpeg x64 ..."
Expand-Archive -LiteralPath $zip64 -DestinationPath "$tmp\ffmpeg64" -Force
$ff64 = Get-ChildItem -LiteralPath "$tmp\ffmpeg64" -Directory | Select-Object -First 1
Copy-Item -LiteralPath "$($ff64.FullName)\bin\ffmpeg.exe" -Destination "$binDir\x64\ffmpeg.exe" -Force
Copy-Item -LiteralPath "$($ff64.FullName)\bin\ffplay.exe" -Destination "$binDir\x64\ffplay.exe" -Force -ErrorAction SilentlyContinue
Copy-Item -LiteralPath "$($ff64.FullName)\bin\ffprobe.exe" -Destination "$binDir\x64\ffprobe.exe" -Force -ErrorAction SilentlyContinue
Write-Host "FFmpeg x64 -> .\bin\x64\" -ForegroundColor Green

# --- yt-dlp -> youtube-dl.exe ---
$yt = Join-Path $tmp "yt-dlp.exe"
Download "yt-dlp" $urls["yt-dlp"] $yt
Copy-Item -LiteralPath $yt -Destination "$binDir\youtube-dl.exe" -Force
Write-Host "yt-dlp -> .\bin\youtube-dl.exe" -ForegroundColor Green

# cleanup
Remove-Item -LiteralPath $tmp -Recurse -Force

Write-Host ""
Write-Host "Dependencies ready. You can now build and run the tool." -ForegroundColor Green
Write-Host "  dotnet build"
Write-Host "  .\bin\Debug\net461\Youtube Zenni Tool.exe   (run from a folder containing .\bin and .\ffmpeg code)"

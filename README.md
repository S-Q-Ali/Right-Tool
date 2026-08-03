# Youtube Zenni Tool (Open Source)

A batch video-processing tool for Windows that drives **FFmpeg** through an
editable library of preset commands, plus a YouTube link grabber.

This is a **deobfuscated and refactored** rebuild of the original
"Youtube Zenni Tool - VienNV" application:

- All Pro-version paywall locks removed (previously gated features are now free).
- Donation prompts, upgrade links, and author branding removed.
- Obfuscated symbol names renamed to meaningful ones.
- Builds cleanly with the modern .NET SDK (targets .NET Framework 4.6.1).

## Features

- **Render tab** — queue many video files and run an FFmpeg preset against
  every item in parallel (configurable thread count).
- **Live tab** — non-filter and filter streaming setups, plus an ffplay preview.
- **Get Link tab** — extract a direct download link from a YouTube URL via
  `youtube-dl` (bundled as `yt-dlp`).
- **Editable presets** — the `ffmpeg code\` folder is a plain-text command
  library you can edit at runtime from the UI.

## Requirements

- Windows
- [.NET SDK 8+](https://dotnet.microsoft.com/download/dotnet/8.0) (build)
- FFmpeg and yt-dlp binaries (see next step)

## Setup

```powershell
# 1. Fetch runtime dependencies (FFmpeg x64/x86, yt-dlp)
.\get-deps.ps1

# 2. Build
dotnet build -c Release

# 3. Run (from the project root, so .\bin and .\ffmpeg code resolve)
.\bin\Release\net461\Youtube Zenni Tool.exe
```

`get-deps.ps1` downloads:

| Path | Source |
| --- | --- |
| `bin\x64\ffmpeg.exe`, `ffplay.exe` | [BtbN FFmpeg-Builds](https://github.com/BtbN/FFmpeg-Builds) |
| `bin\youtube-dl.exe` | [yt-dlp](https://github.com/yt-dlp/yt-dlp) (renamed) |

> Notes:
> - The app auto-selects `bin\x64\` on 64-bit Windows. BtbN no longer ships
>   32-bit FFmpeg builds, so only x64 is bundled; to support 32-bit Windows,
>   place `ffmpeg.exe`/`ffplay.exe` into `bin\x86\` yourself.
> - The original tool shipped `youtube-dl.exe` from 2018 which required the
>   legacy VC++ 2010 runtime (MSVCR100.dll). This rebuild uses **yt-dlp**, a
>   maintained fork with no extra runtime, renamed to `youtube-dl.exe` so the
>   existing code path is unchanged.

## Runtime layout

The app resolves everything relative to the current working directory, so run
it from a folder that contains:

```
bin\
  x64\ffmpeg.exe
  x86\ffmpeg.exe   <- optional, for 32-bit Windows only
  youtube-dl.exe
ffmpeg code\
  *.txt            <- command presets (loaded into the "FFmpeg code" combo box)
videoBG.mp4        <- background clip used by the "Shrink Copyright Bypass" preset
FIL2.png           <- filter PNG used by the "Shrink Copyright Bypass" preset
conan2.png         <- filter used by the "Demo Code - Shrink Video" preset
longtieng2.mp4     <- background clip used by the "Demo Code - Shrink Video" preset
```

> The media files above ship with the repo (in the project root). Presets that
> reference files *not* bundled (e.g. `intro.mp4`, `outro.mp4`, `Logo.png`,
> `audio.wav`, `E:\Render\...`) expect you to provide them in the working
> directory yourself.

## Presets

The `ffmpeg code\` folder contains the original preset templates (kept as-is,
filenames normalized to ASCII). Each file is an FFmpeg command line with
`{input}` and `{output}` placeholders:

```
ffmpeg -ss 10 -i "{input}.*" -vcodec copy -acodec copy "{output}.mp4"
```

- `{input}.*` is replaced with the selected input file path (and its extension).
- `{output}.mp4` is replaced with the chosen output folder + file name.
- Lines starting with `//` are treated as comments (stripped before running).

Some presets reference extra media files (e.g. `intro.mp4`, `logo.png`,
`audio.wav`) that must exist in the working directory — see each preset's
comments. Output files are written to the folder chosen in the "Output" box.

> If FFmpeg fails (e.g. a referenced media file is missing), the row turns red
> with **"Failed"** and a dialog shows the last FFmpeg error lines, e.g.
> `FIL2.png: No such file or directory`. The chosen output folder is created
> automatically if it doesn't exist yet.

## Project structure

| File | Purpose |
| --- | --- |
| `MainForm.cs` | Main UI: Render, Live Stream, and editor tabs |
| `GetLinkForm.cs` | YouTube link grabber (youtube-dl) |
| `EditFfmpegCodeForm.cs` | In-app preset editor |
| `FfmpegHelper.cs` | Preset file discovery, comment stripping, command execution |
| `FolderSelectDialog.cs` | Folder picker wrapper |
| `get-deps.ps1` | Downloads FFmpeg + yt-dlp |

## Provenance

The source was produced by decompiling the original obfuscated application,
decoding its embedded string-encryption, and renaming the obfuscated symbols.
The original author's Pro-version restrictions and branding were removed.

# Simple Screen Recorder

Simple Screen Recorder **v1.1.0.4** is a lightweight Windows app for recording your screen.

You can record the full screen, one monitor, or only a selected part of the screen.

![Screenshot](https://naetech.ro/wp-content/uploads/2025/SimpleScreenRecorder/simplescreenrecorder.png?v1.1.0.3)

---

## Main Features

- Record full display
- Record selected screen area
- Record Windows audio
- Record microphone audio
- Choose H.264 or H.265 video
- Choose 15, 30, 60, or 120 FPS
- Choose video quality level
- Use CBR or VBR recording mode
- Optional 3-second countdown
- Recording timer
- Tray controls
- Open last recording
- F9 / F10 hotkeys

---

## How to Use

Download `SimpleScreenRecorder.exe` from the [official GitHub releases](https://github.com/nmd-113/SimpleScreenRecorder/releases),
save it to a folder, and run it. No application installation or administrator
privileges are required. The distribution needs only this EXE; application DLLs,
configuration files, and debugging symbols do not need to accompany it.

To retain preferences when upgrading, replace the EXE in the same folder with the
same filename. On first launch, v1.1.0.4 imports the previous version's saved settings
if it has no settings of its own yet.

1. Open **Simple Screen Recorder**.
2. Choose where recordings should be saved.
3. Choose your audio options:
   - Windows Audio
   - Microphone
4. Choose video settings:
   - Codec
   - FPS
   - Quality
   - CBR or VBR
5. Choose what to record:
   - Leave area empty to record the full display.
   - Click **Select Area** to record only part of the screen.
6. Click **Start Recording** or press **F9**.
7. Click **Stop Recording** or press **F10**.

While recording, click the primary button or press **F9** to pause. Use it again to resume.

---

## Recording Part of the Screen

Click **Select Area**.

A transparent window will appear.

Move and resize it over the part of the screen you want to record.

Then:

- Press **Enter** to confirm
- Press **Esc** to cancel
- Double-click to confirm

Click **Clear** to return to full-display recording.

---

## Video Settings

### FPS

Choose:

- **15 FPS - Low**
- **30 FPS - Standard**
- **60 FPS - Smooth**
- **120 FPS - Very Smooth**

Higher FPS gives smoother video, but uses more system resources and creates larger files.

### Quality

The quality slider has 10 levels.

Lower quality creates smaller files.

Higher quality creates sharper video but larger files.

### Codec

Choose:

- **H.264** for best compatibility
- **H.265** for smaller files, if your PC supports it

---

## Hotkeys

- **F9** = Start/Pause/Resume recording
- **F10** = Stop recording

If another app already uses these keys, Simple Screen Recorder will show a warning.

You can still use the buttons and tray menu.

---

## Tray Menu

When minimized, the app can be controlled from the tray.

You can:

- Show the app
- Start recording
- Pause recording
- Resume recording
- Stop recording
- Open recordings folder
- Open last recording
- Exit

---

## Requirements

- Windows x64
- .NET Framework 4.8
- The x64 Visual C++ runtime and Windows Media Foundation used by ScreenRecorderLib

These runtime prerequisites may already be present. Windows N/KN editions may need
the Media Feature Pack. The application does not install or update prerequisites.

For building the project:

- Visual Studio 2022 recommended
- .NET Framework 4.8 Developer Pack

Build **Release | x64**. Distribute only `SimpleScreenRecorder.exe`; the generated
PDB is optional debugging information and should stay outside the distribution.

---

## Antivirus Notice

Historical VirusTotal results supplied for this project were **32/68 detections**
for the original v1.1.0.3 release and **1/71** for the audited candidate, with only
Malwarebytes reporting `Malware.AI.4183176083`. That candidate predates the 1.1.0.4
version update and has SHA-256:

`10b7d164db9c4b02aa74cdf9cc243365c00f574e8346b6c85bcbd609d9d40869`

The audit improved private temporary DLL extraction, SHA-256 integrity verification,
and controlled cleanup. The maintainer also reports **1/71 detections** in a
follow-up scan of the rebuilt **v1.1.0.4** EXE, with SHA-256:

`1c01ed9b68486dd39802c47357a4636a1bc3841dcb93b74fa5e75ea6cfc45a44`

These results concern the specific files scanned; they do not guarantee that a file
is malware-free or that future builds will receive the same result. Keep antivirus
protection enabled, verify the downloaded file's published hash, and report
suspected false positives to the detecting vendor.

---

## Notes

- Selected-area recording works best when the selected area is fully inside one monitor.
- If the selected area crosses multiple monitors, the app records the main part of the selected area.
- On some mixed-DPI multi-monitor setups, selected-area recording may be slightly offset.
- H.265 support depends on your PC/GPU.
- Recording functionality uses the embedded [ScreenRecorderLib](https://github.com/sskodje/ScreenRecorderLib) **6.5.1 x64** library. It is extracted to a private temporary directory for native loading; locked files may remain until a later launch can safely clean them up.

---

## License

The application is distributed under the [MIT License](LICENSE.txt).

ScreenRecorderLib's [MIT license](Resources/ScreenRecorderLib-LICENSE.txt) and the
embedded JetBrains Mono font's [SIL Open Font License](Resources/JetBrainsMono-OFL.txt)
are retained in the source and embedded in the executable.

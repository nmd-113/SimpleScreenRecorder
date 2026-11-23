# Simple Screen Recorder

Simple Screen Recorder is a lightweight, portable Windows screen recorder.

Record a full display or a selected area, with Windows audio, microphone audio, or both.
Save your recordings as MP4 files and control recording with buttons, hotkeys, or the tray menu.

**[Download Simple Screen Recorder](https://github.com/nmd-113/SimpleScreenRecorder/releases)**

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
privileges are required. You only need the EXE and the runtimes listed under
[Requirements](#requirements).

To update, close the app and replace the EXE in the same folder, keeping the same
filename so your saved preferences can carry over.

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
   - Choose a monitor to record its full display.
   - Click **Select Area** to record only part of the screen.
6. Click **Start Recording** or press **F9**.
7. Click **Stop Recording** or press **F10**.

While recording, click **Pause Recording** or press **F9** to pause. Click
**Resume Recording** or press **F9** again to continue.

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

---

## Antivirus Notice

Some antivirus products may flag the app. Download only from the
[official releases](https://github.com/nmd-113/SimpleScreenRecorder/releases) and keep
your antivirus protection enabled. If you receive a warning, check it with the
antivirus vendor before running the app.

---

## Notes

- Selected-area recording works best when the selected area is fully inside one monitor.
- If the selected area crosses multiple monitors, the app records the main part of the selected area.
- On some mixed-DPI multi-monitor setups, selected-area recording may be slightly offset.
- H.265 support depends on your PC/GPU.

---

## Build from Source

Use Visual Studio 2022 with the .NET Framework 4.8 Developer Pack. Open
`SimpleScreenRecorder.sln`, select **Release | x64**, and build.

The executable is generated in `bin\x64\Release`. Distribute only
`SimpleScreenRecorder.exe`; the PDB is optional debugging information.

---

## License

The application is distributed under the [MIT License](LICENSE.txt).

Recording is powered by [ScreenRecorderLib 6.5.1](https://github.com/sskodje/ScreenRecorderLib),
licensed under [MIT](Resources/ScreenRecorderLib-LICENSE.txt). The embedded JetBrains
Mono font uses the [SIL Open Font License](Resources/JetBrainsMono-OFL.txt).

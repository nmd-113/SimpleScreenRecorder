# Simple Screen Recorder

Simple Screen Recorder is a lightweight Windows app for recording your screen.

You can record the full screen, one monitor, or only a selected part of the screen.

![Screenshot](https://naetech.ro/wp-content/uploads/2025/SimpleScreenRecorder/simplescreenrecorder.jpg)

---

## Main Features

- Record full display
- Record selected screen area
- Record Windows audio
- Record microphone audio
- Choose H.264 or H.265 video
- Choose 30, 60, or 120 FPS
- Choose video quality level
- Use CBR or VBR recording mode
- Optional 3-second countdown
- Recording timer
- Tray controls
- Open last recording
- F9 / F10 hotkeys

---

## How to Use

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

- **F9** = Start/Pause recording
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
- Stop recording
- Open recordings folder
- Open last recording
- Exit

---

## Requirements

- Windows
- .NET Framework 4.8
- 64-bit system

For building the project:

- Visual Studio 2022 recommended
- .NET Framework 4.8 Developer Pack

---

## Notes

- Selected-area recording works best when the selected area is fully inside one monitor.
- If the selected area crosses multiple monitors, the app records the main part of the selected area.
- On some mixed-DPI multi-monitor setups, selected-area recording may be slightly offset.
- H.265 support depends on your PC/GPU.
- Recording functionality is powered by [ScreenRecorderLib](https://github.com/sskodje/ScreenRecorderLib), an open-source C# screen recording library.

---

## License

MIT License

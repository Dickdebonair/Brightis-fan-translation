# Brightis-fan-translation

A decompilation and eventual translation of the PSX game Brightis by fan(s)

## Why we're doing this

I came across this game: Brightis after finding it covered in a [video on Japan-only PS1 games.](https://youtu.be/RgxZ9lyp2WE?si=m2LKTEifSTgfvFeX&t=3310) [by Sean Seanson]  
After watching the video, and trying it myself, I fell in love with the game's mechanics. To the point that I'd like to see the game more easily playable for western/English-speaking audience.
A guide [referenced by Sean Seanson] also is useful in playing the game: <https://gamefaqs.gamespot.com/ps/576111-brightis/faqs/80425/intro-plus-controls>

## What we're doing + the goal

Right now, we're working on __tooling for text patching.__  
Brightis is an action RPG, and thus doesn't have much in the way of dialogue (compared to other PSX games,) and very little text stored within images.  
I felt it reasonable to look into how it might be to discover the location of all the displayed text and begin to slowly replace it with english. Along with dumping it so that others may translate it into different languages easier.

If you'd like to help:

- [Start on the wiki](https://github.com/Dickdebonair/Brightis-fan-translation/wiki) to find specifics & details.
  - If you DO discover anything, please contact me on RomHacking.net at `Tank120` or at my email: `dickdebonair@outlook.com`
  - Discord, [Bluesky](https://bsky.app/profile/dickdebonair.bsky.social), or [Twitter](https://x.com/DickDebonair) also works for contact under the `Dickdebonair` name.
- Refer to the [issues](https://github.com/Dickdebonair/Brightis-fan-translation/issues) on where we need help
- Spread the word that we're looking for contributors!

## Roadmap

Check the [projects](https://github.com/users/Dickdebonair/projects/1) tab to get a more up-to-date view of where we're at.
Please also contribute against the [issues](https://github.com/Dickdebonair/Brightis-fan-translation/issues) we have here! We can always improve!

- [x] Figure out the file compression
  - [ ] Determine DMA functions to understand each sections of the game [many calls in the `SCPS_101.05`]
  - [x] Get basic text decompression figured out  
  - [ ] Get basic image decompression figured out  
  - [ ] Determine Compression function to be able to reinsert files
- [ ] Locate all of the spoken/written text in the game [spread throughout multiple BIN files]
- [ ] Dump the text wholesale from the code [current tools will work with this]
  - [ ] CHR.BIN (character/NPC data)
  - ED00.STR (video file; skip)
  - [ ] MAP.BIN (Map/world data)
  - [x] ONMOVR.BIN (player data)
  - [x] OVR.BIN (Overlay/dialogue data)
  - PDADOC.BIN
  - [ ] PDADOWN.BIN (PocketStation minigame data)
  - PDADOWN.exe (PocketStation minigame executable)
  - [x] SCPS_101.05 (Main game executable)
  - SND.BIN (sound effect & Music data)
- [ ] Font hacking to add english support + Variable width
- [ ] Get a bulk translation ([Google Sheet now available for comment](https://docs.google.com/spreadsheets/d/16ST1GpUGnfzQkkyA7Y5LqPaeRHxq0L23jmVaQDX_wBU/edit?usp=sharing))
- [ ] Get text reinserted via tooling
- [ ] Figure out how to create an IPS patch
- [ ] Privately test, then publicly test the game through
- [ ] Locate all the images with text

### Discoveries

Please check the [wiki](https://github.com/Dickdebonair/Brightis-fan-translation/wiki) for the most up-to-date details.

Other potentially useful info:

- PSX File formats: <https://psx-spx.consoledev.net/cdromfileformats/>
- PSX Decompilation Gist: <https://gist.github.com/obskyr/99ce080f325bcc3d044f98fd90d447cb>
- Game Manual Scans: <https://archive.org/details/BrightisManual/mode/2up>
- Game Walkthrough: <https://gamefaqs.gamespot.com/ps/576111-brightis/faqs/80425>
- Offical Guide from Arc Ent.: <https://web.archive.org/web/20010302014341/http:/arc.scei.co.jp/brightis/1.htm>

Potential Resources:

- Secondary guide: <https://github.com/RetroAchievements/guides/wiki/Brightis> [has some names as well]
- Code Notes from others: <https://retroachievements.org/codenotes.php?g=18202>
- Proof of previous group on working on translation(?): <https://fxtwitter.com/keke_094/status/1634014234899251200>
- SJIS to Hex converter: <http://freaka.freehostia.com/charset_js2.html>
- PSX Runtime Library Reference: <https://psx.arthus.net/sdk/Psy-Q/DOCS/Devrefs/Libref.pdf>
- Overlay demo project/Example: <https://github.com/JaberwockySeamonstah/PSXOverlayExample>
- Overlay readings: <https://www.beneaththewaves.net/Software/TDR_Practice_Using_OVERLAYS.html#WalkthroughOVERLAYSOverlays>

### Tools we're using (that we haven't made)

- CDMage: <https://www.romhacking.net/utilities/1435/> [Used to open the main .BIN]
- abcde: <https://www.romhacking.net/utilities/1392/> [Script dumper/reinserer]
- SJIS Dump: <https://www.romhacking.net/utilities/645/> [Shift-JIS text dumper. Outdated]
- ImHex: <https://imhex.werwolv.net/> [Modern Hex Viewer]
- PCSX-Redux: <https://github.com/grumpycoders/pcsx-redux> [PSX emulator with Debugger]
- No$PSX: <https://www.problemkaputt.de/psx.htm> [Very detailed PSX Debugger]
- Updated PSXImager: <https://github.com/Ripper55555/psximager>
- Ghidra: <https://github.com/NationalSecurityAgency/ghidra/> [Decompiler & static Code Analysis]
  - PSX Loader for Ghidra <https://github.com/lab313ru/ghidra_psx_ldr> [Required for proper analysis]
- DeepL: <https://www.deepl.com/en/translator> [Quick Text translator]
- Cloe: <https://github.com/blueaxis/Cloe> [Useful for copying kanji locally & quickly]
- ARMips: <https://github.com/Kingcom/armips> [Used to patch code directly for translations]
- LINQpad: <https://www.linqpad.net/>

### File Structure

```text
CDmage B5 1.02.1 files list of the image "Brightis (Japan).bin"

Folder: \ (ISO9660)

Name       |       Size|    LBA|Type       |         Date and time|Timezone|Flags
CHR.BIN    |  3,778,560|    380|BIN File   |  8/26/1999 2:49:04 AM|    0:00|F
ED00.STR   | 30,143,276| 30,537|STR File   |   5/7/1999 3:10:50 AM|    0:00|FM
MAP.BIN    | 17,670,144|  2,529|BIN File   |   9/1/1999 3:16:26 AM|    0:00|F
ONMOVR.BIN |     75,676|    343|BIN File   |   9/3/1999 3:40:08 AM|    0:00|F
OP00.STR   | 37,199,276| 14,721|STR File   |   2/1/1999 5:00:20 AM|    0:00|FM
OVR.BIN    |    622,592|  2,225|BIN File   |   9/3/1999 3:40:08 AM|    0:00|F
PDADOC.BIN |  1,697,792| 13,365|BIN File   |  8/27/1999 2:16:42 AM|    0:00|F
PDADOWN.BIN|    888,832| 14,287|BIN File   |  8/30/1999 2:39:12 AM|    0:00|F
PDADOWN.EXE|    190,464| 14,194|Application|  8/30/1999 2:39:40 AM|    0:00|F
SCPS_101.05|    653,312|     24|05 File    |   9/4/1999 1:34:26 AM|    0:00|F
SND.BIN    |  4,521,984| 11,157|BIN File   |  5/21/1999 1:32:08 AM|    0:00|F
SYSTEM.CNF |         61|     23|CNF File   |  7/7/1999 11:30:14 PM|    0:00|F

 97,441,969 bytes in 0 folders and 12 files
 ```

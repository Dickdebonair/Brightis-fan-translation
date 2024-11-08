# Brightis-fan-translation
A decompilation and eventual translation of the PSX game Brightis by fan(s)

## Why we're doing this
I came across this game: Brightis after finding it covered in a video on Japan-only PS1 games. [by Sean Seanson]  
After watching the video, and trying it myself, I fell in love with the game's mechanics. To the point that I'd like to see the game more easily playable for western/English-speaking audience. 
A guide [referenced by Sean Seanson] also is useful in playing the game: https://gamefaqs.gamespot.com/ps/576111-brightis/faqs/80425/intro-plus-controls

## What we're doing + the goal
So far, we're in the exploratory phase. Brightis is an action RPG, and thus doesn't have much in the way of dialogue and a reasonable amount of text with very little of it stored in images.  
I felt it reasonable to look into how it might be to discover the location of all the displayed text and begin to slowly replace it with english. Along with dumping it so that others may translate it into different languages easier.

If you'd like to do some investigation/help: [Looking here may help](https://github.com/Dickdebonair/Brightis-fan-translation/blob/main/Ram%20Discoveries.md) in finding specifics & details. 
If you DO discover anything, please contact me on RomHacking.net at `Tank120` or at my email. `dickdebonair@outlook.com`

## Roadmap
Check the projects tab to get a more up-to-date view of where we're at.
Please also contribute against the issues we have here! We can always improve!
- [ ] Figure out the file compression 
	- [ ] Determine DMA functions to understand each sections of the game [many calls in the `SCPS_101.05`]
	- [x] Get basic text decompression figured out  
	- [ ] Get basic image decompression figured out  
		- related to FUN_8007d334(LoadImage) in `SCPS_101.05`
	- [ ] Determine Compression function to be able to reinsert files
- [ ] Locate all of the spoken/written text in the game [spread throughout multiple BIN files] use the
	- [ ] OVR.BIN (Main Overlay/dialogue text file)
	- [ ] ONMOVR.BIN (has player info/stats)
	- [ ] PDADOC.BIN 
	- [ ] SCPS_101.05 (main executable; has common menu text)
- [ ] Dump the text wholesale from the code [current tools will work with this]
	- CHR.BIN
	- ED00.STR (video file; skip)
	- MAP.BIN
	- ONMOVR.BIN
	- OP00.STR (video file; skip)
	- OVR.BIN
	- PDADOC.BIN
	- PDADOWN.BIN
	- PDADOWN.exe
	- SCPS_101.05
	- SND.BIN
	- SYSTEM.CNF (config file; Skip)
- [ ] Font hacking to add english support + Variable width
- [ ] Get a bulk translation (Excel file above will have where we are so far)
- [ ] Get the text reinserted
- [ ] Test the game through
- [ ] Locate all the images with text
- [ ] Figure out how to create an IPS patch 

### Discoveries
- The game appears encoded using a Shift-JIS table for the kanji & the on-screen English characters.
- The game is compressed, so text is split throughout various .BIN files
- We've located text in 5 main files. There's probably more, but refer to the issues/project tab for more details

- Other potentially useful info
  - Redump info: http://redump.org/disc/9919/
  - PSX file types & Common compressions. https://psx-spx.consoledev.net/cdromfileformats/
  - Game Manual: https://archive.org/details/BrightisManual/mode/2up
  - Walkthrough: https://gamefaqs.gamespot.com/ps/576111-brightis/faqs/80425 

### Tools we're using (that we haven't made)
- CDMage: https://www.romhacking.net/utilities/1435/ [Used to open the main .BIN]
- Immediate Pointer Finder: https://www.romhacking.net/utilities/1671/ [Used in looking for pointers]
- Cartographer: https://www.romhacking.net/utilities/647/ [Script dumper with limited success]
- SJIS Dump: https://www.romhacking.net/utilities/645/ [Shift-JIS text dumper. Outdated]
- ImHex: https://imhex.werwolv.net/ [Modern Hex Viewer]
- PCSX-Redux: https://github.com/grumpycoders/pcsx-redux [PSX emulator with Debugger]
- BizHawk: https://github.com/TASEmulators/BizHawk [Multi-platform emulator with many tools like a RAM watcher]
- No$PSX: https://www.problemkaputt.de/psx.htm [Very detailed PSX Debugger]
- Ghidra: https://github.com/NationalSecurityAgency/ghidra/ [Decompiler]
- DeepL: https://www.deepl.com/en/translator [Quick Text translator]
- iLoveOCR: https://www.iloveocr.com [To get Screengrabs of the game translated easy]
- Cloe: https://github.com/blueaxis/Cloe [Useful for copying kanji locally & quickly]

### File Structure 
```
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

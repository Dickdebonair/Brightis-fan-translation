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
- [ ] Figure out the file compression 
	- [ ] Determine DMA functions to understand each sections of the game [many calls in the `SCPS_101.05`]
		- FUN_80020170(decompress) 
		- FUN_8007d334(LoadImage)
	- [x] Get basic text decompression figured out  
		- [build the `BrightisCompressor` in the Scripts folder]
		- FUN_80020170(decompress) in `SCPS_101.05` in game files
	- [ ] Get basic image decompression figured out  
		- related to FUN_8007d334(LoadImage) in `SCPS_101.05`
	- [ ] Determine Compression function to be able to reinsert files
		- Use `BrightisCompressor` & FUN_80020170(decompress)
- [ ] Locate all of the spoken/written text in the game [spread throughout multiple BIN files] use the
	- [ ] OVR.BIN (Main Overlay/dialogue text file)
		- Some text translated in OVR.BIN 
		- Need to move over to OVR.BINv2 [More complete dump]
		- File uses Immediate Pointers, need pointer tables
	- [ ] ONMOVR.BIN (has player info/stats)
		- File uses Immediate Pointers, need pointer tables
	- [ ] PDADOC.BIN 
	- [ ] SCPS_101.05 (main executable; has common menu text)
		- File is not compressed
- [ ] Dump the text wholesale from the code [current tools will work with this]
	- CHR.BIN
		- uncertain about full usage 
	- ED00.STR (skip)
		- Video files. no words
	- MAP.BIN
		- Main environment file. unknown if it has text
	- ONMOVR.BIN
		- Player stats, magic, moves, etc
	- OP00.STR (skip)
		- Video file. no words
	- OVR.BIN
		- Has the majority of the text in game
	- PDADOC.BIN
		- Appears uncompressed. Dumped using SJIS-Dump
	- PDADOWN.BIN
		- used in the PocketStation game
	- PDADOWN.exe
		- used in the PocketStation game
	- SCPS_101.05
		- main function; uncompressed
		- Dumped using SJIS-Dump
	- SND.BIN
		- Unknown usage; hundreds of Unknown functions
	- SYSTEM.CNF (Skip)
		- Boot info doc. Readable ASCII in english. 
- [ ] Font hacking to add english support + Variable width
	- Game uses full width JP text from Shift-JIS Table. Need to convert. 
	- [ ] research how to convert the game to use varible width
- [ ] Get a bulk translation
	- use the [spreadsheet (TBA)]
- [ ] Get the text reinserted
	- abcde is the popular option. Will attempt to use that 
	- [ ] create working extraction script for abcde with pointers
	- [ ] create working insertion script for abcde with pointers
- [ ] Test the game through
	- Building a save file at each dungeon/before each cutscene
	- save states are in the Github for multiple emulators
- [ ] Locate all the images with text
  - Primarily found to the PocketStation game [located in PDADOWN.BIN/EXE]
  - Most text not written is in fantasy font/language
- [ ] Figure out how to create an IPS patch 

### Discoveries
- The game appears encoded using a Shift-JIS table for the kanji & the on-screen English characters.
- The game is compressed, so text is split throughout various .BIN files
- We've located text in 5 main files:
	- CHR.BIN
	  - Second largest file. Fairly unsure of the full contents
  - OVR.BIN
    - Largest collection of text, both dialogue & Enemy data [names are displayed as ASCII such as "HONEYBEE"].
    - Has a large amount of readable kanji interspliced with formatting command codes & other data
  - ONMOVR.BIN
    - Appears to be for Player data & menus. Words like "GOLD/SKILL/EXP" are here, It also includes Shop data, special moves, and appears to be everything related to what the player can do, both in the context of the game world and meta.
    - The intro text, where the player speaks to the Wizard and performs the Tutorial is also found here. 
  - PDADOWN.EXE
    - Most of the written kanji appears to be towards the bottom [starting at `0x000285D0`] and looks to be regarding the pocketstation game.
    - There are character tables [starting at `0x0002AD00`] which can be used to test encodings. 
  - SCPS_101.05
    - The MAIN executable for the game, containing common library files.  Starting at `0x0008EB20` this contains location names, a list of enemies, some menu text, along with other info that would be useful as a guide of sorts to other files. 
- `ED00.STR` & `OP00.STR` are the Ending & Opening video files, respectively.

- Other potentially useful info
  - Redump info: http://redump.org/disc/9919/
  - PSX file types & Common compressions. https://psx-spx.consoledev.net/cdromfileformats/
  - Game Manual: https://archive.org/details/BrightisManual/mode/2up
  - Walkthrough: https://gamefaqs.gamespot.com/ps/576111-brightis/faqs/80425 

### Tools we're using 
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

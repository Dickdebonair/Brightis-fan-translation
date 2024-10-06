# Text Notes
- Text is (mostly) readable using a Shift-JIS table
- Game will regularly use `8140` [ShiftJIS blank space] to indent text
- code used to read the text for the first scene is in one overlay inside ONMOVR.BIN at `0x2E38D`
- Some files seem to be normally compressed in `OVR.BIN` while other text are compressed using different methods.
- Pointers are used differently across files.
	- ONMOVR.BIN appears to use Immediate Pointers. [Use Immediate Pointer Finder to assist]
### Command Codes
  - Command codse are hex under `20`
   - New line (appears to be) `0B 0F`
   - `07`: Regular line breaking without the screen stopping
   - `04`: Line break with a stop button then text on a new text box
     - Following text will appear in a new text box. [scrolling]
   - `05`: Line break with a stop button but text continue in same text box
   - [?] `01`: Loading the text box
   - [?] `03`: Unload the text box
   - `1E` appears to be used regularly when at the end of a scene that needs to transition
   - `・・.` used to placehold the player's name
    - hex: `81 45 81 45 FF FF`
### Controlling the Text Overlay
Certain aspects of the characters can be changed in RAM
- `8001c0c0` - Set Character Width
- `8001c0c4` - Set Character Height (from top)
- `8001c0c8` - Set Character Height (from bottom)
- `8001c0d0` - Set Font total height (will squish character)
- `8001c09d` / `8001c070` / `8001c060` - Set Character boldness
- `8001c09c` - Set Character Aliasing
- `8001c06c`  / `8001c05`c - Set Character Transparency
- `8001c0b8` - Character kerning
### Talking with the Wizard, Wadoh:
| JP | Translation |
| ---      | ---       |
|ワドー | Wado/Wadoh |
|気がついたようじゃな。| I see you've noticed [me].|
|どうじゃ・・・立てるか?	| How about [it]... can you stand?|
 - location in RAM: `0x8015c9c8`
 - location in ROM/file: `0x1D064`[OVR.BIN]
 - how to trigger in game: intro to game with Wado.

### Other Misc. text 
- Text: `ＳＫＩＬＬ` [Shift-JIS Encoded]
	- location in RAM: TBD
 	- location in ROM/File: `0x000B2D30` [Full Game BIN]
  - how to trigger in game: Pause Menu, first screen

- Text: `キーコンフィグ` key config
	- location in RAM: TBD
  - location in ROM/FIle: `0x000CEA42`[Full Game BIN]
  - how to trigger in game: pause menu, far right option

- Text: `【はい】／　【いいえ】` Yes / No
	- location in RAM: TBD
  - location in ROM/FIle: `0x000B29E2`[Full Game BIN]
  - how to trigger in game: used in multiple places, Load game is first

- Text: `休憩しますか？` Do you want to take a break?
	- location in RAM: TBD
  - location in ROM/FIle: `0x000B29FC`[Full Game BIN]
  - how to trigger in game: Use a fountain [Dungeon]

### Player Name
- Stored in RAM [breakpoint]: `8001e4e8`
- default name: Al Ted `アル・テッド`

# Image Notes
- Doesn't appear that palette data can be easily tracked.
  - Game tracks images in 'binary color code'(?)
  - no use in looking for images in VRAM
- images stored in 8 bpp
- Palette data appears to be stored in the `SCPS_101.05`
- Game uses Standard TIM matches.
  - found many Magic Tag hex sequences
    - `(A magic tag "0×10000000" is basically what Identifies if it is an image or not)`
  - The next 8 bytes tell you the size of the file.

### Intro text Crawl: 
| JP | Translation |
| ---      | ---       |
| 世の国々が大陸内の番権笠争った  | Long ago, all the kingdoms of the world fought for control of the continent- |
|永き戦乱に疲抽し。 | Tired of the long warfare.| 
|土族ら世代全貸邊 きっか(ま皮 | The generations of the earthly tribes lent their skins to the war. |
|休戦へと流れ | Toward a truce |
|天った冨と国力を回復すべく興和を使い |	to restore the wealth and power of the country.|
 - location in RAM: `0x8007F080`
 - location in ROM/file: Part of a texture (Displayed as 'QuadSemiTex')
 - how to trigger in game: Opening text [start of game]
 - the remainder is saved in the spreadsheet. Will be made public at a later date

# Ghidra discoveries
Using Ghidra, I was able to find some comments at the top of some files & functions. They may be useful so I'll save them here. 
 ```
 SYSTEM.CNF: // ram:00000000-ram:0000003c ()
 OVR.BIN: // ram:00000000-ram:000d8fff 
 PDADOC.BIN: //ram:00000000-ram:0019e7ff 
 PDADOWN.BIN // ram:00000000-ram:000d8fff 
 SCPS_101.05 [Has the main() function, mutlitple ram locations]
   - main // ram:80010000-ram:8009e30f 
   - cache // ram:1f800000-ram:1f8003ff 
 SND.BIN // ram:00000000-ram:0044ffff 
 ONMOVR.BIN // ram:00000000-ram:0001279b 
 CHR.BIN // ram:00000000-ram:0039a7ff 
 MAP.BIN // ram:00000000-ram:010d9fff 
 PDADOWN.EXE [Another main() function]
   - main // ram:80010000-ram:80037dbf 
   - cache // ram:1f800000-ram:1f8003ff 
 ```

We've also found two functions key in determining how the game works. 
- One is `FUN_80020170` in `SCPS101.05` which we've confirmed is the game's decompress function for files. 
- The other is `FUN_8007d334` in the same file which we believe is how the game is loading images/textures.
- We suspect `FUN_8001bad8` in `SCPS_101.05` is used to draw Japanese characters from the main text box Overlay.
	- `FUN_8001bc9c` for English/ASCII characters (such as enemy names in the top right)
 
These can be found using the decompilations in the `Decrypted Files/Ghidra` folder, and browsing them in Ghidra. 

# Text Notes
- Text is (mostly) readable using a Shift-JIS table
  - Command codse are hex under `20`
   - New line (appears to be) `0B 0F`
   - Keep an eye on `07`, `05`, `04`

### Talking with the Wizard, Wado:
- Text: 
```
ワドー										WADO.
気がついたようじゃな。			I see you've noticed [me].
どうじゃ・・・立てるか?			How about [it]... can you stand?
```
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
- Text: 
```
世の国々が大陸内の番権笠争った 			The nations of the world fought for dominion over the continent
永き戦乱に疲抽し。									Tired of the long warfare.
土族ら世代全貸邊 きっか(ま皮 				The generations of the earthly tribes lent their skins to the war.
休戦へと流れ 											Toward a truce
天った冨と国力を回復すべく興和を使い 	to restore the wealth and power of the country.
```
 - location in RAM: `0x8007F080`
 - location in ROM/file: Part of a texture (Displayed as 'QuadSemiTex')
 - how to trigger in game: Opening text [start of game]

# Ghidra discoveries
 Using Ghidra, I was able to find some comments at the top of some files & functions. They may be useful so I'll save them here. 
 ```
 SYSTEM.CNF: // ram:00000000-ram:0000003c 
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
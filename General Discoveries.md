# Text Notes
- Text is (mostly) readable using a Shift-JIS table
- Game will regularly use `8140` [ShiftJIS blank space] to indent text
- code used to read the text for the first scene is in one overlay inside ONMOVR.BIN at `0x2E38D`
- Some files seem to be normally compressed in `OVR.BIN` while other text are compressed using different methods.
- Pointers are used differently across files.
	- ONMOVR.BIN appears to use Immediate Pointers.
### Decompression notes (and what to look out for)
(Discovered by Onepiecefreak)
- If the data is coincidentally set up in a way that displacements never go out of the start of the buffer, you can decompress any buffer given to it.
- A patch of 00's would just be a collection of raw flags that have a count of 0.
- For repetition you only need 2 bytes along with a properly set up bit pattern in the flag for it.  
So you most likely end up with either a seqeuence of raws until you hit the offset, or sliding window pairs that don't go back too much by coincidence
- Just being able to be decompressed is never a sure-fire way to declare something compressed
### Command Codes
  - Command codse are hex under `20`
  - Start of text blocks always begin with `BD 27`
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
### OVR.BIN Specifics
(Discovered by Onepiecefreak)
- If you look at the data of OVR.BIN, you will see that it does start out with a table of size-offset pairs, prepended by a 4-byte count.
- Notice how the columns below marked all increase regularly. This wouldn't happen with the compression.  
Those patterns would break up somewhere due to flag bytes having to be in there.
- This pattern acts as a "Table of Contents", and will need to be managed when compressing/decompressing.
  - It tells you how many sectors there are, where they are in the BIN and how big they are.
  - This is not the case for all BIN files and not entirely reliable, but it IS a thing for at least 2 of them. [ONMOVR.BIN & CHR.BIN]
- If you count the 4-byte values from `0x4` onwards, you'll notice, that there are `0x3e` x 4-byte values following it.  
Which makes the value at `0x0` the count for that size-offset table.
- You can also see how the size-offset table pattern breaks after the selected area, another indicator of the end of the table which coincides with the count.
- The same pattern appears in other BIN files
- The size and offsets are shifted, so they can't be taken directly.
- It also seems like the shift logic is different per file, but if you just shift them correctly you WILL be at the start of data.
- We cannot say if that table is part of the .BIN file itself, or if this is just part of one of the many parts in that file.
  - Given this table is not compressed, but its parts are, I'd say this table is for the whole .BIN
  - (while table assumption is sound, it could be just part of one blob in the .BIN of many, which supports other assumptions)
#### Determining the Table - OVR.BIN
We believe it could be: 
```
One size-offset pair is split into two 2-byte values, little endian.
Left value is the size, right value is the offset. In the OVR, shift left the offset by 10, shift left the size by 2
```
- Other .BIN files might use other shift values, un-researched currently
- those parts are then compressed as well

![OVR BIN table](https://github.com/user-attachments/assets/e8e31fa6-09e4-4188-9caf-719532a4d140)

#### Determining the Table - CHR.BIN
We believe it could be: 
- Following the same pattern as `OVR.BIN` as it:
  - follows the mentioned pattern
  - Count checks out
  - table pattern visibly stops after selected area
  - columns only contain increasing values in an unbroken pattern
- The offset should only be shifted by 2, the size also shifted by 2.
- This one may not be the table for the whole BIN file, but maybe just one blob of many.
- Shifting the offset here doesn't make much sense beyond 2, and never coincides with clear `00` patches.

![CHR BIN table](https://github.com/user-attachments/assets/7febfb9f-197e-49a0-b4f9-0b27d87fdfee)

#### Determining the Table - ONMOVR.BIN
We believe it could be: 
- Following the same pattern as `OVR.BIN` as it:
  - follows the mentioned pattern
  - Count checks out
  - table pattern visibly stops after selected area
  - columns only contain increasing values in an unbroken pattern
- The offset should only be shifted by 2, the size also shifted by 2.
- This one MAY be the table for the whole BIN file.

![ONMOVR BIN table](https://github.com/user-attachments/assets/2f07a2bc-dbae-4eb4-8822-3e7cc2b21021)
### MAP.BIN Specifics 
-  While it does not have a Table of Contents, its blobs/sectors are just compressed and aligned to `0x800`
-  They seem to be a form of data tree
-  They contain textures for the world
  -  Textures are paletted images.
  -  Start with an identifier TEX8 followed by a palette of 256 colors, encoded with RGB555.
  -  identifers follows the image data itself, encoded as 1-byte indexes into the palette
  -  texture size to be 256x256
-  BIN seems to contain more than just one file format
# Image Notes
- Palette data appears to be stored in the `SCPS_101.05`
- Game uses Standard TIM matches.
- found many Magic Tag hex sequences
  - (A magic tag "0×10000000" is basically what Identifies if it is an image or not)

# Pointers & Text offsets
After we've gotten a dump of `OVR.BIN`, we started working on listing the decompressed pointers. Due to the decompression tool at the time [as of Oct 21st], it is split into 61 parts, by sector, with the trailing zeros removed. There ***may*** be issues in some parts, but this is a starting point. 
Due to this, we've noticed pointers being stored in different methods: 
- **Trailing** *[or default/following/etc.]* - following toward the end of the file, or near the top in some cases. 
- **Embedded** - Pointers are stuck in the middle of the assembly code.

We're currently using these methods to determine the allignment of pointers to the text:
- **Trailing**

`(default RAM starting position) - (pointer value) = (Text starting location)`
i.e. `80158180 - 80158138 = 48`  
so our line starts at an offset of 48.

- **Embedded**

Since it's much trickier, and seems to be done for Dialogue heavy cutscenes/events. a snippet of code has been graciously provided by esperknight on how to determine the position. 
```
        if ((PtrValue & 0x8000) >> 15 == 1)
        {
            int ActualPtrValue = (PtrValue & 0xFFFF0000) + ((PtrValue & 0xFFFF) + -0x10000);
            PtrValue += PtrValue - ActualPtrValue;
        }
```
The current WIP allignment of text to pointers can be found here: https://github.com/Dickdebonair/Brightis-fan-translation/blob/9d96fd76c2a1c1a95d0f0471e9687712716e156d/Brightis%20Pointers%20%26%20WIP%20translations%20%5BOct%2021st%202024%5D.xlsx

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
## Static Code Analysis
We've found some functions key in determining how the game works. 
- `FUN_80020170` in `SCPS101.05` which we've confirmed is the game's decompress function for files. 
- The other is `FUN_8007d334` in the same file which we believe is how the game is loading images/textures.
- We suspect `FUN_8001bad8` in `SCPS_101.05` is used to draw Japanese characters from the main text box Overlay.
  - `FUN_8001bc9c` for English/ASCII characters (such as enemy names in the top right)
- `sub_8001F57C` in `SCPS101.05` utilizes `DAT_800E0190` which holds an array of 6 elements, which is populated by the file handles to every BIN file.
  - `sub_8001F57C` looks up a blob in one of those 6 BIN files, by a size-offset pair, and return a pointer to that blob.
  - then receives the index to the BIN file it should take the blob from (which is indirectly clamped by `sub_8001E12C` due to it loading 6 files)
  - And the `sizeOffsetPair` is size in the lower 0x1FFFF bits, shifted right by 2. And the top 0x7FFF bits are the offset into the BIN file
  - it can be assumed that there ISN'T necessarily a global lookup table to all the blobs in the BIN files, but the game might just hardcode the offset and sizes where it needs it.
    - Need to find all the references to `sub_8001F57C`, collect the size offset pairs, and later patch them, if any of the blobs changed in size or offset.  
Maybe by a script or something

These functions can be found using the decompilations in the `Decrypted Files/Ghidra` folder, and browsing them in Ghidra or IDA. 

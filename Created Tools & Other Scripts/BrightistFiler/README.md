# Description

The purpose of this tool is to decompress while perserving the table structure at the beginning of Brightis' game files, while also being able to reconstruct/recompress the file.

In example, a reconstructed `OVR.BIN` (without changes) uses ~4KB less, due to better compression efficiency than the original game. Useful for modication of files.

## Tested (& Successful) with

- OVR.BIN (Primary Dialogue & in-game Cutscenes)
- ONMOVR.BIN (Character stats, weapons, skills, etc.)

## How to use

Extract a BIN into a folder:

```command line
BrightistFiler.exe -e Path/to/BIN
```

The extracted files will have 3 digits as their names, which persists their original index for recreation purposes.  
The file extension persists the file type to recreate image and other formats later on. However, for now this is irrelevant.

Create a BIN file from a folder:

```command line
BrightistFiler.exe -c Path/to/FOLDER 
```

The folder is required to have files with 3 digits at the start, which signify their index in the recreated file.  
The tool will not create the BIN if an index is missing.  
It's possible to rename the files to anything you want, as long as the 3 digits remain at the beginning.

For example `003_demoplay.bin` is a valid name.

For `ONMOVR.BIN` a warning is printed to the command line.  
If it exceeds a size of `0x12800` (as that is the size checked for in code), the game code MUST be patched.

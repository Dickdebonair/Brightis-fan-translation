# What are these tools?

These are the tools that have been created by those part of the Brightis Fan Translation Project. Below is a description of what everything here does.

Any that are specfically built programs will have READMEs within them.

## BrightistFiler

The current compressor tool. Used to open the files in the game by decompressing them, and splitting it into blobs/sectors ready to be edited and modified.  
Currently, it works with only `OVR.BIN` & `ONMOVR.BIN`.  
More support to be added as discoveries are made.

## BrightistRenderer

Current Text Renderer tool. Used to best preview how the game will show text in game and leverage the control codes.  
It leverages the [Google Sheet](https://docs.google.com/spreadsheets/d/16ST1GpUGnfzQkkyA7Y5LqPaeRHxq0L23jmVaQDX_wBU/edit?usp=sharing), and preps files ready to be loaded into `TranslationToSource`.

## C# LinQpad Scripts

A collection of C# scripts written for LinQPad. They could be convert into other languages, but the are used to properly extract all of the text, per blob, by pointer.

## TranslationToSource

Current tool to convert completed translations into ARMips compatable assembly. This is needed to directly patch file changes back into the game.  
To be used with `BrightistRenderer`.

## BrightisCompressor

A C# version of the original `decompress.py`. Works with some issue, and is kept for archival reference.

## decompress.py

The very first decompression python script we ever had to break open the game's files. Kept for archival reference.

# Brightis Compressor
A .NET 8.0 project that focus on decompression and compression of the files.
You can use it inside the commandline.

Requirements
- .NET Runtime 8.0.8 (to be able to run the application)
- SDK 8.0.401 (to be able to build the project on Visual Studio)

## How to use it

Decompressing a file
BrighitsComp -d compressed.bin decompressed.bin

Decompressing a file starting at a specific offset (in Hex)
BrighitsComp -d compressed.bin decompressed.bin 9C800

Compressing a file
BrighitsComp -c decompressed.bin compressed.bin


## Structure of the project

BrightisCompressor
- Main project for the compressor functions

- Program.cs: Main executable that is used for the console application
- Compressor.cs: Class used to decompress/compress files

Testing
- Project used to test the different functions for compression
- CompTests.cs: Class used to create tests

## Contact
-Stewie on discord
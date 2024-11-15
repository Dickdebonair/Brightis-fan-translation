using BrightistFiler.Compression;
using BrightistFiler.Formats.Archives.Models;
using Kontract.Kompression;

namespace BrightistFiler.Formats.Archives.Files
{
    internal class ChrReader
    {
        private static readonly ICompression Compression = new BrightisCompression();
        private static readonly ArchiveFileTypeReader TypeReader = new();

        /*8001e8c8 -> 0 547e [size*4]
  8001e8e8 -> File 1 (Copied to DAT_80145ea0)
  8001e948 -> File 2 (Copied to DAT_801239c0)
  8001ea04 -> File 3 (Loaded to DAT_800c0122)
    8001ea18 -> Decompress File 3.1
	8001ea58 -> Decompress File 3.2*/

        public ArchiveFileData[] Read(Stream input)
        {
            var result = new List<ArchiveFileData>();

            //using var br = new BinaryReaderX(input, true);

            //using var subContent = new SubStream(input, 0, 0x151F8);
            //result.Add(ReadSubFile(subContent));

            return result.ToArray();
        }

        //private MultiArchiveFileData ReadSubFile(Stream subFile)
        //{
        //    var decompressedStream = new MemoryStream();
        //    Compression.Decompress(subFile, decompressedStream);

        //    decompressedStream.Position = 0;
        //    using var br = new BinaryReaderX(decompressedStream, true);
        //}
    }
}

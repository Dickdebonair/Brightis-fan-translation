using BrightistFiler.Compression;
using BrightistFiler.Formats.Archives.Models;
using Komponent.IO;
using Komponent.IO.Streams;
using Kontract.Kompression;

namespace BrightistFiler.Formats.Archives.Files
{
    internal class OvrReader
    {
        private static readonly ICompression Compression = new BrightisCompression();
        private static readonly ArchiveFileTypeReader TypeReader = new();

        public ArchiveFileData[] Read(Stream input)
        {
            var result = new List<ArchiveFileData>();

            using var br = new BinaryReaderX(input, true);

            int fileCount = br.ReadInt32();
            for (var i = 0; i < fileCount; i++)
            {
                uint sizeOffset = br.ReadUInt32();

                uint fileSize = (sizeOffset & 0x1FFFF) << 2;
                uint fileOffset = (sizeOffset >> 17) << 11;

                using Stream fileStream = new SubStream(input, fileOffset, fileSize);
                Stream decompStream = new MemoryStream();

                Compression.Decompress(fileStream, decompStream);

                decompStream.Position = 0;
                result.Add(new SingleArchiveFileData
                {
                    Index = i,
                    Data = decompStream,
                    Type = TypeReader.Peek(decompStream)
                });
            }

            return result.ToArray();
        }
    }
}

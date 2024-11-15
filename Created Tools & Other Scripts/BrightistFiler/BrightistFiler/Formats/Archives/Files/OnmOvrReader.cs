using BrightistFiler.Compression;
using BrightistFiler.Formats.Archives.Models;
using Komponent.IO.Streams;
using Komponent.IO;
using Kontract.Kompression;

namespace BrightistFiler.Formats.Archives.Files
{
    internal class OnmOvrReader
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
                int fileSize = br.ReadInt16() << 2;
                int fileOffset = br.ReadInt16() << 2;

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

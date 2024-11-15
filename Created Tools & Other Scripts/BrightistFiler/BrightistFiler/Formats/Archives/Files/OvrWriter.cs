using BrightistFiler.Compression;
using BrightistFiler.Formats.Archives.Models;
using Komponent.IO;
using Kontract.Kompression;

namespace BrightistFiler.Formats.Archives.Files
{
    internal class OvrWriter
    {
        private static readonly ICompression Compression = new BrightisCompression();

        public void Write(ArchiveFileData[] files, Stream output)
        {
            using var bw = new BinaryWriterX(output, true);

            bw.Write(files.Length);

            long fileDataOffset = (files.Length * 4 + 4 + 0x7FF) & ~0x7FF;
            for (var i = 0; i < files.Length; i++)
            {
                if (files[i] is not SingleArchiveFileData singleFile)
                    throw new InvalidOperationException($"Index {i:000} cannot be multiple files.");

                if (singleFile.Index != i)
                    throw new InvalidOperationException($"Missing index {i:000}.");

                using var ms = new MemoryStream();
                Compression.Compress(singleFile.Data, ms);

                // Write file info
                output.Position = i * 4 + 4;

                long fileSize = ((ms.Length + 3) >> 2) & 0x1FFFF;
                long fileOffset = (fileDataOffset >> 11) & 0x7FFF;

                long sizeOffset = (fileOffset << 17) | fileSize;
                bw.Write((uint)sizeOffset);

                // Write compressed file data
                output.Position = fileDataOffset;

                ms.Position = 0;
                ms.CopyTo(output);

                bw.WriteAlignment(0x800);

                fileDataOffset = output.Position;
            }
        }
    }
}

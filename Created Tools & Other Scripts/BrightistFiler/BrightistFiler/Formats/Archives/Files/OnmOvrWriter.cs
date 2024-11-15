using BrightistFiler.Compression;
using BrightistFiler.Formats.Archives.Models;
using Komponent.IO;
using Kontract.Kompression;

namespace BrightistFiler.Formats.Archives.Files
{
    internal class OnmOvrWriter
    {
        private static readonly ICompression Compression = new BrightisCompression();

        public void Write(ArchiveFileData[] files, Stream output)
        {
            using var bw = new BinaryWriterX(output, true);

            bw.Write(files.Length);

            long fileDataOffset = files.Length * 4 + 4;
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

                long fileSize = (ms.Length + 3) >> 2;
                long fileOffset = fileDataOffset >> 2;
                
                bw.Write((ushort)fileSize);
                bw.Write((ushort)fileOffset);

                // Write compressed file data
                output.Position = fileDataOffset;

                ms.Position = 0;
                ms.CopyTo(output);

                bw.WriteAlignment(4);

                fileDataOffset = output.Position;
            }

            // Warn, if static check in code would fail
            if (output.Length >= 0x12800)
                Console.WriteLine("! ONMOVR size 0x12800 exceeded. Patch required.");
        }
    }
}

using BrightistFiler.Formats.Archives.Models;

namespace BrightistFiler.Formats.Archives
{
    internal class ArchiveFileTypeReader
    {
        public ArchiveFileType Peek(Stream input)
        {
            if (input.Length < 4)
                return ArchiveFileType.None;

            var buffer = new byte[4];
            _ = input.Read(buffer);

            input.Position -= 4;

            if (buffer[0] == 'P' && buffer[1] == 'S' && buffer[2] == 'M')
                return ArchiveFileType.Psm;

            if (buffer[0] == 'T' && buffer[1] == 'E' && buffer[2] == 'X')
                return ArchiveFileType.Tex;

            return ArchiveFileType.None;
        }
    }
}

using System.Diagnostics.CodeAnalysis;
using BrightistFiler.Formats.Archives.Files;
using BrightistFiler.Formats.Archives.Models;
using Kontract.Models;

namespace BrightistFiler.Formats.Archives
{
    internal class ArchiveReader
    {
        private static readonly OnmOvrReader OnmOvrReader = new();
        private static readonly ChrReader ChrReader = new();
        private static readonly OvrReader OvrReader = new();
        private static readonly MapReader MapReader = new();
        private static readonly SndReader SndReader = new();
        private static readonly PdaDocReader PdaDocReader = new();
        
        public bool TryRead(StreamFile input, ArchiveType archiveType, [NotNullWhen(true)] out ArchiveData? archiveData)
        {
            archiveData = null;

            if (archiveType == ArchiveType.None)
                return false;

            ArchiveFileData[] files;
            switch (archiveType)
            {
                case ArchiveType.OnMovR:
                    files = OnmOvrReader.Read(input.Stream);
                    break;

                case ArchiveType.Chr:
                    files = ChrReader.Read(input.Stream);
                    break;

                case ArchiveType.Ovr:
                    files = OvrReader.Read(input.Stream);
                    break;

                case ArchiveType.Map:
                    files = MapReader.Read(input.Stream);
                    break;

                case ArchiveType.Snd:
                    files = SndReader.Read(input.Stream);
                    break;

                case ArchiveType.PdaDoc:
                    files = PdaDocReader.Read(input.Stream);
                    break;

                default:
                    return false;
            }

            archiveData = new ArchiveData
            {
                Type = archiveType,
                Files = files
            };

            return true;
        }
    }
}

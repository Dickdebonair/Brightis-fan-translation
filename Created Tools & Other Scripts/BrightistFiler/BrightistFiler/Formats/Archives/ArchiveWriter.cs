using BrightistFiler.Formats.Archives.Files;
using BrightistFiler.Formats.Archives.Models;

namespace BrightistFiler.Formats.Archives
{
    internal class ArchiveWriter
    {
        private static readonly OnmOvrWriter OnmOvrWriter = new();
        private static readonly ChrWriter ChrWriter = new();
        private static readonly OvrWriter OvrWriter = new();
        private static readonly MapWriter MapWriter = new();
        private static readonly SndWriter SndWriter = new();
        private static readonly PdaDocWriter PdaDocWriter = new();

        public void Write(ArchiveData archiveData, Stream output)
        {
            if (archiveData.Type == ArchiveType.None)
                return;
            
            switch (archiveData.Type)
            {
                case ArchiveType.OnMovR:
                    OnmOvrWriter.Write(archiveData.Files, output);
                    break;

                case ArchiveType.Chr:
                    ChrWriter.Write(archiveData.Files, output);
                    break;

                case ArchiveType.Ovr:
                    OvrWriter.Write(archiveData.Files, output);
                    break;

                case ArchiveType.Map:
                    MapWriter.Write(archiveData.Files, output);
                    break;

                case ArchiveType.Snd:
                    SndWriter.Write(archiveData.Files, output);
                    break;

                case ArchiveType.PdaDoc:
                    PdaDocWriter.Write(archiveData.Files, output);
                    break;
            }
        }
    }
}

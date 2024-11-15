using BrightistFiler.Formats.Archives.Models;
using Kontract.Models;

namespace BrightistFiler.Formats.Archives
{
    internal class ArchiveTypeReader
    {
        public ArchiveType Determine(StreamFile file)
        {
            if (Path.GetFileName(file.Path.FullName).Equals("ONMOVR.BIN", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.OnMovR;

            if (Path.GetFileName(file.Path.FullName).Equals("CHR.BIN", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.Chr;

            if (Path.GetFileName(file.Path.FullName).Equals("OVR.BIN", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.Ovr;

            if (Path.GetFileName(file.Path.FullName).Equals("MAP.BIN", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.Map;

            if (Path.GetFileName(file.Path.FullName).Equals("SND.BIN", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.Snd;

            if (Path.GetFileName(file.Path.FullName).Equals("PDADOC.BIN", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.PdaDoc;

            return ArchiveType.None;
        }

        public ArchiveType Determine(string directoryPath)
        {
            if (Path.GetFileName(directoryPath).Equals("ONMOVR", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.OnMovR;

            if (Path.GetFileName(directoryPath).Equals("CHR", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.Chr;

            if (Path.GetFileName(directoryPath).Equals("OVR", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.Ovr;

            if (Path.GetFileName(directoryPath).Equals("MAP", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.Map;

            if (Path.GetFileName(directoryPath).Equals("SND", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.Snd;

            if (Path.GetFileName(directoryPath).Equals("PDADOC", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.PdaDoc;

            return ArchiveType.None;
        }
    }
}

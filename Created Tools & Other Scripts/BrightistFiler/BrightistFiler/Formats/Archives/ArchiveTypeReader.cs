using BrightistFiler.Formats.Archives.Models;
using Konnect.Contract.DataClasses.FileSystem;

namespace BrightistFiler.Formats.Archives
{
    internal class ArchiveTypeReader
    {
        public ArchiveType Determine(StreamFile file)
        {
            if (Path.GetFileName(file.Path.FullName).StartsWith("ONMOVR", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.OnMovR;

            if (Path.GetFileName(file.Path.FullName).StartsWith("CHR", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.Chr;

            if (Path.GetFileName(file.Path.FullName).StartsWith("OVR", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.Ovr;

            if (Path.GetFileName(file.Path.FullName).StartsWith("MAP", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.Map;

            if (Path.GetFileName(file.Path.FullName).StartsWith("SND", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.Snd;

            if (Path.GetFileName(file.Path.FullName).StartsWith("PDADOC", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.PdaDoc;

            return ArchiveType.None;
        }

        public ArchiveType Determine(string directoryPath)
        {
            if (Path.GetFileName(directoryPath).StartsWith("ONMOVR", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.OnMovR;

            if (Path.GetFileName(directoryPath).StartsWith("CHR", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.Chr;

            if (Path.GetFileName(directoryPath).StartsWith("OVR", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.Ovr;

            if (Path.GetFileName(directoryPath).StartsWith("MAP", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.Map;

            if (Path.GetFileName(directoryPath).StartsWith("SND", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.Snd;

            if (Path.GetFileName(directoryPath).StartsWith("PDADOC", StringComparison.OrdinalIgnoreCase))
                return ArchiveType.PdaDoc;

            return ArchiveType.None;
        }
    }
}

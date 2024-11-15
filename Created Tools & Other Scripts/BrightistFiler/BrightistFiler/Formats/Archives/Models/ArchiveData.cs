namespace BrightistFiler.Formats.Archives.Models
{
    internal class ArchiveData
    {
        public ArchiveType Type { get; set; }
        public ArchiveFileData[] Files { get; set; }
    }
}

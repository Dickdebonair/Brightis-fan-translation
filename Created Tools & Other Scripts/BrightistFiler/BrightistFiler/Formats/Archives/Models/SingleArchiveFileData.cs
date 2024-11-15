namespace BrightistFiler.Formats.Archives.Models
{
    internal class SingleArchiveFileData : ArchiveFileData
    {
        public ArchiveFileType Type { get; set; }
        public Stream Data { get; set; }
    }
}

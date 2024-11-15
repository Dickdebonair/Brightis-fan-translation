namespace BrightistFiler.Formats.Archives.Models
{
    internal class MultiArchiveFileData : ArchiveFileData
    {
        public IList<SingleArchiveFileData> Files { get; set; }
    }
}

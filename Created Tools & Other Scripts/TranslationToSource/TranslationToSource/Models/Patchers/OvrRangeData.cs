namespace TranslationToSource.Models.Patchers;

internal class OvrRangeData
{
    public required long OverlayBaseAddress { get; set; }
    public required int OverlaySize { get; set; }
    public required int OverlayMaxSize { get; set; }
}
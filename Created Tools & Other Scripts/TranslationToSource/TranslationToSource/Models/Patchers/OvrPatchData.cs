using TranslationToSource.Models.Sheets;

namespace TranslationToSource.Models.Patchers;

internal class OvrPatchData
{
    public required OverlayConfigData Config { get; set; }
    public required OvrRangeData OverlayRange { get; set; }
    public required OvrTextPatchData[] TextPatches { get; set; }
}
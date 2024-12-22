using TranslationToSource.Models.Sheets;

namespace TranslationToSource.Models.Patchers.Layout
{
    internal class OvrPatchLayoutData
    {
        public required OverlayConfigData Config { get; set; }
        public required OvrRangeData OverlayRange { get; set; }
        public required OvrTextPatchLayoutData[] TextPatches { get; set; }
    }
}

using BrightisRendererV2.Models.Sheets;

namespace BrightisRendererV2.Models.UI.Components;

internal class PopupPreviewData
{
    public OverlaySheetData PopupSheetData { get; set; }
    public OverlaySheetData SubPopupSheetData { get; set; }

    public OverlaySheetData ActiveSheetData { get; set; }
}
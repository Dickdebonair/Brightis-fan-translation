using BrightistRenderer.Models.Sheets;

namespace BrightistRenderer.Models.UI.Components
{
    internal class PopupPreviewData
    {
        public OverlaySheetData PopupSheetData { get; set; }
        public OverlaySheetData SubPopupSheetData { get; set; }
        
        public OverlaySheetData ActiveSheetData { get; set; }
    }
}

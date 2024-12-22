using GoogleSheetsApiV4.Contract.Aspects;

namespace BrightistRenderer.Models.Sheets
{
    internal class OverlayUpdateRawSheetData
    {
        [Column("F")]
        public string? TranslatedText { get; set; }
    }
}
namespace BrightisRendererV2.Models.Sheets;

internal class OverlayRawSheetData
{
    public string Offset { get; set; }
    public string DataOffsets { get; set; }
    public string PrintOffsets { get; set; }
    public string TextType { get; set; }
    public string OriginalText { get; set; }
    public string? TranslatedText { get; set; }
}
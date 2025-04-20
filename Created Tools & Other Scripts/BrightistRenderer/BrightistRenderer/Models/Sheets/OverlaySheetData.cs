namespace BrightistRenderer.Models.Sheets
{
    internal class OverlaySheetData
    {
        public int OverlayIndex { get; set; }
        public long Offset { get; set; }
        public TextType? TextType { get; set; }
        public string OriginalText { get; set; }
        public string TranslatedText { get; set; }
    }
}

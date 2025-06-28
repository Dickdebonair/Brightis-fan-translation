namespace TranslationToSource.Models.Sheets;

internal class OvrSheetData
{
    public long Offset { get; set; }
    public long[]? DataOffsets { get; set; }
    public long[]? PrintOffsets { get; set; }
    public TextType? TextType { get; set; }
    public string? OriginalText { get; set; }
    public string? TranslatedText { get; set; }
}
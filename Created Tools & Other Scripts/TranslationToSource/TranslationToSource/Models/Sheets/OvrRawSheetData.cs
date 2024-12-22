using GoogleSheetsApiV4.Contract.Aspects;

namespace TranslationToSource.Models.Sheets
{
    internal class OvrRawSheetData
    {
        [Column("A")]
        public string Offset { get; set; }
        [Column("B")]
        public string DataOffsets { get; set; }
        [Column("C")]
        public string PrintOffsets { get; set; }
        [Column("D")]
        public string TextType { get; set; }
        [Column("E")]
        public string OriginalText { get; set; }
        [Column("F")]
        public string? TranslatedText { get; set; }
    }
}

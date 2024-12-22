using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using GoogleSheetsApiV4.Contract;
using GoogleSheetsApiV4.Contract.DataClasses;
using TranslationToSource.Models.Sheets;

namespace TranslationToSource.Sheets
{
    internal class OvrTranslationManager
    {
        private readonly ISheetManager _sheet;

        public OvrTranslationManager(ISheetManager sheet)
        {
            _sheet = sheet;
        }

        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties, typeof(OvrRawSheetData))]
        public async Task<IList<OvrSheetData>?> GetTranslationsAsync(OverlayConfigData overlayConfig)
        {
            var result = new List<OvrSheetData>();

            CellIdentifier startCell = CellIdentifier.Parse("A2");
            CellIdentifier endCell = CellIdentifier.Parse($"F{overlayConfig.SheetMaxRow}");

            OvrRawSheetData[]? rows = await _sheet.GetRangeAsync<OvrRawSheetData>(overlayConfig.SheetName, startCell, endCell);
            if (rows == null)
                return null;

            foreach (OvrRawSheetData row in rows)
            {
                string[] dataOffsets = row.DataOffsets.ReplaceLineEndings("").Split(',');
                string[] printOffsets = row.PrintOffsets.ReplaceLineEndings("").Split(',', StringSplitOptions.RemoveEmptyEntries);

                result.Add(new OvrSheetData
                {
                    Offset = long.Parse(row.Offset[2..], NumberStyles.HexNumber),
                    DataOffsets = dataOffsets.Select(x => long.Parse(x[2..], NumberStyles.HexNumber)).ToArray(),
                    PrintOffsets = printOffsets.Length <= 0
                        ? Array.Empty<long>() 
                        : printOffsets.Select(x => long.Parse(x[2..], NumberStyles.HexNumber)).ToArray(),
                    TextType = Enum.Parse<TextType>(row.TextType),
                    OriginalText = row.OriginalText,
                    TranslatedText = row.TranslatedText ?? string.Empty
                });
            }

            return result;
        }
    }
}

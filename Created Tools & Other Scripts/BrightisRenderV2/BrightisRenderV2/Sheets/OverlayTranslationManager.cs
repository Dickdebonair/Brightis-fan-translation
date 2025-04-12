using BrightisRendererV2.Extensions;
using BrightisRendererV2.Models.Sheets;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace BrightisRendererV2.Sheets;

internal class OverlayTranslationManager(SpreadsheetManager spreadsheetManager)
{
    private readonly Dictionary<string, IList<OverlaySheetData>> _sheetLines = [];
    private readonly Dictionary<string, int> _sheetNameToId = [];

    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties, typeof(OverlayRawSheetData))]
    public async Task<IList<OverlaySheetData>?> GetTranslationsAsync(OverlayConfigData overlayConfig)
    {
        if (_sheetLines.TryGetValue(overlayConfig.SheetName, out IList<OverlaySheetData>? result))
            return result;

        _sheetLines[overlayConfig.SheetName] = result = [];
        var spreadsheet = await spreadsheetManager.GetSpreadSheetAsync(0, overlayConfig.SheetMaxRow);

        var sheet = spreadsheet.Sheets.FirstOrDefault();
        _sheetNameToId.Add(overlayConfig.SheetName, sheet.Properties.SheetId.Value);
        var rows = sheet.Data.SelectMany(d => d.RowData).Select(r => r.ToOverlayRawSheetData()).ToList();

        if (rows == null)
            return null;

        foreach (OverlayRawSheetData row in rows)
        {
            string[] dataOffsets = row.DataOffsets.ReplaceLineEndings("").Split(',');
            string[] printOffsets = row.PrintOffsets.ReplaceLineEndings("").Split(',', StringSplitOptions.RemoveEmptyEntries);

            result.Add(new OverlaySheetData
            {
                OverlayIndex = overlayConfig.OverlaySlot,
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

    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties, typeof(OverlayUpdateRawSheetData))]
    public async Task UpdateTranslationsAsync(OverlayConfigData overlayConfig)
    {
        if (!_sheetLines.TryGetValue(overlayConfig.SheetName, out IList<OverlaySheetData>? sheetData))
            return;

        var updateList = new List<OverlayUpdateRawSheetData>();
        foreach (OverlaySheetData data in sheetData)
            updateList.Add(new OverlayUpdateRawSheetData { TranslatedText = data.TranslatedText });

        var sheetId = _sheetNameToId[overlayConfig.SheetName];
        await spreadsheetManager.UpdateRangeAsync(sheetId, updateList);
    }
}
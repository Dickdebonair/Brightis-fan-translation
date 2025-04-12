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

        var spreadsheet = await spreadsheetManager.GetSpreadsheetAsync();
        foreach (var sheet in spreadsheet.Sheets)
        {
            var properties = sheet.Properties;
            var rawSheetData = new List<OverlayRawSheetData>();
            var overlaySheetData = new List<OverlaySheetData>();
            var sheetData = sheet.Data!.FirstOrDefault();
            if (sheetData == null)
            {
                continue;
            }

            for (int i = 1; i < sheetData.RowData.Count; i++)
            {
                var rowData = sheetData.RowData[i];
                var rawOverlaySheetData = rowData.ToOverlayRawSheetData();

                string[] dataOffsets = rawOverlaySheetData.DataOffsets.ReplaceLineEndings("").Split(',');
                string[] printOffsets = rawOverlaySheetData.PrintOffsets.ReplaceLineEndings("").Split(',', StringSplitOptions.RemoveEmptyEntries);

                overlaySheetData.Add(new OverlaySheetData
                {
                    OverlayIndex = overlayConfig.OverlaySlot,
                    Offset = long.Parse(rawOverlaySheetData.Offset[2..], NumberStyles.HexNumber),
                    DataOffsets = dataOffsets.Select(x => long.Parse(x[2..], NumberStyles.HexNumber)).ToArray(),
                    PrintOffsets = printOffsets.Length <= 0
                        ? Array.Empty<long>()
                        : printOffsets.Select(x => long.Parse(x[2..], NumberStyles.HexNumber)).ToArray(),
                    TextType = Enum.Parse<TextType>(rawOverlaySheetData.TextType),
                    OriginalText = rawOverlaySheetData.OriginalText,
                    TranslatedText = rawOverlaySheetData.TranslatedText ?? string.Empty
                });
            }

            _sheetLines.Add(properties.Title, overlaySheetData);
            _sheetNameToId.Add(properties.Title, properties.SheetId.Value);
        }

        return _sheetLines[overlayConfig.SheetName];
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
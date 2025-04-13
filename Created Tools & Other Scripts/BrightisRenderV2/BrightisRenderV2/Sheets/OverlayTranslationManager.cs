using BrightisRendererV2.Extensions;
using BrightisRendererV2.Models.Sheets;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace BrightisRendererV2.Sheets;

internal class OverlayTranslationManager(SpreadsheetManager spreadsheetManager)
{
    private readonly ConcurrentDictionary<string, IList<OverlaySheetData>> _sheetLines = [];
    private readonly ConcurrentDictionary<string, int> _sheetNameToId = [];

    public async Task LoadInitialTranslations(OverlayConfigData[] overlayConfigs)
    {
        var spreadsheet = await spreadsheetManager.GetSpreadsheetAsync();

        Parallel.ForEach(spreadsheet.Sheets, sheet =>
        {
            var properties = sheet.Properties;
            var rawSheetData = new List<OverlayRawSheetData>();
            var overlaySheetData = new List<OverlaySheetData>();
            var sheetData = sheet.Data!.FirstOrDefault();
            if (sheetData == null)
            {
                return;
            }

            var overlayConfig = overlayConfigs.FirstOrDefault(x => x.SheetName == properties.Title);
            if (overlayConfig == null)
            {
                return;
            }

            for (int i = 1; i < overlayConfig.SheetMaxRow; i++)
            {
                var rowData = sheetData.RowData[i];
                if (rowData.Values == null)
                {
                    continue;
                }

                if (rowData.Values.Count < 6)
                {
                    continue;
                }

                var rawOverlaySheetData = rowData.ToOverlayRawSheetData();

                if (!Enum.TryParse<TextType>(rawOverlaySheetData.TextType, out var textType))
                {
                    continue;
                }

                overlaySheetData.Add(new OverlaySheetData
                {
                    OverlayIndex = overlayConfig.OverlaySlot,
                    Offset = long.Parse(rawOverlaySheetData.Offset[2..], NumberStyles.HexNumber),
                    TextType = Enum.Parse<TextType>(rawOverlaySheetData.TextType),
                    OriginalText = rawOverlaySheetData.OriginalText,
                    TranslatedText = rawOverlaySheetData.TranslatedText ?? string.Empty
                });
            }

            _sheetLines.TryAdd(properties.Title, overlaySheetData);
            _sheetNameToId.TryAdd(properties.Title, properties.SheetId.Value);
        });
    }

    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor | DynamicallyAccessedMemberTypes.PublicProperties, typeof(OverlayRawSheetData))]
    public async Task<IList<OverlaySheetData>?> GetTranslationsAsync(OverlayConfigData overlayConfig)
    {
        if (_sheetLines.TryGetValue(overlayConfig.SheetName, out IList<OverlaySheetData>? result))
            return result;

        return null;
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
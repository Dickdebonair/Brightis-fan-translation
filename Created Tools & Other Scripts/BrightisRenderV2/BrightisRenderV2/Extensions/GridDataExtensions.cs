using BrightisRendererV2.Models.Sheets;
using Google.Apis.Sheets.v4.Data;

namespace BrightisRendererV2.Extensions;

internal static class GridDataExtensions
{
    public static OverlayRawSheetData ToOverlayRawSheetData(this RowData data)
    {
        return new OverlayRawSheetData()
        {
            Offset = data.Values[ColumnPositions.Offset].EffectiveValue?.StringValue,
            TextType = data.Values[ColumnPositions.TextType].EffectiveValue?.StringValue ?? "0",
            OriginalText = data.Values[ColumnPositions.OriginalText].EffectiveValue?.StringValue,
            TranslatedText = data.Values[ColumnPositions.TranslatedText].EffectiveValue?.StringValue,
        };
    }
}
using BrightisRendererV2.Models.Sheets;
using Google.Apis.Sheets.v4.Data;

namespace BrightisRendererV2.Extensions;

internal static class GridDataExtensions
{
    public static OverlayRawSheetData ToOverlayRawSheetData(this RowData data)
    {
        return new OverlayRawSheetData()
        {
            Offset = data.Values[0].UserEnteredValue.StringValue,
            DataOffsets = data.Values[0].UserEnteredValue.StringValue,
            PrintOffsets = data.Values[0].UserEnteredValue.StringValue,
            TextType = data.Values[0].UserEnteredValue.StringValue,
            OriginalText = data.Values[0].UserEnteredValue.StringValue,
            TranslatedText = data.Values[0].UserEnteredValue.StringValue,
        };
    }
}
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using GoogleSheetsApiV4.Contract;
using GoogleSheetsApiV4.Contract.DataClasses;
using TranslationToSource.Models.Reports;
using TranslationToSource.Models.Sheets;
using TranslationToSource.Reports;


namespace TranslationToSource.Sheets;

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
        var ovrSheetOutput = new List<OvrSheetData>();

        var startCell = CellIdentifier.Parse("A2");
        var endCell = CellIdentifier.Parse($"F{overlayConfig.SheetMaxRow}");

        var sheetName = overlayConfig.SheetName;
        var errorsOccurred = false;

        ErrorReport.Instance.EnterSection($"OvrTranslationManager Sheet {sheetName}");
        ConsoleReport.Instance.WriteSectionName($"Processing Sheet {overlayConfig.SheetName}");

        OvrRawSheetData[]? rows = await _sheet.GetRangeAsync<OvrRawSheetData>(overlayConfig.SheetName, startCell, endCell);

        if (rows == null)
            return null;

        for (var i = 0; i < rows.Length; i++)
        {
            OvrRawSheetData row = rows[i];

            string[] dataOffsets = row.DataOffsets.ReplaceLineEndings("").Split(',');
            string[] printOffsets = row.PrintOffsets.ReplaceLineEndings("").Split(',', StringSplitOptions.RemoveEmptyEntries);

            try
            {
                var rowData = new OvrSheetData();

                ParsingValues(() => rowData.Offset = long.Parse(row.Offset[2..], NumberStyles.HexNumber),
                    sheetName, nameof(rowData.Offset), i, $"{row.Offset}");

                ParsingValues(() => rowData.DataOffsets = [.. dataOffsets.Select(x => long.Parse(x[2..], NumberStyles.HexNumber))],
                    sheetName, nameof(rowData.DataOffsets), i, $"{rowData.DataOffsets}");

                ParsingValues(() => rowData.PrintOffsets = printOffsets.Length <= 0
                        ? [] : [.. printOffsets.Select(x => long.Parse(x[2..], NumberStyles.HexNumber))],
                    sheetName, nameof(rowData.PrintOffsets), i, $"{rowData.PrintOffsets}");

                ParsingValues(() => rowData.TextType = Enum.Parse<TextType>(row.TextType),
                    sheetName, nameof(rowData.TextType), i, $"{rowData.TextType}");

                ParsingValues(() => rowData.OriginalText = row.OriginalText,
                    sheetName, nameof(rowData.OriginalText), i, $"{rowData.OriginalText}");

                ParsingValues(() => rowData.TranslatedText = row.TranslatedText ?? string.Empty,
                    sheetName, nameof(rowData.TranslatedText), i, $"{rowData.OriginalText}");

                ovrSheetOutput.Add(rowData);
            }
            catch (Exception)
            {
                errorsOccurred = true;
            }
        }

        ErrorReport.Instance.ExitSection();
        ErrorReport.Instance.Persist();

        return errorsOccurred ? null : ovrSheetOutput;
    }

    private static void ParsingValues(Action func, string sheetName, string columnName, int rowIndex, string rowValue)
    {
        try
        {
            func();
        }
        catch (Exception)
        {
            var parseErrorItem = new SheetParseErrorReportItem(sheetName, columnName, rowIndex, rowValue);

            ConsoleReport.Instance.WriteReportItem(parseErrorItem);
            ErrorReport.Instance.AddReportItem(parseErrorItem);

            throw;
        }
    }
}
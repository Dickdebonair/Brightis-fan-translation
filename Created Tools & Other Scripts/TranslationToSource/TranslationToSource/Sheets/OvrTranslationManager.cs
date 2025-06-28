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
        private readonly string errorFileName = "OvrTranslationManager";

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

            var errorsThatOccured = new List<string>();
            bool errorOccured = false;

            FileLogger.AddErrorHead($"OvrTranslationManager Errors For Sheet {sheetName}", errorsThatOccured);

            OvrRawSheetData[]? rows = await _sheet.GetRangeAsync<OvrRawSheetData>(overlayConfig.SheetName, startCell, endCell);

            simpleLogger.CreateHeaderMessage($"Working on the following sheet: {overlayConfig.SheetName}");

            if (rows == null)
            {
                return null;
            }

            foreach (var (row, rowNum) in rows.Select((value, index) => (value, index)))
            {
                simpleLogger.CreateHeaderMessage($"Working on sheet {overlayConfig.SheetName}");
                string[] dataOffsets = row.DataOffsets.ReplaceLineEndings("").Split(',');
                string[] printOffsets = row.PrintOffsets.ReplaceLineEndings("").Split(',', StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    var rowData = new OvrSheetData { };

                    ParsingValues(() =>
                        rowData.Offset = long.Parse(
                                row.Offset[2..],
                                NumberStyles.HexNumber
                            )
                    , "Offset", $"{row.Offset}", sheetName, rowNum);

                    ParsingValues(() => 
                        rowData.DataOffsets = dataOffsets.Select(
                                x => long.Parse(x[2..], NumberStyles.HexNumber)
                        ).ToArray()
                    , "DataOffsets", $"{rowData.DataOffsets}", sheetName, rowNum);

                    ParsingValues(
                        () =>
                            rowData.PrintOffsets = printOffsets.Length <= 0
                            ? Array.Empty<long>()
                            : printOffsets.Select(x => long.Parse(x[2..], NumberStyles.HexNumber)).ToArray()
                        , "printOffset", $"{rowData.PrintOffsets}", sheetName, rowNum
                    );

                    ParsingValues(
                        () =>
                            rowData.TextType = Enum.Parse<TextType>(row.TextType)
                        , "TextType", $"{rowData.TextType}", sheetName, rowNum
                    );

                    ParsingValues(
                        () =>
                            rowData.OriginalText = row.OriginalText
                        , "OriginalText", $"{rowData.OriginalText}", sheetName, rowNum
                    );

                    ParsingValues(
                        () =>
                            rowData.TranslatedText = row.TranslatedText ?? string.Empty
                        , "TranslatedText", $"{rowData.OriginalText}", sheetName, rowNum
                    );

                    ovrSheetOutput.Add(rowData);
                }
                catch (Exception e)
                {
                    errorOccured = true;
                    errorsThatOccured.Add(CreateErrorMessageLine(e));
                }
            }

            if(errorOccured)
            await FileLogger.WriteErrorsToFile(errorsThatOccured);

            return errorOccured ? null : ovrSheetOutput;
        }

        public void ParsingValues(Action func, string columnName, string rowValue, string sheetName, int rowNum)
        {
            var handler = func;
            try
            {
                handler();
            }
            catch (Exception)
            {
                simpleLogger.TranslationManagerParseErrorMessage(columnName, rowValue, sheetName, rowNum);
                throw new Exception($"Could not parse {columnName} with value: {rowValue} from sheet:{sheetName} row:{rowNum}");
            }
        }

        public string CreateErrorMessageLine(Exception e)
        {
            return e.Message;
        }
    }
}

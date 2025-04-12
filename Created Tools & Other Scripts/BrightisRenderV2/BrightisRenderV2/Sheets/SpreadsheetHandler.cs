using BrightisRendererV2.Models.Sheets;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace BrightisRendererV2.Sheets;

internal class SpreadsheetManager
{
    private static readonly string[] Scopes = [SheetsService.Scope.Spreadsheets];

    private const string SpreadsheetId = "1Rflah7eg7Xd9Ir-wCPUm1Hp5s39090MCwiL6MQ0Ryeo";

    private const string GoogleCredentialsFileName = "./mrsquidgy-brightis-credentials.json";

    private SheetsService SheetsService { get; }

    public SpreadsheetManager()
    {
        using var stream = new FileStream(GoogleCredentialsFileName, FileMode.Open, FileAccess.Read);
        var serviceInitializer = new BaseClientService.Initializer
        {
            HttpClientInitializer = GoogleCredential.FromStream(stream).CreateScoped(Scopes)
        };
        SheetsService = new SheetsService(serviceInitializer);
    }

    public async Task<Spreadsheet> GetSpreadSheetAsync(int sheetId, int endRowIndex)
    {
        var getSheetRequest = new GetSpreadsheetByDataFilterRequest()
        {
            DataFilters =
            [
                new DataFilter
                {
                    GridRange = new GridRange(){
                        SheetId = 4,
                        StartColumnIndex = 0,
                        EndColumnIndex = 6,
                        StartRowIndex = 2,
                        EndRowIndex = endRowIndex
                    }
                }
            ]
        };

        var getByDataFilterRequest = SheetsService.Spreadsheets.GetByDataFilter(getSheetRequest, SpreadsheetId);
        return await getByDataFilterRequest.ExecuteAsync();
    }

    public async Task UpdateRangeAsync(int sheetId, IList<OverlayUpdateRawSheetData> updateData)
    {
        try
        {
            // do nothing for now so i don't mess up any current spreadsheets

            //var updateCells = new UpdateCellsRequest()
            //{
            //    Rows = updateData.Select(d => new RowData
            //    {
            //        Values =
            //        [
            //            new CellData()
            //            {
            //                EffectiveValue = new ExtendedValue
            //                {
            //                    StringValue = d.TranslatedText
            //                }
            //            }
            //        ]
            //    }).ToArray(),
            //    Start = new GridCoordinate()
            //    {
            //        SheetId = sheetId,
            //        RowIndex = 1,
            //        ColumnIndex = ColumnPositions.TranslatedText
            //    }
            //};

            //var batching = new BatchUpdateSpreadsheetRequest()
            //{
            //    Requests =
            //    [
            //        new Request
            //        {
            //            UpdateCells = updateCells
            //        }
            //    ]
            //};

            //var updateRequest = SheetsService.Spreadsheets.BatchUpdate(batching, SpreadsheetId);
            //await updateRequest.ExecuteAsync();
        }
        catch (Exception ex)
        {
            // Suppress the exception because I'm lazy atm
        }
    }
}
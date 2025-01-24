using System.Configuration.Assemblies;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Requests;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace Pointer.Finder.Clients
{

    class SpreadSheetHelper
    {
        private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private const string SpreadsheetId = "1Rflah7eg7Xd9Ir-wCPUm1Hp5s39090MCwiL6MQ0Ryeo";
        private const string GoogleCredentialsFileName = "./google-api-key.json";
        private SheetsService SheetsService { get; }

        public BatchUpdateValuesRequest batching { get; set; }

        public SpreadSheetHelper()
        {
            using (var stream = new FileStream(GoogleCredentialsFileName, FileMode.Open, FileAccess.Read))
            {
                var serviceInitializer = new BaseClientService.Initializer
                {
                    HttpClientInitializer = GoogleCredential.FromStream(stream).CreateScoped(Scopes)
                };
                SheetsService = new SheetsService(serviceInitializer);
            }

            batching = new BatchUpdateValuesRequest() {
                Data = new List<ValueRange>()
            };
        }

        public int AddTranslationData(string sheetName, List<CSVDataModel> fileContent, int currentRow)
        {

            try
            {
                int counter = currentRow;
                // var valueRange = new ValueRange();

                var rows = new ValueRange() {
                    Values = new List<IList<object>>() {

                    },
                    Range = $"{sheetName}!A{counter}"
                };

                // valueRange.Values = new List<IList<object>>();
                
                foreach (var record in fileContent)
                {

                    rows.Values.Add( new List<object>() {
                        record.Pointer,
                        record.PointerOffset,
                        0,
                        0,
                        record.OriginalText,
                        record.TranslatedText
                    });

                    counter++;
                }

                batching.Data.Add(rows);

                return currentRow;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }

        }

        public async Task CreateSheetAndHeaders(string sheetName)
        {
            try
            {

                var headersList = new List<string>() {
                    "Offset",
                    "Code Data Offset",
                    "Code Print Offset",
                    "Text Type",
                    "Original Text",
                    "Translated Text",
                    "Comments",
                };

                var valueRange = new ValueRange();

                valueRange.Values = new List<IList<object>>();

                var row = new List<object>();

                foreach (var record in headersList)
                {
                    row.Add(record);
                }

                valueRange.Values.Add(row);

                var request = SheetsService.Spreadsheets.Values.Update(valueRange, SpreadsheetId, $"{sheetName}!A1");
                request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
                await request.ExecuteAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public async Task BulkUpdate() {
            try {

                batching.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW.ToString();

                var response = await SheetsService.Spreadsheets.Values.BatchUpdate(batching, SpreadsheetId).ExecuteAsync();

                batching = new BatchUpdateValuesRequest() {
                    Data = new List<ValueRange>()
                };

            } catch(Exception e ) {
                Console.WriteLine(e);
            }
        }

        public void CreateSheetsForBinFiles()
        {
            try
            {
                int counter = 0;

                var batchUpdates = new BatchUpdateSpreadsheetRequest()
                {
                    Requests = new List<Request>() { }
                };

                string fileNameSuffix = "_OVR.bin";

                while (counter <= 61)
                {

                    string fileName = "";

                    if (counter > 9)
                    {
                        fileName = $"0{counter}{fileNameSuffix}";
                    }
                    else
                    {
                        fileName = $"00{counter}{fileNameSuffix}";
                    }

                    var creatSheetRequest = new AddSheetRequest()
                    {
                        Properties = new()
                        {
                            Title = fileName
                        }
                    };

                    batchUpdates.Requests.Add(new Request()
                    {
                        AddSheet = creatSheetRequest
                    }
                    );

                    counter++;
                }

                SheetsService.Spreadsheets.BatchUpdate(batchUpdates, SpreadsheetId).Execute();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    class CSVDataModel
    {
        public string? Pointer { get; set; }
        public string? PointerOffset { get; set; }
        public string? OriginalText { get; set; }
        public string? TranslatedText { get; set; }
    }

}